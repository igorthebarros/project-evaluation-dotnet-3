using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM.Mapping
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> entity)
        {
            entity.ToTable("Transactions", schema: "cashflowsystem_schema");

            entity.HasKey(t => t.Id).HasName("PK_TRANSACTION");
            entity.HasIndex(x => x.MerchantId).HasDatabaseName("IX_TRANSACTION_MERCHANTID");

            entity.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.MerchantId)
                .IsRequired()
                .HasColumnName("MerchantId");

            entity.HasOne(x => x.Merchant)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.MerchantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.Amount)
                .IsRequired()
                .HasColumnName("Amount");

            entity.Property(x => x.Currency)
                .IsRequired()
                .HasColumnName("Currency");

            entity.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("Currency");

            entity.Property(x => x.PaymentMethod)
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnName("PaymentMethod");

            entity.Property(x => x.Status)
                .IsRequired()
                .HasMaxLength(9)
                .HasColumnName("Status");
        }
    }
}
