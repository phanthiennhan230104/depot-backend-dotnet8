using CleanArchitecture.Shared.Models.Requests.Yards.Positions;
using CleanArchitecture.Shared.Models.Responses.Yards.Positions;

namespace CleanArchitecture.Application.Services.Interfaces;

public interface IPositionService
{
    Task<IEnumerable<PositionResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PositionResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PositionResponse> CreateAsync(CreatePositionRequest request, CancellationToken cancellationToken = default);
    Task<PositionResponse?> UpdateAsync(Guid id, UpdatePositionRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
