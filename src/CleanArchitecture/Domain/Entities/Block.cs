namespace CleanArchitecture.Domain.Entities;

public class Block
{
    public Guid Id { get; set; }
    public Guid DepotId { get; set; }
    public Depot Depot { get; set; } = default!;

    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public bool IsVirtual { get; set; }
    public bool IsActive { get; set; } = true;
    
    public ICollection<Position> Positions { get; set; } = new List<Position>();
}
