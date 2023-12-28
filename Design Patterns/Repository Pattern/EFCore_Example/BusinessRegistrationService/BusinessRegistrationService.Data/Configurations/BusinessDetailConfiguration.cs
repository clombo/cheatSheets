using BusinessRegistrationService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessRegistrationService.Data.Configurations;

public class BusinessDetailConfiguration : IEntityTypeConfiguration<BusinessDetailEntity>
{
    public void Configure(EntityTypeBuilder<BusinessDetailEntity> builder)
    {
        builder.ToTable("BusinessDetails");
        
        builder.HasKey(x => x.Id);

        builder
            .HasOne(d => d.BankAccount)
            .WithOne(s => s.BusinessDetail)
            .HasForeignKey<BusinessDetailEntity>(k => k.BankAccountId)
            .HasConstraintName("FK_BusinessDetail_BankAccount");
    }
}