using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace contoso_receipt_backend.Classes.Statuses
{
    public class StatusChange
    {
        public string Email  { get; set; }
        public Status  Old_status { get; set; }
        public Status New_status { get; set; }

        public string? Comment { get; set; }

        public StatusChange() { }
        public StatusChange(String email, Status old_status, Status new_status, string? comment = null)
        {
            Email = email;
            Old_status = old_status;
            New_status = new_status;
            Comment = comment;
        }
    }
}