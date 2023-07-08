namespace FootPrint.Domain
{
    public interface IAutitable
    {
         int Id { get; set; }       
         DateTime OperationDateTime { get; set; }
         OperationType OperationType { get; set; }
         int OperationBy { get; set; }
    }

}