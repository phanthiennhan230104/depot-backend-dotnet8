namespace CleanArchitecture.Shared.Models.Requests.Yards.Blocks;

public class CreateBlockRequest
{
    public Guid DepotId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsVirtual { get; set; }
}
