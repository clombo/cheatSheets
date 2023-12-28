using BusinessRegistrationService.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessRegistrationService.Data.Configurations;

public class AccountDetailConfiguration : IEntityTypeConfiguration<AccountDetailEntity>
{
    public void Configure(EntityTypeBuilder<AccountDetailEntity> builder)
    {
        builder.ToTable("AccountDetails");
        
        builder.HasKey(x => x.Id);

        builder
            .HasOne(d => d.BusinessDetail)
            .WithMany(o => o.AccountDetails)
            .HasForeignKey(k => k.BusinessDetailId)
            .HasConstraintName("FK_BusinessDetail_AccountDetail");
    }
}