using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ORM.Mapping
{
    public class MerchantMapping : IEntityTypeConfiguration<Merchant>
    {
        public void Configure(EntityTypeBuilder<Merchant> entity)
        {
            entity.ToTable("Merchants", schema: "cashflowsystem_schema");

            entity.HasKey(t => t.Id).HasName("PK_MERCHANT");
            entity.HasIndex(x => x.Name).HasDatabaseName("IX_MERCHANT_NAME");

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Name");

            entity.Property(x => x.TaxId)
                .IsRequired()
                .HasMaxLength(4)
                .HasColumnName("TaxId");

            entity.Property(x => x.ContactEmail)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("ContactEmail");

            entity.Property(x => x.ContactPhone)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("ContactPhone");

            entity.Property(x => x.IsActive)
                .IsRequired()
                .HasColumnName("IsActive");
        }
    }
}
