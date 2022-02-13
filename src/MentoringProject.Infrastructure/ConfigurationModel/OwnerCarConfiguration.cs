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
            builder.Property(p => p.Id).HasDefaultValueSql("NEWID()");
            //   builder.HasAlternateKey(k => k.OwnerId);
            //    builder.HasAlternateKey(k => k.CarId);

            builder.HasOne(p => p.Car).WithMany(p => p.OwnerCars);
            builder.HasOne(p => p.Owner).WithMany(p => p.OwnersCars);


            builder.Property(p => p.RegistrationNumber).IsRequired();
        }
    }
}
