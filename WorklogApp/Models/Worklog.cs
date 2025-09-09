namespace WorklogApp.Models
{
    public enum WorklogStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public class Worklog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }
        public DateOnly Date { get; set; }
        public double OnlineHours { get; set; }
        public double OfflineHours { get; set; }
        public double OtherHours { get; set; }
        public string Description { get; set; } = string.Empty;
        public WorklogStatus Status { get; set; } = WorklogStatus.Pending;
    }
}
