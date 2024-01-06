namespace BusinessRegistrationService.API.Models.Types;

public class BankAccountDetails
{
    public string BankName { get; set; }
    public int BranchCode { get; set; }
    public int AccountNumber { get; set; }
    public string AccountType { get; set; }
}