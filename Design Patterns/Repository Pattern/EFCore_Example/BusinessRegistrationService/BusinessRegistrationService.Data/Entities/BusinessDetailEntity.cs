namespace BusinessRegistrationService.Data.Entities;

public class BusinessDetailEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public Guid BankAccountId { get; set; }
    public BankAccountEntity BankAccount { get; set; }
    public ICollection<AccountDetailEntity> AccountDetails { get; set; }
}