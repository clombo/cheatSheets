using AgreementsService.API.Models.Types;
using Contracts.Types;

namespace AgreementsService.API.Models;

public class AgreementsModel
{
    public string AgreementType { get; set; }
    public ForCompany For { get; set; }
    public List<PartyModel> Parties { get; set; }
    public TermsModel Terms { get; set; }
    public EnumerationModel? Enumeration { get; set; }
}