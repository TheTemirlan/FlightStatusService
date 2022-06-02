using FlightStatusService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightStatusService.Application.Common.Interfaces;


public interface IApplicationDbContext
{
    DbSet<Flight> Flights { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
