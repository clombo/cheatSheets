namespace OrderService.API.Models.Types.Payment;

public class BankingDetails
{
    public string AccountName { get; set; }
    public int AccountNumber { get; set; }
    public string Bank { get; set; }
    public string Branch { get; set; }
    public string SwiftCode { get; set; }
}