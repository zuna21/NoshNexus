namespace ApplicationCore;

public interface IRestaurantReviewRepository
{
    Task<bool> CanCustomerMakeReview(int customerId, int restaurantId);
    Task<RestaurantReviewDto> GetReviewById(int reviewId);
    void CreateReview(RestaurantReview restaurantReview);
    Task<bool> SaveAllAsync();
}
