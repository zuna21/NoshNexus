using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using ApplicationCore.DTOs.OwnerDtos;

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

    public void AddMany(ICollection<Table> tables)
    {
        _context.Tables.AddRange(tables);
    }

    public void AddTable(Table table)
    {
        _context.Add(table);
    }

    public void Delete(Table table)
    {
        _context.Tables.Remove(table);
    }

    public async Task<ICollection<TableDto>> GetAllRestaurantTableNames(int ownerId, int restaurantId)
    {
        return await _context.Tables
            .Where(x => x.RestaurantId == restaurantId && x.Restaurant.OwnerId == ownerId)
            .Select(x => new TableDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<ICollection<OwnerDtos.TableCardDto>> GetEmployeeTables(int restaurantId)
    {
        return await _context.Tables
            .Where(x => x.RestaurantId == restaurantId)
            .Select(x => new OwnerDtos.TableCardDto
            {
                Id = x.Id,
                Name = x.Name,
                Restaurant = new OwnerDtos.GetRestaurantTableDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name
                }
            })
            .ToListAsync();
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

    public async Task<List<GetTableQrCodeDto>> GetRestaurantTableQrCodes(int restaurantId)
    {
        return await _context.Tables
            .Where(x => x.RestaurantId == restaurantId)
            .Select(x => new GetTableQrCodeDto
            {
                Id = x.Id,
                Name = x.Name,
                Url = $"https://noshnexus.com/selection/{restaurantId}?tableId={x.Id}"
            })
            .ToListAsync();

    }

    public async Task<ICollection<CustomerDtos.TableDto>> GetRestaurantTables(int restaurantId)
    {
        return await _context.Tables
            .Where(x => x.RestaurantId == restaurantId)
            .Select(x => new CustomerDtos.TableDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<PagedList<OwnerDtos.TableCardDto>> GetTables(int ownerId, OwnerQueryParams.TablesQueryParams tablesQueryParams)
    {
        var query = _context.Tables
            .Where(x => x.Restaurant.OwnerId == ownerId);

        if (!string.IsNullOrEmpty(tablesQueryParams.Search))
            query = query.Where(x => x.Name.ToLower().Contains(tablesQueryParams.Search.ToLower()));
        
        if (tablesQueryParams.Restaurant != -1)
            query = query.Where(x => x.RestaurantId == tablesQueryParams.Restaurant);
        
        var totalItems = await query.CountAsync();
        var result = await query
            .Skip(tablesQueryParams.PageSize * tablesQueryParams.PageIndex)
            .Take(tablesQueryParams.PageSize)
            .Select(x => new OwnerDtos.TableCardDto
            {
                Id = x.Id,
                Name = x.Name,
                Restaurant = new OwnerDtos.GetRestaurantTableDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name
                }
            })
            .ToListAsync();

        return new PagedList<OwnerDtos.TableCardDto>
        {
            Result = result,
            TotalItems = totalItems
        };
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
