﻿using MyWebApi.Dto.CheckIn;
using Newtonsoft.Json;
using Ocelot.Middleware;
using Ocelot.Multiplexer;

namespace MyWebApi.Aggregator
{
    public class ReservasSinCheckInAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            if (responses.Any(x => x.Items.Errors().Count > 0))
            {
                return new DownstreamResponse(null, System.Net.HttpStatusCode.BadRequest, (List<Header>)null, null);
            }

            var reserva_Str = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
            var checkin_Str = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();

            var reserva_lista = JsonConvert.DeserializeObject<List<ReservaDto>>(reserva_Str);
            var checkIn_lista = JsonConvert.DeserializeObject<List<CheckInDto>>(checkin_Str);


            var result = new List<ReservaDto>();
            foreach (var r in reserva_lista)
            {
                int cant = 0;
                foreach (var c in checkIn_lista)
                {
                    if (r.Id.Equals(c.ReservaId))
                        cant++;
                }
                if (cant == 0)
                    result.Add(r);
            }

            var contentBody = JsonConvert.SerializeObject(result);

            var stringContent = new StringContent(contentBody)
            {
                Headers = { ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json") }
            };

            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
        }
    }
}
