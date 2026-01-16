namespace contoso_receipt_backend.Classes.Users.Administrators
{
    public class Administrator : User
    {
        public Administrator() { }
        public Administrator(String email, String password, String first_name, String last_name)
            :base(email, password, first_name, last_name){}
    }
}