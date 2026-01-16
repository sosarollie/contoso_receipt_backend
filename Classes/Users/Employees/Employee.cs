namespace contoso_receipt_backend.Classes.Users.Employees
{
    public class Employee : User
    {
        public Employee() { }
        public Employee(String email, String password, String first_name, String last_name)
            :base(email, password, first_name, last_name){}
    }

}