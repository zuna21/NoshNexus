namespace ApplicationCore;

public interface IRestaurantReviewRepository
{
    Task<bool> CanCustomerMakeReview(int customerId, int restaurantId);
    void CreateReview(RestaurantReview restaurantReview);
    Task<bool> SaveAllAsync();
}
