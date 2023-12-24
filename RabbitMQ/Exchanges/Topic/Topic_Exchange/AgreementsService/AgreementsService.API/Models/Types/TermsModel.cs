namespace AgreementsService.API.Models.Types;

public class TermsModel
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string[] AgreedTerms { get; set; }
    public string[] Deliverables { get; set; }
    public string[] AdditionalInfo { get; set; }
}