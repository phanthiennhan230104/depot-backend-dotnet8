namespace CleanArchitecture.Shared.Models.Requests.Yards.Depots;

public class UpdateDepotRequest
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public bool IsActive { get; set; }
}
