namespace CleanArchitecture.Shared.Models.Responses.Yards.Positions;

public class PositionResponse
{
    public Guid Id { get; set; }
    public Guid BlockId { get; set; }
    public int? BayNo { get; set; }
    public int? RowNo { get; set; }
    public int? TierNo { get; set; }
    public string PositionCode { get; set; } = default!;
    public bool IsActive { get; set; }
}
