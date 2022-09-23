namespace MyWebApi.Dto.Ejercicio
{
    public class UsersDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public AddressDto address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public CompanyDto company { get; set; }

        public ICollection<PostsDto> posts = new List<PostsDto>();

    }
}
