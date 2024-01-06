using BusinessRegistrationService.API.Models.Types;

namespace BusinessRegistrationService.API.Models;

public class CreateBusinessDetailRecordRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Category { get; set; }
    public List<AccountDetails> AccountDetails { get; set; }
    public BankAccountDetails BankAccountDetails { get; set; }
}