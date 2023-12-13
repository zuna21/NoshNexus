using ApplicationCore.DTOs;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IOrderService
{
    Task<Response<bool>> CreateOrder(int restaurantId, CreateOrderDto createOrderDto);
    Task<Response<ICollection<OrderCardDto>>> GetOwnerInProgressOrders();


    // Employee
    Task<Response<ICollection<OrderCardDto>>> GetEmployeeInProgressOrders();

    // Customer
    Task<Response<CustomerLiveRestaurantOrdersDto>> GetCustomerInProgressOrders(int restaurantId);
    
}

