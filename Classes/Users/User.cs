namespace contoso_receipt_backend
{
    public abstract class User
    {
        //Primary Key
        public String Email { get; set; }

        //User properties
        public String Password { get; set; }
        public String First_name { get; set; }
        public String Last_name { get; set; }

        public User() { }
        public User(String email, String password, String first_name, String last_name)
        {
            Email = email;
            Password = password;
            First_name = first_name;
            Last_name = last_name;
        }
    }
}