using WebApplicationAssesment.Application.Common.Models;

namespace WebApplicationAssesment.Application.Users.Models
{
    public class UserDto : BaseDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public long AccountId { get; set; }
    }
}
