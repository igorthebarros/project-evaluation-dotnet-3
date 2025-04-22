using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedKernel.Common;

namespace ORM.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity) 
        {
            entity.ToTable("Users", schema: "cashflowsystem_schema");

            entity.HasKey(t => t.Id).HasName("PK_USER");
            entity.HasIndex(x => x.Username).HasDatabaseName("IX_USER_NAME");

            entity.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("Id");

            entity.Property(x => x.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Username");

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Email");

            entity.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("Phone");

            entity.Property(x => x.Role)
                .IsRequired()
                .HasColumnName("Role");

            entity.Property(x => x.Status)
                .IsRequired()
                .HasColumnName("Status");
        }
    }
}
