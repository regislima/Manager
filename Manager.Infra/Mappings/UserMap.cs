using Manager.Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manager.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);
            
            builder.Property(user => user.Id)
                    .UseIdentityColumn()
                    .HasColumnType("BIGINT");
            
            builder.Property(user => user.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR(50)");
            
            builder.Property(user => user.Email)
                    .IsRequired()
                    .HasMaxLength(180)
                    .HasColumnName("email")
                    .HasColumnType("VARCHAR(180)");
            
            builder.Property(user => user.Password)
                    .IsRequired()
                    .HasMaxLength(15)
                    .HasColumnName("password")
                    .HasColumnType("VARCHAR(15)");
            
            builder.Property(user => user.CreatedAt)
                    .IsRequired()
                    .HasColumnName("createdat")
                    .HasColumnType("DATETIME");
            
            builder.Property(user => user.UpdatedAt)
                    .HasColumnName("updatedat")
                    .HasColumnType("DATETIME");
        }
    }
}