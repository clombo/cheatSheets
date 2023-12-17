namespace OrderService.API.Models.Types.Payment;

public class PaymentDetails
{
    public string PaymentMethod { get; set; }
    public BankingDetails? BankingDetails { get; set; }
    public CardDetails? CardDetails { get; set; }
}