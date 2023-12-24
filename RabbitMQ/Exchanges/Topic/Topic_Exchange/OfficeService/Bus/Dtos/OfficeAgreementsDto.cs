using Contracts;
using Contracts.Types;

namespace Bus.Dtos;

public class OfficeAgreementsDto : IOfficeAgreements
{
    public string AgreementType { get; set; }
    public List<PartyRecord> Parties { get; set; }
    public TermsRecord Terms { get; set; }
    public EnumerationRecord? Enumeration { get; set; }
}