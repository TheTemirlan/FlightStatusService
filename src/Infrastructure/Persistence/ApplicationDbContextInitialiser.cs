using FlightStatusService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FlightStatusService.Infrastructure.Persistence;


public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        if (!_context.Flights.Any())
        {
            _context.Flights.AddRange(new Flight
            {
                Arrival = DateTimeOffset.Now + TimeSpan.FromHours(4),
                Origin = "Moscow",
                Destination = "Karaganda",
                Departure = DateTimeOffset.Now,
                Status = Domain.Enums.FlightStatus.Delayed
            }, new Flight
            {
                Arrival = DateTimeOffset.Now + TimeSpan.FromHours(4),
                Origin = "New York",
                Destination = "Kuba",
                Departure = DateTimeOffset.Now,
                Status = Domain.Enums.FlightStatus.InTime
            }, new Flight
            {
                Arrival = DateTimeOffset.Now + TimeSpan.FromHours(4),
                Origin = "Seoul",
                Destination = "Mars",
                Departure = DateTimeOffset.Now,
                Status = Domain.Enums.FlightStatus.Cancelled
            }
            );

            await _context.SaveChangesAsync();
        }
    }
}
