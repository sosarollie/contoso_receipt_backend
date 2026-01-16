namespace contoso_receipt_backend.Classes.Users
{
    public abstract class UserDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            Email = user.Email;
            FirstName = user.First_name;
            LastName = user.Last_name;
        }
    }
}