namespace OrderService.API.Models.Types.Payment;

public class CardDetails
{
    public int CardNumber { get; set; }
    public string Expire { get; set; }
    public string CardHolderName { get; set; }
}