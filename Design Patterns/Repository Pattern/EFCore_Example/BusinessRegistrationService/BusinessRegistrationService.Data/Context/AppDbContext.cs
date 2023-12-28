using System.Reflection;
using BusinessRegistrationService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessRegistrationService.Data.Context;

public class AppDbContext :DbContext
{
    public virtual DbSet<AccountDetailEntity> AccountDetails { get; set; }
    public virtual DbSet<BankAccountEntity> BankAccounts{ get; set; }
    public virtual DbSet<BusinessDetailEntity> BusinessDetails { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}