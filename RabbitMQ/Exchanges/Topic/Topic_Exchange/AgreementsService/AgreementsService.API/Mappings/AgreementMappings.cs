using AgreementsService.API.Models;
using AgreementsService.API.Models.MessageDto;
using AgreementsService.API.Models.Types;
using AutoMapper;
using Contracts.Types;

namespace AgreementsService.API.Mappings;

public class AgreementMappings : Profile
{
    public AgreementMappings()
    {
        CreateAgreementsMapping();
    }

    private void CreateAgreementsMapping()
    {
        CreateMap<AgreementsModel,AgreementDetails>()
            .ForMember(
                d => d.AgreementType , 
                mo => 
                    mo.MapFrom(s => s.AgreementType)
                )
            .ForMember(
                d => d.Parties, mo => 
                    mo.MapFrom(s => s.Parties)
                )
            .ForMember(
                d => d.Terms,
                mo => 
                    mo.MapFrom(s => s.Terms)
                )
            .ForMember(
                    d => d.Enumeration,
                    mo => 
                    mo.MapFrom(s => s.Enumeration)
                );
        CreateMap<EnumerationModel,EnumerationRecord>()
            .ForMember(
                d => d.Price,
                mo => 
                    mo.MapFrom(s => s.Price)
                )
            .ForMember(
                d => d.DueDate,
                mo => 
                    mo.MapFrom(s => s.DueDate)
            );
        CreateMap<PartyModel,PartyRecord>()
            .ForMember(
                    d => d.Name,
                    mo => 
                        mo.MapFrom(s => s.Name)
                )
            .ForMember(
                d => d.Address,
                mo => 
                    mo.MapFrom(s => s.Address)
            )
            .ForMember(
                d => d.ContactPerson,
                mo => 
                    mo.MapFrom(s => s.ContactPerson)
            );
        CreateMap<TermsModel,TermsRecord>()
            .ForMember(
                    d => d.StartDate,
                    mo => 
                        mo.MapFrom(s => s.StartDate)
                )
            .ForMember(
                d => d.EndDate,
                mo => 
                    mo.MapFrom(s => s.EndDate)
            )
            .ForMember(
                d => d.AgreedTerms,
                mo => 
                    mo.MapFrom(s => s.AgreedTerms)
            )
            .ForMember(
                d => d.Deliverables,
                mo => 
                    mo.MapFrom(s => s.Deliverables)
            )
            .ForMember(
                d => d.AdditionalInfo,
                mo => 
                    mo.MapFrom(s => s.AdditionalInfo)
            );
    }
}