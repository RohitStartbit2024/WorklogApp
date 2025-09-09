namespace WorklogApp.Models
{
    public class Worklog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public DateOnly Date { get; set; }
        public int OnlineHours { get; set; }
        public int OfflineHours { get; set; }
        public int OtherHours { get; set; }
        public string Log { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
    }
}
