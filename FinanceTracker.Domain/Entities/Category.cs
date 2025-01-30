using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; } = string.Empty;
        public string Type { get; set; } = "Expense";
    }
}
