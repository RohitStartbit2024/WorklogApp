namespace WorklogApp.Dto
{
    public class CurrentUserDto
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public int UserRoleId { get; set; }
        public string RoleName { get; set; } = default!;
    }

}
