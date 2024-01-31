using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IRestaurantImageRepository
{
    void AddImage(RestaurantImage image);
    void AddImages(ICollection<RestaurantImage> images);
    Task<RestaurantImage> GetProfileImage(int restaurantId);
    Task<RestaurantImage> GetImage(int restaurantId, int imageId);
    Task<ICollection<ImageDto>> GetRestaurantGalleryImages(int restaurantId);
    Task<bool> SaveAllAsync();
}

