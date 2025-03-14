using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinanceTracker.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Type { get; set; } = "Expense";
        public string? UserId { get; set; } = string.Empty;
        [JsonIgnore]
        public Users? User { get; set; } = null!;
        [NotMapped]
        public string? TitleWithIcon { 
            get
            {
                return this.Icon + " " + this.Title;
            }
        }
    }
}
