using Contracts;
using Contracts.Types;

namespace AgreementsService.API.Models.MessageDto;

public class AgreementDetails : IStoreAgreements, IOfficeAgreements
{
    public string AgreementType { get; set; }
    public List<PartyRecord> Parties { get; set; }
    public TermsRecord Terms { get; set; }
    public EnumerationRecord? Enumeration { get; set; }
}