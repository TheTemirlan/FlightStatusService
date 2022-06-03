using AutoMapper;
using FlightStatusService.Application.Common.Enums;
using FlightStatusService.Application.Common.Exceptions;
using FlightStatusService.Application.Common.Interfaces;
using FlightStatusService.Domain.Entities;
using MediatR;

namespace FlightStatusService.Application.Flights.Commands.UpdateFlightStatus;

public record CreateFlightCommand : IRequest
{
    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public DateTimeOffset Departure { get; set; }

    public DateTimeOffset Arrival { get; set; }

    public FlightStatus Status { get; set; }
}

public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateFlightCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = new Flight()
        {
            Arrival = request.Arrival,
            Departure = request.Departure,
            Destination = request.Destination,
            Origin = request.Origin,
            Status = _mapper.Map<Domain.Enums.FlightStatus>(request.Status)
        };

        await _context.Flights.AddAsync(flight);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
