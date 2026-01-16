namespace contoso_receipt_backend.Classes.Receipts
{
    public class Receipt
    {
        //Primary Key
        public int Id { get; set; }

        //Receipt properties
        public DateOnly Receipt_date { get; set; }
        public Decimal Total_amount { get; set; }

        //Foreign keys
        public String Proper_name { get; set; }
        public String Email { get; set; }
        public String Name { get; set; }
    }


}
