using MentoringProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentoringProject.Infrastructure.ConfigurationModel
{
    internal class OwnerCarConfiguration : IEntityTypeConfiguration<OwnerCar>
    {
        public void Configure(EntityTypeBuilder<OwnerCar> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.HasAlternateKey(t => new { t.OwnerId, t.CarId });
            builder.Property(p => p.RegistrationNumber).IsRequired();
        }
    }
}
