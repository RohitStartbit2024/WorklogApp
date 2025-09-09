namespace WorklogApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNo { get; set; } = string.Empty;
        public DateOnly DOB { get; set; }
        public DateOnly DateOfJoining { get; set; }
        public string Password { get; set; } = string.Empty;  // store hashed password

        public int UserRoleId { get; set; }
        public UserRole? UserRole { get; set; }
    }
}
