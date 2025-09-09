namespace WorklogApp.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string ProjectId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
    }
}
