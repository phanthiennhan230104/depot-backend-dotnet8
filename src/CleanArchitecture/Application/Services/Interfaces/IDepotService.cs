using CleanArchitecture.Shared.Models.Requests.Yards.Depots;
using CleanArchitecture.Shared.Models.Responses.Yards.Depots;

namespace CleanArchitecture.Application.Services.Interfaces;

public interface IDepotService
{
    Task<List<DepotResponse>> GetAllAsync();
    Task<DepotResponse?> GetByIdAsync(Guid id);
    Task<DepotResponse> CreateAsync(CreateDepotRequest request);
    Task<DepotResponse?> UpdateAsync(Guid id, UpdateDepotRequest request);
    Task<bool> DeleteAsync(Guid id);
}
