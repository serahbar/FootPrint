namespace FootPrint.Domain
{
    public class LogData : ILogData
    {

        public int Id { get; set; }
        public string EntityTpe { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string serializedData { get; set; }
        public DateTime DateTime { get; set; }
    }
}
