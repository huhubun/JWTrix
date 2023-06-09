using JWTrix.Data.Abstractions;

namespace JWTrix.Data.Entities
{
    public class History : EntityBase
    {
        public long Id { get; set; }
        public DateTimeOffset DecodeTime { get; set; }
        public string? Content { get; set; }
        public string? Header { get; set; }
        public string? Payload { get; set; }
    }
}
