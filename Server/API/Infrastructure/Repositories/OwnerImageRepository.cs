
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class OwnerImageRepository : IOwnerImageRepository
{
    private readonly DataContext _context;
    public OwnerImageRepository(
        DataContext context
    )
    {
        _context = context;
    }
    public void AddImage(OwnerImage image)
    {
        _context.OwnerImages.Add(image);
    }

    public async Task<OwnerImage> GetProfileImage(int ownerId)
    {
        return await _context.OwnerImages
            .Where(x => x.OwnerId == ownerId && x.Type == OwnerImageType.Profile)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
