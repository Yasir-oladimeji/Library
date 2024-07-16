namespace Library.Models
{
    public class ResponseModel<T>
    {
        public string Message{ get; set; }
        public bool IsSucess { get; set; }
        public T Data { get; set; }
    }
}
