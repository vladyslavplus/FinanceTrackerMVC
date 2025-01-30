using FinanceTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Domain.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(t => t.Note)
                .HasMaxLength(80);

            builder.Property(t => t.Date)
                .IsRequired();
        }
    }
}
