namespace contoso_receipt_backend.Classes.Receipts
{
    public class ReceiptDTO
    {

        // the point of this class is to only return the properties we want to expose to the user
        // in this case it's not that neccesary given that I've already modelled the DB and there are no sensitive fields for a receipt
        // but in a real world scenario you might not want to expose all fields of a database and it is good practice to use DTOs

        public int Id { get; set; }
        public DateOnly Receipt_date { get; set; }
        public Decimal Total_amount { get; set; }
        public String Proper_name { get; set; }
        public String Email { get; set; }
        public String CategoryName { get; set; }

        public ReceiptDTO() { }
        public ReceiptDTO(Receipt receipt)
        {
            Id = receipt.Id;
            Receipt_date = receipt.Receipt_date;
            Total_amount = receipt.Total_amount;
            Proper_name = receipt.Proper_name;
            Email = receipt.Email;
            CategoryName = receipt.CategoryName;
        }
       
    }
}
