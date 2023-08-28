namespace WebApplicationAssesment.Application.Users.Models
{
    public class CreateAccountDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public long RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
