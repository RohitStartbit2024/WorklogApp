namespace WorklogApp.Models
{
    public class UserProject
    {
        public int Id { get; set; }   // PK

        public int UserId { get; set; }
        public User? User { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}
