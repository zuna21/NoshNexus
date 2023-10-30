

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

    public async Task<ICollection<TableCardDto>> GetTables(Owner owner)
    {
        return await _context.Tables
            .Where(x => x.Restaurant.OwnerId == owner.Id)
            .Select(t => new TableCardDto{
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
