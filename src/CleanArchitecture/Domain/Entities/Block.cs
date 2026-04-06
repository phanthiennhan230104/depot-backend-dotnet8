namespace CleanArchitecture.Domain.Entities;

public class Block
{
    public Guid Id { get; set; }
    public Guid DepotId { get; set; }

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsVirtual { get; set; }
    public bool IsActive { get; set; } = true;

    public Depot Depot { get; set; } = default!;
}
