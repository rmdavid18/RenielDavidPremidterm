using FluentFTP.Helpers;

namespace RenielDavidPremidActivity.Infrastructure.Domain.Models
{
    public class Video
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DateOfPublished { get; set; }
        public Enums.Type? Type { get; set; }
        
    }
}
