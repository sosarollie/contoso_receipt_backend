namespace contoso_receipt_backend.Classes.Users.Employees
{
    public class EmployeeDTO : UserDTO
    {
        public EmployeeDTO() { }

        public EmployeeDTO(Employee employee) : base(employee){}
    }
}