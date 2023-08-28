using WebApplicationAssesment.Domain.Common;
using System.Net.Mail;

namespace WebApplicationAssesment.Domain.Users
{
    public class Account : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }

        public bool IsValidUsername()
        {
            try
            {
                MailAddress mailAddress = new MailAddress(Username);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

    }
}
