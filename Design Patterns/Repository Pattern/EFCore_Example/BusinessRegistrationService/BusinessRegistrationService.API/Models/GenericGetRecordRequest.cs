namespace BusinessRegistrationService.API.Models;

public class GenericGetRecordRequest<TInclude>
{
    public Guid Id { get; set; }
    public TInclude Include { get; set; }
}