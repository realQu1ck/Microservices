using AutoMapper;
using Basket.API.Entities;
using EventBus.Messages.Event;

namespace Basket.API.Mapper;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
    }
}