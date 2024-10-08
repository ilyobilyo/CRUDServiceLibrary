namespace CRUDServiceLibrary.Abstract
{
    public class ErrorResponseModel
    {
        public int Status { get; set; }
        public string Message { get; set; } = null!;
    }
}
