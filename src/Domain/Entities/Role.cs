namespace FlightStatusService.Domain.Entities;


public class Role : BaseEntity
{
    public Role()
    {
        Users = new List<User>();
    }
    
    public string? Code { get; set; }

    public List<User> Users { get; set; }
}

