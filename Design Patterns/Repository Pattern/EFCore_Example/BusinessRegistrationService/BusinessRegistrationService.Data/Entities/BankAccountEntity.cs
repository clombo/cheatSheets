namespace BusinessRegistrationService.Data.Entities;

public class BankAccountEntity
{
    public Guid Id { get; set; }
    public string BankName { get; set; }
    public int BranchCode { get; set; }
    public int AccountNumber { get; set; }
    public string AccountType { get; set; }
    public BusinessDetailEntity BusinessDetail { get; set; }
}