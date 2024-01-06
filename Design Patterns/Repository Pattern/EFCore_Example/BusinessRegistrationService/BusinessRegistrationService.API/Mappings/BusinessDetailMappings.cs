using AutoMapper;
using BusinessRegistrationService.API.Models;
using BusinessRegistrationService.API.Models.Types;
using BusinessRegistrationService.Data.Entities;

namespace BusinessRegistrationService.API.Mappings;

public class BusinessDetailMappings : Profile
{
    public BusinessDetailMappings()
    {
        CreateBusinessDetailMapping();
        UpdateBusinessDetailMapping();
    }

    private void UpdateBusinessDetailMapping()
    {
        CreateMap<UpdateBusinessDetailRecordRequest, BusinessDetailEntity>()
            .ForMember(
                d => d.Name,
                o =>
                    o.MapFrom(s => s.Name)
            )
            .ForMember(
                d => d.Address,
                o =>
                    o.MapFrom(s => s.Address)
            )
            .ForMember(
                d => d.Category,
                o =>
                    o.MapFrom(s => s.Category)
            );
    }

    private void CreateBusinessDetailMapping()
    {
        CreateMap<AccountDetails, AccountDetailEntity>()
            .ForMember(
                d => d.Id, 
                o => 
                    o.MapFrom(s => Guid.NewGuid())
                    )
            .ForMember(
                d => d.ContactPerson , 
                o => 
                    o.MapFrom(s => s.ContactPerson)
                )
            .ForMember(
                d => d.EmailAddress , 
                o => 
                    o.MapFrom(s => s.EmailAddress)
            )
            .ForMember(
                d => d.PhoneNumber , 
                o => 
                    o.MapFrom(s => s.PhoneNumber)
            );
        
        CreateMap<BankAccountDetails, BankAccountEntity>()
            .ForMember(
                d => d.Id,
                o =>
                    o.MapFrom(s => Guid.NewGuid())
            )
            .ForMember(
                d => d.BankName,
                o =>
                    o.MapFrom(s => s.BankName)
            )
            .ForMember(
                d => d.BranchCode,
                o =>
                    o.MapFrom(s => s.BranchCode)
            )
            .ForMember(
                d => d.AccountNumber,
                o =>
                    o.MapFrom(s => s.AccountNumber)
            )
            .ForMember(
                d => d.AccountType,
                o =>
                    o.MapFrom(s => s.AccountType)
            );

        CreateMap<CreateBusinessDetailRecordRequest, BusinessDetailEntity>()
            .ForMember(
                d => d.Id,
                o =>
                    o.MapFrom(s => Guid.NewGuid())
            )
            .ForMember(
                d => d.Name,
                o =>
                    o.MapFrom(s => s.Name)
            )
            .ForMember(
                d => d.Address,
                o =>
                    o.MapFrom(s => s.Address)
            )
            .ForMember(
                d => d.Category,
                o =>
                    o.MapFrom(s => s.Category)
            )
            .ForMember(
                d => d.BankAccount,
                o =>
                    o.MapFrom(s => s.BankAccountDetails)
            )
            .ForMember(
                d => d.AccountDetails,
                o =>
                    o.MapFrom(s => s.AccountDetails)
            );
    }
}