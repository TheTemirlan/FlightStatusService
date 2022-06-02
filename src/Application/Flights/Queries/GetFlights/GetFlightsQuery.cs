using AutoMapper;
using FlightStatusService.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightStatusService.Application.Flights.Queries.GetFlights;


public record GetFlightsQuery : IRequest<IEnumerable<FlightDto>>;

public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, IEnumerable<FlightDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFlightsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FlightDto>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {

        var flights = await _context.Flights.ToListAsync();
        List<FlightDto> flightsDto = _mapper.Map<List<FlightDto>>(flights);
        return flightsDto;
    }
}
