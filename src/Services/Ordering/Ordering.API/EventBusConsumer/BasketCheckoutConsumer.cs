using AutoMapper;
using EventBus.Messages.Event;
using MassTransit;
using MediatR;
using Ordering.Application.Features.Orders.Commands.CheckoutOrder;

namespace Ordering.API.EventBusConsumer;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMapper mapper;
    private readonly IMediator mediator;
    private readonly ILogger<BasketCheckoutConsumer> logger;
    
    public BasketCheckoutConsumer(IMapper _mapper, IMediator _mediator,
        ILogger<BasketCheckoutConsumer> _logger)
    {
        mapper = _mapper;
        mediator = _mediator;
        logger = _logger;
    }
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = mapper.Map<CheckoutOrderCommand>(context.Message);
        var res = await mediator.Send(command);
        
        logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id = {res}");
    }
}