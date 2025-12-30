namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class RecentTrainerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public int Experience { get; set; }
    }
}
