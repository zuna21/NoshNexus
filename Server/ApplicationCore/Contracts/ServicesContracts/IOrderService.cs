using ApplicationCore.DTOs;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IOrderService
{
    Task<Response<bool>> CreateOrder(int restaurantId, CreateOrderDto createOrderDto);
    Task<Response<ICollection<OrderCardDto>>> GetOwnerInProgressOrders(OrdersQueryParams ordersQueryParams);
    Task<Response<ICollection<OrderCardDto>>> GetOrdersHistory(OrdersHistoryQueryParams ordersHistoryQueryParams);


    // Employee
    Task<Response<ICollection<OrderCardDto>>> GetEmployeeInProgressOrders();
    Task<Response<int>> AcceptOrder(int orderId);
    Task<Response<int>> DeclineOrder(int orderId, DeclineReasonDto declineReasonDto);


    // Customer
    Task<Response<CustomerLiveRestaurantOrdersDto>> GetCustomerInProgressOrders(int restaurantId);
    Task<Response<ICollection<OrderCardDto>>> GetCustomerOrders(string sq);
    Task<Response<ICollection<OrderCardDto>>> GetCustomerAcceptedOrders(string sq);
    Task<Response<ICollection<OrderCardDto>>> GetCustomerDeclinedOrders(string sq);
    
}

