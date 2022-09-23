using Microsoft.Extensions.Hosting;

namespace MyWebApi.Dto.Ejercicio
{
    public class PostsDto
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
