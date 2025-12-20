namespace TrainerManager.Shared.DTOs
{
    public class TrainerSummaryDto
    {
        public int Id { get; init; }
        public string? FullName { get; init; } = string.Empty;
        public string? Email { get; init; } = string.Empty;
        public string? Field { get; init; } = string.Empty;
        public int Experience { get; init; }
        public decimal HourlyRate { get; init; }
    }
}