using AutoMapper;
using Bus.Dtos;
using Contracts;

namespace Bus.Mappings;

public class StoreAgreementsMapping : Profile
{
    public StoreAgreementsMapping()
    {
        CreateStoreAgreementsMapping();
    }

    private void CreateStoreAgreementsMapping()
    {
        CreateMap<IStoreAgreements, StoreAgreementsDto>();
    }
}