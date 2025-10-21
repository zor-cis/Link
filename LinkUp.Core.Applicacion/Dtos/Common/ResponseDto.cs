namespace LinkUp.Core.Applicacion.Dtos.Response
{
    public class ResponseDto<T> where T : class
    {
        public bool IsError { get; set; } = false;
        public string? MessageResult { get; set; }
        public T? Result { get; set; }
    }
}
