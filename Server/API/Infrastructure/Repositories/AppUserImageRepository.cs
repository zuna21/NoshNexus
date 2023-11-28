
using Microsoft.EntityFrameworkCore;

namespace API;

public class AppUserImageRepository : IAppUserImageRepository
{
    private readonly DataContext _context;
    public AppUserImageRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddImage(AppUserImage image)
    {
        _context.AppUserImages.Add(image);
    }

    public async Task<AppUserImage> GetProfileImage(int userId)
    {
        return await _context.AppUserImages
            .Where(x => x.AppUserId == userId && x.IsDeleted == false)
            .FirstOrDefaultAsync();
    }

    public async Task<ImageDto> GetUserProfileImage(int userId)
    {
        return await _context.AppUserImages
            .Where( x => 
                x.AppUserId == userId && 
                x.IsDeleted == false && 
                x.Type == AppUserImageType.Profile
            )
            .Select(x => new ImageDto
            {
                Id = x.Id,
                Size = x.Size,
                Url = x.Url
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
