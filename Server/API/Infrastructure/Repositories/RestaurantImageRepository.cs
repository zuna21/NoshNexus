
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class RestaurantImageRepository : IRestaurantImageRepository
{
    private readonly DataContext _context;
    public RestaurantImageRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddImage(RestaurantImage image)
    {
        _context.RestaurantImages.Add(image);
    }

    public async Task<RestaurantImage> GetImage(int restaurantId, int imageId)
    {
        return await _context.RestaurantImages.FirstOrDefaultAsync(x => x.Id == imageId && x.RestaurantId == restaurantId);
    }

    public async Task<RestaurantImage> GetProfileImage(int restaurantId)
    {
        return await _context.RestaurantImages
            .FirstOrDefaultAsync(x => x.RestaurantId == restaurantId && x.Type == RestaurantImageType.Profile && x.IsDeleted == false);
    }

    public async Task<ICollection<ImageDto>> GetRestaurantGalleryImages(int restaurantId)
    {
        return await _context.RestaurantImages
            .Where(x => x.RestaurantId == restaurantId && x.Type == RestaurantImageType.Gallery && x.IsDeleted == false)
            .Select(i => new ImageDto
            {
                Id = i.Id,
                Size = i.Size,
                Url = i.Url
            })
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
