using AutoMapper;
using Bus.Dtos;
using Contracts;

namespace Bus.Mappings;

public class OfficeAgreementsMapping : Profile
{
    public OfficeAgreementsMapping()
    {
        CreateOfficeAgreementsMapping();
    }

    private void CreateOfficeAgreementsMapping()
    {
        CreateMap<IOfficeAgreements, OfficeAgreementsDto>();
    }
}