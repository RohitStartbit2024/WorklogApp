namespace WorklogApp.Models
{
    public class UserRole
    {
        public int Id { get; set; }
        public string Role { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
