using Microsoft.AspNetCore.Mvc;
using MediatR;
using FlightStatusService.Application.Flights.Queries.GetFlights;
using FlightStatusService.Application.Flights.Commands.UpdateFlightStatus;

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

    [HttpPut("{id}", Name = "UpdateFlight")]
    public async Task<ActionResult> Update(int id, UpdateFlightStatusCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpPost(Name = "CreateFlight")]
    public async Task<ActionResult<int>> Create(CreateFlightCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }
}
