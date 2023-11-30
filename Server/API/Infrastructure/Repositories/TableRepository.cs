

using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class TableRepository : ITableRepository
{
    private readonly DataContext _context;
    public TableRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddTable(Table table)
    {
        _context.Add(table);
    }

    public void Delete(Table table)
    {
        _context.Tables.Remove(table);
    }


    public async Task<Table> GetOwnerTable(int tableId, int ownerId)
    {
        return await _context.Tables.FirstOrDefaultAsync(x => x.Id == tableId && x.Restaurant.OwnerId == ownerId);
    }

    public async Task<Table> GetRestaurantTable(int tableId, int restaurantId)
    {
        return await _context.Tables
            .Where(x => x.Id == tableId && x.RestaurantId == restaurantId)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<TableCardDto>> GetTables(int ownerId)
    {
        return await _context.Tables
            .Where(x => x.Restaurant.OwnerId == ownerId)
            .Select(t => new TableCardDto
            {
                Id = t.Id,
                Name = t.Name,
                Restaurant = new TableRestaurant
                {
                    Id = t.RestaurantId,
                    Name = t.Restaurant.Name
                }
            }).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
