using FlightStatusService.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FlightStatusService.Application.Flights.Commands.UpdateFlightStatus;

public class CreateFlightCommandValidator : AbstractValidator<CreateFlightCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateFlightCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Origin)
            .MaximumLength(256).WithMessage("Maximum length is 256")
            .NotEmpty().WithMessage("Origin is required.");
        RuleFor(v => v.Destination)
            .MaximumLength(256).WithMessage("Maximum length is 256")
            .NotEmpty().WithMessage("Destination is required.");
        RuleFor(v => v.Departure)
            .NotEmpty().WithMessage("Departure is required.");
        RuleFor(v => v.Arrival)
           .NotEmpty().WithMessage("Arrival is required.");
        RuleFor(v => v.Status)
            .NotEmpty().WithMessage("Status is required");
    }
}
