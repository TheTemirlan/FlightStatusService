namespace FlightStatusService.Domain.Entities;


public class User : BaseEntity
{
    public string? Username { get; set; }

    public string? Password { get; set; }

    public int RoleId { get; set; }

    public FlightStatus Status { get; set; }
}

