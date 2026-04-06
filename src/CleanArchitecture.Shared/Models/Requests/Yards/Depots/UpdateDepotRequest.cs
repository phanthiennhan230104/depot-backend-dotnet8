namespace CleanArchitecture.Shared.Models.Requests.Yards.Depots;

public class UpdateDepotRequest
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public bool IsActive { get; set; }
}
