using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;

namespace API;

public class RestaurantReviewService(
    IUserService userService,
    IRestaurantReviewRepository restaurantReviewRepository,
    IRestaurantRepository restaurantRepository
) : IRestaurantReviewService
{
    private readonly IUserService _userService = userService;
    private readonly IRestaurantReviewRepository _restaurantReviewRepository = restaurantReviewRepository;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

    public async Task<Response<RestaurantReviewDto>> CreateReview(int restaurantId, CreateRestaurantReviewDto createRestaurantReviewDto)
    {
        Response<RestaurantReviewDto> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var restaurant = await _restaurantRepository.GetAnyRestaurantById(restaurantId);
            if (restaurant == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var canMakeReview = await _restaurantReviewRepository.CanCustomerMakeReview(customer.Id, restaurant.Id);
            if (!canMakeReview)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "To leave a review you must activate your account and make at least one order at this restaurant.";
                return response;
            }

            RestaurantReview restaurantReview = new()
            {
                CustomerId = customer.Id,
                Customer = customer,
                RestaurantId = restaurant.Id,
                Restaurant = restaurant,
                Rating = createRestaurantReviewDto.Rating,
                Review = createRestaurantReviewDto.Review
            };

            _restaurantReviewRepository.CreateReview(restaurantReview);
            if (!await _restaurantReviewRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create a review.";
                return response;
            }

            var createdReview = await _restaurantReviewRepository.GetReviewById(restaurantReview.Id);
            if (createdReview == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to get your review.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = createdReview;

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
