namespace Hrms.Lite.Shared.General
{

    public class ResponseEntity
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public ResponseEntity(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
    public class ResponseEntity<TData>
    {
        public TData Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public ResponseEntity(string message, bool success, TData data)
        {
            Message = message;
            Success = success;
            Data = data;
        }
    }
}
