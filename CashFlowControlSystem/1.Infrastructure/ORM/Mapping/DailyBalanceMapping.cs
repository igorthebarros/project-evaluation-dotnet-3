using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM.Mapping
{
    public class DailyBalanceMapping : IEntityTypeConfiguration<DailyBalance>
    {
        public void Configure(EntityTypeBuilder<DailyBalance> entity)
        {
            entity.ToTable("DailyBalance", schema: "cashflowsystem_schema");

            entity.HasKey(x => x.Id).HasName("PK_DAILY_BALANCE");
            entity.HasIndex(x => x.MerchantId).HasDatabaseName("IX_DAILY_BALANCE_MERCHANT_ID");

            entity.HasOne(x => x.Merchant)
                .WithMany(x => x.DailyBalances)
                .HasForeignKey(x => x.MerchantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.OpeningBalance)
                .IsRequired()
                .HasColumnName("OpeningBalance");

            entity.Property(x => x.TotalDebits)
                .IsRequired()
                .HasColumnName("TotalDebits");

            entity.Property(x => x.TotalCredits)
                .IsRequired()
                .HasColumnName("TotalCredits");

            entity.Property(x => x.ClosingBalance)
                .IsRequired()
                .HasColumnName("ClosingBalance");

            entity.Property(x => x.TransactionCount)
                .IsRequired()
                .HasColumnName("TransactionCount");
        }
    }
}
