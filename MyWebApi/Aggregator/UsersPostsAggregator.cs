using MyWebApi.Dto.Ejercicio;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace MyWebApi.Aggregator
{
    public class UsersPostsAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }

            var VueloStr = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var checkInStr = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var users_lista = JsonConvert.DeserializeObject<List<UsersDto>>(VueloStr);
            var posts_lista = JsonConvert.DeserializeObject<List<PostsDto>>(checkInStr);

            foreach (var u in users_lista)
            {
                foreach (var p in posts_lista)
                {
                    if (u.id.Equals(p.userId))
                    {
                        u.posts.Add(p);
                    }
                }
            }

            var contentBody = JsonConvert.SerializeObject(users_lista);

            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
