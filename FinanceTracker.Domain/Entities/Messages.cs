using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Domain.Entities
{
    public class Messages
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
