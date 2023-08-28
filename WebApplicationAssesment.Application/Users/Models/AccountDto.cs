using WebApplicationAssesment.Application.Common.Models;

namespace WebApplicationAssesment.Application.Users.Models
{
    public class AccountDto : BaseDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
    }
}
