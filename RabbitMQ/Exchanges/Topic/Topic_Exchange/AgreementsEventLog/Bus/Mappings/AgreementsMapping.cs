using AutoMapper;
using Bus.DTOs;
using Contracts;

namespace Bus.Mappings;

public class AgreementsMapping : Profile
{
    public AgreementsMapping()
    {
        CreateAgreementsMapping();
    }

    private void CreateAgreementsMapping()
    {
        CreateMap<IOfficeAgreements, AgreementsDto>();
        CreateMap<IStoreAgreements, AgreementsDto>();
    }
}