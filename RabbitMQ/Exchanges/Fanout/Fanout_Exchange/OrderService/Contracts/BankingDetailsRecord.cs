namespace Contracts;

public record BankingDetailsRecord
{
    public string AccountName { get; set; }
    public int AccountNumber { get; set; }
    public string Bank { get; set; }
    public string Branch { get; set; }
    public string SwiftCode { get; set; }
};