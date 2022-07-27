namespace KaelStore.Service.DTO.Response
{
    public class Response<T>
    {
        public Response(T data, string message = null)
        {
            Data = data;
            Status = "Success";
            Message = message;
        }
        public Response(string errorMessage)
        {
            Status = "Error";
            Message = errorMessage;
        }

        public string Status { get; private set; }
        public T Data { get; private set; }
        public string? Message { get; private set; }
    }
}
