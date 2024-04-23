namespace Identity.DTOs
{
    public class ResponseDto
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
