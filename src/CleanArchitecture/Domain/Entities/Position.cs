namespace CleanArchitecture.Domain.Entities;

public class Position
{
    public Guid Id { get; set; }

    public Guid BlockId { get; set; }
    public Block Block { get; set; } = default!;

    public int? BayNo { get; set; }
    public int? RowNo { get; set; }
    public int? TierNo { get; set; }

    public string PositionCode { get; set; } = default!;
    public bool IsActive { get; set; } = true;
}
