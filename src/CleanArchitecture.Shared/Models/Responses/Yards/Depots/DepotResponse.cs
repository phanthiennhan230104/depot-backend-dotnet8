namespace CleanArchitecture.Shared.Models.Responses.Yards.Depots;

public class DepotResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public bool IsActive { get; set; }
}
