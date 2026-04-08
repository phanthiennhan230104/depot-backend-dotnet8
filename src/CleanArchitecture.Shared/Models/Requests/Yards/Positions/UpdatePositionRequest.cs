namespace CleanArchitecture.Shared.Models.Requests.Yards.Positions;

public class UpdatePositionRequest
{
    public int? BayNo { get; set; }
    public int? RowNo { get; set; }
    public int? TierNo { get; set; }
    public string PositionCode { get; set; } = default!;
    public bool IsActive { get; set; }
}
