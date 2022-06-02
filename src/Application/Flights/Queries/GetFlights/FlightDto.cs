using FlightStatusService.Application.Common.Enums;

namespace FlightStatusService.Application.Flights.Queries.GetFlights;


public class FlightDto
{
    public string? Origin { get; set; }

    public string? Destination { get; set; }

    public DateTimeOffset Departure { get; set; }

    public DateTimeOffset Arrival { get; set; }

    public FlightStatus Status { get; set; }
}
