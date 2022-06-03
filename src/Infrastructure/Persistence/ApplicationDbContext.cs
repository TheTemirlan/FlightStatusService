using System.Reflection;
using FlightStatusService.Application.Common.Interfaces;
using FlightStatusService.Domain.Entities;
using Duende.IdentityServer.EntityFramework.Options;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace FlightStatusService.Infrastructure.Persistence;


public class ApplicationDbContext : ApiAuthorizationDbContext<IdentityUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
    }

    public DbSet<Flight> Flights => Set<Flight>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        string adminRoleName = "admin";
        string userRoleName = "user";

        string adminEmail = "admin@mail.ru";
        string adminPassword = "123456";

        // Add roles
        Role adminRole = new Role { Id = 1, Code = adminRoleName };
        Role userRole = new Role { Id = 2, Code = userRoleName };
        User adminUser = new User { Id = 1, Username = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

        modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
        modelBuilder.Entity<User>().HasData(new User[] { adminUser });
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
