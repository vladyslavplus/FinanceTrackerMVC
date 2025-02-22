using FinanceTracker.Domain.Configurations;
using FinanceTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {   
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Messages> Messages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Title = "Food", Icon = "🍔", Type = "Expense" },
                new Category { Id = 2, Title = "Salary", Icon = "💰", Type = "Income" },
                new Category { Id = 3, Title = "Transport", Icon = "🚗", Type = "Expense" },
                new Category { Id = 4, Title = "Shopping", Icon = "🛍️", Type = "Expense" },
                new Category { Id = 5, Title = "Investment", Icon = "📈", Type = "Income" }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, CategoryId = 1, Amount = 50.75m, Note = "Lunch", Date = DateTime.Now.AddDays(-2) },
                new Transaction { Id = 2, CategoryId = 2, Amount = 2000.00m, Note = "Monthly salary", Date = DateTime.Now.AddDays(-10) },
                new Transaction { Id = 3, CategoryId = 3, Amount = 15.00m, Note = "Taxi ride", Date = DateTime.Now.AddDays(-1) },
                new Transaction { Id = 4, CategoryId = 4, Amount = 100.00m, Note = "New shoes", Date = DateTime.Now.AddDays(-5) },
                new Transaction { Id = 5, CategoryId = 5, Amount = 300.00m, Note = "Stock purchase", Date = DateTime.Now.AddDays(-3) }
            );
        }
    }
}
