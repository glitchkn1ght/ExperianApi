namespace ExperianApi.Models.Response.Common
{
    public abstract class BaseResponse
    {
        public bool IsSuccess { get; set; } = false;

        public string Message { get; set; }
    }
}
