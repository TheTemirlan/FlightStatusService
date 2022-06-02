using Microsoft.AspNetCore.Mvc;
using MediatR;
using FlightStatusService.Application.Flights.Queries.GetFlights;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController : ControllerBase
{
    ISender _mediator;

    private readonly ILogger<FlightsController> _logger;

    public FlightsController(ILogger<FlightsController> logger, ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet(Name = "GetFlights")]
    public async Task<IEnumerable<FlightDto>> Get()
    {
        var flights = await _mediator.Send(new GetFlightsQuery());
        return flights;
    }
}
