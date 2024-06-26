﻿using API.Infrastructure;
using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.SignalR;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

namespace API;

public class OrderService(
    IOrderRepository orderRepository,
    IUserService userService,
    IRestaurantRepository restaurantRepository,
    IMenuItemRepository menuItemRepository,
    ITableRepository tableRepository,
    ICustomerRepository customerRepository,
    ISettingRepository settingRepository,
    IHubContext<OrderHub> orderHub,
    IAppUserRepository appUserRepository,
    IFirebaseNotificationService firebaseNotificationService
    ) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IMenuItemRepository _menuItemRepository = menuItemRepository;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IUserService _userService = userService;
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IHubContext<OrderHub> _orderHub = orderHub;
    private readonly ISettingRepository _settingRepository = settingRepository;
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly IFirebaseNotificationService _firebaseNotificationService = firebaseNotificationService;

    public async Task<Response<int>> AcceptOrder(int orderId)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            var employee = await _userService.GetEmployee();
            if (owner == null && employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            order.Status = OrderStatus.Accepted;
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to accept order.";
                return response;
            }
            
            var userOrder = await _appUserRepository.GetAppUserByCustomerId(order.CustomerId);
            if (userOrder != null && userOrder.FcmToken != null)
            {
                FirebaseMessageDto firebaseMessageDto = new()
                {
                    Title = "Accepted",
                    Body = "Your order has been accepted.",
                    DeviceToken = userOrder.FcmToken
                };
                await _firebaseNotificationService.SendOrderNotification(firebaseMessageDto);
            }

            response.Status = ResponseStatus.Success;
            response.Data = order.Id;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
        }

        return response;
    }

    public async Task<Response<int>> BlockCustomer(int orderId)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var order = await _orderRepository.GetOrderById(orderId);
            var restaurant = await _restaurantRepository.GetOwnerRestaurant(order.RestaurantId, owner.Id);
            var customer = await _customerRepository.GetCustomerById(order.CustomerId);

            if (order == null || restaurant == null || customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            RestaurantBlockedCustomers restaurantBlockedCustomers = new()
            {
                RestaurantId = restaurant.Id,
                CustomerId = customer.Id,
                Restaurant = restaurant,
                Customer = customer
            };

            _orderRepository.BlockCustomer(restaurantBlockedCustomers);
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to block user";
                return response;
            }

            order.Status = OrderStatus.Declined;
            order.DeclineReason = $"User {customer.UniqueUsername} is blocked.";

            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to decline order.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = order.Id;


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<bool>> CreateOrder(int restaurantId, CustomerDtos.CreateOrderDto createOrderDto)
    {
        Response<bool> response = new();
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

            if (await _settingRepository.IsCustomerBlocked(customer.Id, restaurant.Id))
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You cannot place an order for this restaurant. Looks like you have been blocked!";
                return response;
            }

            var table = await _tableRepository.GetRestaurantTable(createOrderDto.TableId, restaurant.Id);
            if (table == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            List<MenuItem> menuItems = [];
            foreach (var menuItemId in createOrderDto.MenuItemIds)
            {
                var menuItem = await _menuItemRepository.GetRestaurantMenuItemEntity(restaurant.Id, menuItemId);
                if (menuItem == null)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Please select all menu items from one restaurant.";
                    return response;
                }
                menuItem.OrderCount++;
                menuItems.Add(menuItem);
            }

            if (menuItems.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Something went wrong.";
                return response;
            }


            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to increase order count.";
                return response;
            }



            int totalItems = menuItems.Count;
            double totalPrice = 0;
            foreach (var menuItem in menuItems)
            {
                if (menuItem.HasSpecialOffer) totalPrice += menuItem.SpecialOfferPrice;
                else totalPrice += menuItem.Price;
            }

            var order = new Order
            {
                CustomerId = customer.Id,
                Customer = customer,
                RestaurantId = restaurant.Id,
                Restaurant = restaurant,
                TableId = table.Id,
                Table = table,
                Note = createOrderDto.Note,
                TotalItems = totalItems,
                TotalPrice = totalPrice
            };

            _orderRepository.Create(order);
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create order";
                return response;
            }

            var orderMenuItems = menuItems.Select(x => new OrderMenuItem
            {
                MenuItemId = x.Id,
                MenuItem = x,
                OrderId = order.Id,
                Order = order
            })
            .ToList();

            _orderRepository.CreateOrderMenuItems(orderMenuItems);
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create order.";
                return response;
            }

            var orderCard = await _orderRepository.GetOrderCardById(order.Id);
            if (orderCard == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            await _orderHub.Clients.Group(restaurant.Name).SendAsync("ReceiveOrder", orderCard);


            response.Status = ResponseStatus.Success;
            response.Data = true;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> DeclineOrder(int orderId, DeclineReasonDto declineReasonDto)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            var employee = await _userService.GetEmployee();
            if (owner == null && employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            order.Status = OrderStatus.Declined;
            order.DeclineReason = declineReasonDto.Reason;
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to decline order.";
                return response;
            }

            var orderUser = await _appUserRepository.GetAppUserByCustomerId(order.CustomerId);
            if (orderUser != null && orderUser.FcmToken != null)
            {
                FirebaseMessageDto firebaseMessageDto = new()
                {
                    Title = "Declined",
                    Body = order.DeclineReason,
                    DeviceToken = orderUser.FcmToken
                };
                await _firebaseNotificationService.SendOrderNotification(firebaseMessageDto);
            }

            response.Status = ResponseStatus.Success;
            response.Data = order.Id;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<OrderCardDto>>> GetCustomerOrders(CustomerQueryParams.OrdersQueryParams ordersQueryParams)
    {
        Response<ICollection<OrderCardDto>> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _orderRepository.GetCustomerOrders(customer.Id, ordersQueryParams);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<OrderCardDto>>> GetEmployeeInProgressOrders()
    {
        Response<ICollection<OrderCardDto>> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _orderRepository.GetEmployeeInProgressOrders(employee.RestaurantId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<PagedList<OrderCardDto>>> GetOrdersHistory(OwnerQueryParams.OrdersHistoryQueryParams ordersHistoryQueryParams)
    {
        Response<PagedList<OrderCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _orderRepository.GetOrdersHistory(owner.Id, ordersHistoryQueryParams);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<OrderCardDto>>> GetOwnerInProgressOrders(OwnerQueryParams.OrdersQueryParams ordersQueryParams)
    {
        Response<ICollection<OrderCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var orders = await _orderRepository.GetOwnerInProgressOrders(owner.Id, ordersQueryParams);
            if (orders == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = orders;


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
        }

        return response;
    }
}
