using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaApi.Domain.employee.entitie;

namespace MinhaApi.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {

        
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employee");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150);
            
            builder.Property(e => e.Age)
           .IsRequired();

            builder.Property(e => e.Photo)
           .IsRequired(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(e => e.Email)
                .IsUnique(); 

            builder.Property(e => e.Password)
                .IsRequired();

            builder.Property(e => e.IsEmailSend)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
    
}
