namespace CleanArchitecture.Shared.Models.Requests.Yards.Blocks;

public class UpdateBlockRequest
{
    public string Code { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsVirtual { get; set; }
    public bool IsActive { get; set; }
}
