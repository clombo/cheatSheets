using BusinessRegistrationService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessRegistrationService.Data.Configurations;

public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccountEntity>
{
    public void Configure(EntityTypeBuilder<BankAccountEntity> builder)
    {
        builder.ToTable("BankAccounts");
        builder.HasKey(x => x.Id);
    }
}