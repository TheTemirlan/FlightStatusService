using AutoMapper;
using FlightStatusService.Application.Common.Enums;
using FlightStatusService.Application.Common.Exceptions;
using FlightStatusService.Application.Common.Interfaces;
using FlightStatusService.Domain.Entities;
using MediatR;

namespace FlightStatusService.Application.Flights.Commands.UpdateFlightStatus;

public record UpdateFlightStatusCommand : IRequest
{
    public int Id { get; init; }

    public FlightStatus Status { get; init; }
}

public class UpdateFlightStatusCommandHandler : IRequestHandler<UpdateFlightStatusCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateFlightStatusCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateFlightStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Flights
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Flight), request.Id);
        }

        entity.Status = _mapper.Map<Domain.Enums.FlightStatus>(request.Status);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
