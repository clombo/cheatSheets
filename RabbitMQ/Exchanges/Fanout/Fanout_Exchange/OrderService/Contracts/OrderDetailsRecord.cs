namespace Contracts;

public record OrderDetailsRecord<T>
{
    public string ClientName { get; set; }
    public string ClientEmail { get; set; }
    public double OrderTotal { get; set; }
    public string Currency { get; set; }
    public T PaymentDetails { get; set; }
};