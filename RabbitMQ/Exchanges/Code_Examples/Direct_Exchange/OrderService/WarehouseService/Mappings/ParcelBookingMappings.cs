using AutoMapper;
using Contracts;
using OrderService.Models;

namespace OrderService.Mappings;

public class ParcelBookingMappings : Profile
{
    public ParcelBookingMappings()
    {
        CreateBookingMapping();
    }

    private void CreateBookingMapping()
    {
        CreateMap<ParcelBooking, DeliveryModel>()
            .ForMember(
                d => d.BookingId, 
                mo => 
                    mo.MapFrom(s => s.BookingId)
                )
            .ForMember(
                    d => d.DeliveryAddress,
                    mo => 
                        mo.MapFrom(s => s.Receiver.Address)
                );
    }
}