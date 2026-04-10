using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Shared.Models.Requests.Yards.Blocks;
using CleanArchitecture.Shared.Models.Responses.Yards.Blocks;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Services;

public class BlockService : IBlockService
{
    private readonly ApplicationDbContext _context;

    public BlockService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<BlockResponse>> GetAllAsync()
    {
        return await _context.Blocks
            .Select(x => new BlockResponse
            {
                Id = x.Id,
                DepotId = x.DepotId,
                Code = x.Code,
                Name = x.Name,
                IsVirtual = x.IsVirtual,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }

    public async Task<BlockResponse?> GetByIdAsync(Guid id)
    {
        return await _context.Blocks
            .Where(x => x.Id == id)
            .Select(x => new BlockResponse
            {
                Id = x.Id,
                DepotId = x.DepotId,
                Code = x.Code,
                Name = x.Name,
                IsVirtual = x.IsVirtual,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync();
    }

    public async Task<BlockResponse> CreateAsync(CreateBlockRequest request)
    {
        var depotExists = await _context.Depots.AnyAsync(x => x.Id == request.DepotId);
        if (!depotExists)
            throw new Exception("Depot does not exist.");

        var duplicated = await _context.Blocks
            .AnyAsync(x => x.DepotId == request.DepotId && x.Code == request.Code);

        if (duplicated)
            throw new Exception("Block code already exists in this depot.");

        var entity = new Block
        {
            Id = Guid.NewGuid(),
            DepotId = request.DepotId,
            Code = request.Code,
            Name = request.Name,
            IsVirtual = request.IsVirtual,
            IsActive = true
        };

        _context.Blocks.Add(entity);
        await _context.SaveChangesAsync();

        return new BlockResponse
        {
            Id = entity.Id,
            DepotId = entity.DepotId,
            Code = entity.Code,
            Name = entity.Name,
            IsVirtual = entity.IsVirtual,
            IsActive = entity.IsActive
        };
    }

    public async Task<BlockResponse?> UpdateAsync(Guid id, UpdateBlockRequest request)
    {
        var entity = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return null;

        var duplicated = await _context.Blocks.AnyAsync(x =>
            x.DepotId == entity.DepotId &&
            x.Code == request.Code &&
            x.Id != id);

        if (duplicated)
            throw new Exception("Block code already exists in this depot.");

        entity.Code = request.Code;
        entity.Name = request.Name;
        entity.IsVirtual = request.IsVirtual;
        entity.IsActive = request.IsActive;

        await _context.SaveChangesAsync();

        return new BlockResponse
        {
            Id = entity.Id,
            DepotId = entity.DepotId,
            Code = entity.Code,
            Name = entity.Name,
            IsVirtual = entity.IsVirtual,
            IsActive = entity.IsActive
        };
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Blocks.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return false;

        var hasPositions = await _context.Positions.AnyAsync(x => x.BlockId == id);
        if (hasPositions)
            throw new Exception("Cannot delete block because it still has positions.");

        _context.Blocks.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
