using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Type { get; set; } = "Expense";
        [NotMapped]
        public string? TitleWithIcon { 
            get
            {
                return this.Icon + " " + this.Title;
            }
        }
    }
}
