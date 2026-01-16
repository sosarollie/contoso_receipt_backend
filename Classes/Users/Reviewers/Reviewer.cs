namespace contoso_receipt_backend.Classes.Users.Reviewers
{
    public class Reviewer : User
    {
        public Reviewer() { }
        public Reviewer(String email, String password, String first_name, String last_name)
            :base(email, password, first_name, last_name){}
    }

}