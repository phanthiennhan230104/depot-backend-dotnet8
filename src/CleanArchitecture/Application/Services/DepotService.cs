using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Shared.Models.Requests.Yards.Depots;
using CleanArchitecture.Shared.Models.Responses.Yards.Depots;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Services;

public class DepotService : IDepotService
{
    private readonly ApplicationDbContext _context;

    public DepotService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<DepotResponse>> GetAllAsync()
    {
        return await _context.Depots
            .OrderBy(x => x.Code)
            .Select(x => new DepotResponse
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Address = x.Address,
                IsActive = x.IsActive
            })
            .ToListAsync();
    }

    public async Task<DepotResponse?> GetByIdAsync(Guid id)
    {
        return await _context.Depots
            .Where(x => x.Id == id)
            .Select(x => new DepotResponse
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Address = x.Address,
                IsActive = x.IsActive
            })
            .FirstOrDefaultAsync();
    }

    public async Task<DepotResponse> CreateAsync(CreateDepotRequest request)
    {
        var isCodeExists = await _context.Depots.AnyAsync(x => x.Code == request.Code);
        if (isCodeExists)
            throw new Exception("Depot code already exists.");

        var entity = new Depot
        {
            Id = Guid.NewGuid(),
            Code = request.Code.Trim(),
            Name = request.Name.Trim(),
            Address = request.Address?.Trim(),
            IsActive = true
        };

        _context.Depots.Add(entity);
        await _context.SaveChangesAsync();

        return new DepotResponse
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Address = entity.Address,
            IsActive = entity.IsActive
        };
    }

    public async Task<DepotResponse?> UpdateAsync(Guid id, UpdateDepotRequest request)
    {
        var entity = await _context.Depots.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return null;

        var isCodeExists = await _context.Depots.AnyAsync(x => x.Code == request.Code && x.Id != id);
        if (isCodeExists)
            throw new Exception("Depot code already exists.");

        entity.Code = request.Code.Trim();
        entity.Name = request.Name.Trim();
        entity.Address = request.Address?.Trim();
        entity.IsActive = request.IsActive;

        await _context.SaveChangesAsync();

        return new DepotResponse
        {
            Id = entity.Id,
            Code = entity.Code,
            Name = entity.Name,
            Address = entity.Address,
            IsActive = entity.IsActive
        };
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _context.Depots.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null) return false;

        _context.Depots.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
