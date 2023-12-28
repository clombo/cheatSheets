namespace BusinessRegistrationService.Data.Entities;

public class AccountDetailEntity
{
    public Guid Id { get; set; }
    public string ContactPerson { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Guid BusinessDetailId { get; set; }
    public BusinessDetailEntity BusinessDetail { get; set; }
}