namespace FinanceTracker.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
