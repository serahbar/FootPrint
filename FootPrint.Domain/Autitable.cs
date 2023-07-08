namespace FootPrint.Domain
{
    public class Autitable : IAutitable
    {
        public DateTime OperationDateTime { get; set; }
        public OperationType OperationType { get; set; }
        public int OperationBy { get; set; }
        public int Id { get; set; }
    }

}