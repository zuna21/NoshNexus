using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IOrderRepository
{
    void Create(Order order);
    void CreateOrderMenuItems(ICollection<OrderMenuItem> orderMenuItems);
    Task<bool> SaveAllAsync();

    Task<ICollection<OrderCardDto>> GetOwnerInProgressOrders(int ownerId);
    Task<ICollection<OrderCardDto>> GetOrdersHistory(int ownerId);



    // Employee
    Task<ICollection<OrderCardDto>> GetEmployeeInProgressOrders(int restaurantId);
    Task<Order> GetRestaurantOrderById(int orderId, int restaurantId);


    // Customer
    Task<CustomerLiveRestaurantOrdersDto> GetCustomerInProgressOrders(int restaurantId);

    Task<ICollection<OrderCardDto>> GetCustomerOrders(int customerId, string sq);
    Task<ICollection<OrderCardDto>> GetCustomerAcceptedOrders(int customerId, string sq);
    Task<ICollection<OrderCardDto>> GetCustomerDeclinedOrders(int customerId, string sq);
}