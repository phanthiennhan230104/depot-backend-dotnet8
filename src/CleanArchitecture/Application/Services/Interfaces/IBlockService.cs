using CleanArchitecture.Shared.Models.Requests.Yards.Blocks;
using CleanArchitecture.Shared.Models.Responses.Yards.Blocks;

namespace CleanArchitecture.Application.Services.Interfaces;

public interface IBlockService
{
    Task<List<BlockResponse>> GetAllAsync();
    Task<BlockResponse?> GetByIdAsync(Guid id);
    Task<BlockResponse> CreateAsync(CreateBlockRequest request);
    Task<BlockResponse?> UpdateAsync(Guid id, UpdateBlockRequest request);
}
