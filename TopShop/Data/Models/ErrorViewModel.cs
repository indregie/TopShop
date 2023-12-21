namespace TopShop.Data.Models
{
    public class ErrorViewModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ErrorViewModel(string message, int statusCode)
        {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
