namespace contoso_receipt_backend.Classes
{
    public class Merchant
    {
        //Primary Key
        public String Proper_name { get; set; }
        public Merchant() { }
        public Merchant(String proper_name)
        {
            Proper_name = proper_name;
        }   
    }
}