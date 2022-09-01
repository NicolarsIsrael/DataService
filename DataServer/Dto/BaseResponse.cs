namespace DataServer.Dto
{
    public class BaseResponse
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
    }
}
