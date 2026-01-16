using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace contoso_receipt_backend.Classes.Statuses
{
    public class StatusChange
    {
        public int Id { get; set; }
        public string Email  { get; set; }
        public StatusName  Old_status { get; set; }
        public StatusName New_status { get; set; }

        public string? Comment { get; set; }

        public StatusChange() { }
        public StatusChange(String email, StatusName old_status, StatusName new_status, string? comment = null)
        {
            Email = email;
            Old_status = old_status;
            New_status = new_status;
            Comment = comment;
        }
    }
}