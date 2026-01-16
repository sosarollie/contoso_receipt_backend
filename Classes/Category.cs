namespace contoso_receipt_backend.Classes
{
    public class Category
    {
        //Primary Key
        public String Name { get; set; }

        public Category() { }
        public Category(String name)
        {
            Name = name;
        }
    }
}