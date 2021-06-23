namespace Models
{
    public class ResponseModel
    {
        public int OpCode { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}