using ApplicationCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class RestaurantReviewRepository(
    DataContext context
) : IRestaurantReviewRepository
{
    private readonly DataContext _context = context;

    public async Task<bool> CanCustomerMakeReview(int customerId, int restaurantId)
    {
        /**
            Uslov da napravi review je:
                1.) Ima aktivan profil
                2.) Da nije blokiran od strane restorana
                3.) Ima barem jednu narudzbu koja je prihvacena ili odbijena
        */

        bool canMakeReview = true;

        canMakeReview = await _context.Customers
            .Where(x => x.Id == customerId)
            .Select(x => x.IsActivated)
            .FirstOrDefaultAsync();

        if (!canMakeReview) return canMakeReview;

        canMakeReview = !await _context.RestaurantBlockedCustomers
            .AnyAsync(x => x.CustomerId == customerId && x.RestaurantId == restaurantId);
            
        if (!canMakeReview) return canMakeReview;

        canMakeReview = await _context.Orders
            .AnyAsync(x => 
                x.CustomerId == customerId && 
                x.RestaurantId == restaurantId && 
                (x.Status == ApplicationCore.Entities.OrderStatus.Accepted || x.Status == ApplicationCore.Entities.OrderStatus.Declined)
            );

        if (!canMakeReview) return canMakeReview;



        return canMakeReview;
    }

    public void CreateReview(RestaurantReview restaurantReview)
    {
        _context.RestaurantReviews.Add(restaurantReview);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
