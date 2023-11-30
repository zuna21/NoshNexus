
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class EmployeeImageRepository : IEmployeeImageRepository
{
    private readonly DataContext _context;
    public EmployeeImageRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddImage(EmployeeImage image)
    {
        _context.EmployeeImages.Add(image);
    }

    public async Task<EmployeeImage> GetProfileImage(int employeeId)
    {
        return await _context.EmployeeImages.FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.Type == EmployeeImageType.Profile);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
