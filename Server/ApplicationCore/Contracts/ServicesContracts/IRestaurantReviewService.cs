using ApplicationCore.DTOs;

namespace ApplicationCore;

public interface IRestaurantReviewService
{
    Task<Response<RestaurantReviewDto>> CreateReview(int restaurantId, CreateRestaurantReviewDto createRestaurantReviewDto);     
}
