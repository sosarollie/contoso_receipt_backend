namespace contoso_receipt_backend.Classes.Users.Reviewers
{
    public class ReviewerDTO: UserDTO
    {
        public ReviewerDTO() { }

        public ReviewerDTO(Reviewer reviewer): base(reviewer){}
    
    }
}