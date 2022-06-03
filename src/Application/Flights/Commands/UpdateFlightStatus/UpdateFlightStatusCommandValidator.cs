using FlightStatusService.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FlightStatusService.Application.Flights.Commands.UpdateFlightStatus;

public class UpdateFlightStatusCommandValidator : AbstractValidator<UpdateFlightStatusCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateFlightStatusCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Id)
            .NotEmpty().WithMessage("Id is required.");
        RuleFor(v => v.Status)
            .NotEmpty().WithMessage("Status is required.");
    }
}
