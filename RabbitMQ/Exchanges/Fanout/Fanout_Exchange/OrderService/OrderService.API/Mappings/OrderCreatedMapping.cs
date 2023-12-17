using AutoMapper;
using Contracts;
using OrderService.API.Models;
using OrderService.API.Models.Types.Payment;

namespace OrderService.API.Mappings;

public class OrderCreatedMapping : Profile
{
    public OrderCreatedMapping()
    {
        CreateOrderProcessMapping();
        CreatePaymentDetailsMapping();
    }

    private void CreatePaymentDetailsMapping()
    {
        CreateMap<BankingDetails, BankingDetailsRecord>();
        CreateMap<CardDetails, CreditCardDetailsRecord>();
    }

    private void CreateOrderProcessMapping()
    {
        CreateMap<OrderDetails, OrderDetailsRecord<BankingDetailsRecord>>()
            .ForMember(
                d => d.ClientName , 
                mo => 
                    mo.MapFrom(s => s.Client.ClientName)
                    )
            .ForMember(
                d => d.ClientEmail , 
                mo => 
                    mo.MapFrom(s => s.Client.ClientEmail)
            )
            .ForMember(
                d => d.OrderTotal , 
                mo => 
                    mo.MapFrom(s => s.OrderTotal)
            )
            .ForMember(
                d => d.Currency , 
                mo => 
                    mo.MapFrom(s => "ZAR")
            )
            .ForMember(
                d => d.PaymentDetails , 
                mo => 
                    mo.MapFrom(s => s.Payment.BankingDetails)
            );

        CreateMap<OrderDetails, OrderDetailsRecord<CreditCardDetailsRecord>>()
            .ForMember(
                d => d.ClientName , 
                mo => 
                    mo.MapFrom(s => s.Client.ClientName)
            )
            .ForMember(
                d => d.ClientEmail , 
                mo => 
                    mo.MapFrom(s => s.Client.ClientEmail)
            )
            .ForMember(
                d => d.OrderTotal , 
                mo => 
                    mo.MapFrom(s => s.OrderTotal)
            )
            .ForMember(
                d => d.Currency , 
                mo => 
                    mo.MapFrom(s => "ZAR")
            )
            .ForMember(
                d => d.PaymentDetails , 
                mo => 
                    mo.MapFrom(s => s.Payment.CardDetails)
            );
    }
}