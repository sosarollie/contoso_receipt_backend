namespace contoso_receipt_backend.Classes.Statuses
{
    public enum StatusName
    {
        Approved,
        Rejected,
        PendingReview
    }
    public class Status
    {
        //Primary Key
        public StatusName Name { get; set; }

        public Status() { }
        public string? RejectionReason { get; set; }
        public Status(StatusName name, string? rejectionReason = null)
        {
            Name = name;
            RejectionReason = rejectionReason;
        }
    }
}