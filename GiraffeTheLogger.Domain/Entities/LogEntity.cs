namespace GiraffeTheLogger.Domain.Entities {
    public class LogEntity : BaseEntity {
        public string AppTitle { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int? ErrorCode { get; set; }
        public long? UserRef { get; set; }
        public double CreationDateTime { get; set; }
    }
}