namespace contoso_receipt_backend.Classes.Users.Administrators
{
    public class AdministratorDTO : UserDTO
    {
        public AdministratorDTO() { }
        public AdministratorDTO(Administrator administrator) : base(administrator){}
    }
}