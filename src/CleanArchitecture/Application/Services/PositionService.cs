using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Shared.Models.Requests.Yards.Positions;
using CleanArchitecture.Shared.Models.Responses.Yards.Positions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Services;

public class PositionService : IPositionService
{
    private readonly ApplicationDbContext _context;

    public PositionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PositionResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Positions
            .AsNoTracking()
            .OrderBy(x => x.PositionCode)
            .Select(x => new PositionResponse
            {
                Id = x.Id,
                BlockId = x.BlockId,
                BayNo = x.BayNo,
                RowNo = x.RowNo,
                TierNo = x.TierNo,
                PositionCode = x.PositionCode,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<PositionResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Positions
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new PositionResponse
            {
                Id = x.Id,
                BlockId = x.BlockId,
                BayNo = x.BayNo,
                RowNo = x.RowNo,
                TierNo = x.TierNo,
                PositionCode = x.PositionCode,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PositionResponse> CreateAsync(CreatePositionRequest request, CancellationToken cancellationToken = default)
    {
        var block = await _context.Blocks
            .FirstOrDefaultAsync(x => x.Id == request.BlockId, cancellationToken);

        if (block is null)
        {
            throw new Exception("Block not found.");
        }

        if (block.IsVirtual)
        {
            if (request.BayNo.HasValue || request.RowNo.HasValue || request.TierNo.HasValue)
            {
                throw new Exception("Virtual block cannot have Bay/Row/Tier.");
            }
        }
        else
        {
            if (!request.BayNo.HasValue || !request.RowNo.HasValue || !request.TierNo.HasValue)
            {
                throw new Exception("Non-virtual block requires Bay/Row/Tier.");
            }
        }

        var duplicatedCode = await _context.Positions
            .AnyAsync(x => x.PositionCode == request.PositionCode, cancellationToken);

        if (duplicatedCode)
        {
            throw new Exception("Position code already exists.");
        }

        var entity = new Position
        {
            Id = Guid.NewGuid(),
            BlockId = request.BlockId,
            BayNo = request.BayNo,
            RowNo = request.RowNo,
            TierNo = request.TierNo,
            PositionCode = request.PositionCode.Trim(),
            IsActive = true
        };

        _context.Positions.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new PositionResponse
        {
            Id = entity.Id,
            BlockId = entity.BlockId,
            BayNo = entity.BayNo,
            RowNo = entity.RowNo,
            TierNo = entity.TierNo,
            PositionCode = entity.PositionCode,
            IsActive = entity.IsActive
        };
    }

    public async Task<PositionResponse?> UpdateAsync(Guid id, UpdatePositionRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Positions
            .Include(x => x.Block)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        if (entity.Block.IsVirtual)
        {
            if (request.BayNo.HasValue || request.RowNo.HasValue || request.TierNo.HasValue)
            {
                throw new Exception("Virtual block cannot have Bay/Row/Tier.");
            }
        }
        else
        {
            if (!request.BayNo.HasValue || !request.RowNo.HasValue || !request.TierNo.HasValue)
            {
                throw new Exception("Non-virtual block requires Bay/Row/Tier.");
            }
        }

        var duplicatedCode = await _context.Positions
            .AnyAsync(x => x.Id != id && x.PositionCode == request.PositionCode, cancellationToken);

        if (duplicatedCode)
        {
            throw new Exception("Position code already exists.");
        }

        entity.BayNo = request.BayNo;
        entity.RowNo = request.RowNo;
        entity.TierNo = request.TierNo;
        entity.PositionCode = request.PositionCode.Trim();
        entity.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return new PositionResponse
        {
            Id = entity.Id,
            BlockId = entity.BlockId,
            BayNo = entity.BayNo,
            RowNo = entity.RowNo,
            TierNo = entity.TierNo,
            PositionCode = entity.PositionCode,
            IsActive = entity.IsActive
        };
    }
}
