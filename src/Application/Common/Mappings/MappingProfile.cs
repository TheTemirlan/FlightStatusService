using AutoMapper;
using FlightStatusService.Application.Flights.Queries.GetFlights;
using FlightStatusService.Domain.Entities;

namespace FlightStatusService.Application.Common.Mappings;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Flight, FlightDto>();
        CreateMap<Enums.FlightStatus, Domain.Enums.FlightStatus>();
    }
}