namespace LoginAPI.Model
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public Users user { get; set; }

        public List<Users> listUser { get; set; }
     }
}
