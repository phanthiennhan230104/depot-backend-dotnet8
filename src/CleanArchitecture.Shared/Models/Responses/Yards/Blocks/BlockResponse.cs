namespace CleanArchitecture.Shared.Models.Responses.Yards.Blocks;

public class BlockResponse
{
    public Guid Id { get; set; }
    public Guid DepotId { get; set; }
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsVirtual { get; set; }
    public bool IsActive { get; set; }
}
