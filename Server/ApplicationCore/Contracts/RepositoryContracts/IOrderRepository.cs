﻿using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IOrderRepository
{
    void Create(Order order);
    void CreateOrderMenuItems(ICollection<OrderMenuItem> orderMenuItems);
    void BlockCustomer(RestaurantBlockedCustomers restaurantBlockedCustomers);
    Task<bool> SaveAllAsync();

    Task<ICollection<OrderCardDto>> GetOwnerInProgressOrders(int ownerId, OwnerQueryParams.OrdersQueryParams ordersQueryParams);
    Task<PagedList<OrderCardDto>> GetOrdersHistory(int ownerId, OwnerQueryParams.OrdersHistoryQueryParams ordersHistoryQueryParams);
    Task<Order> GetOrderById(int orderId);
    Task<OrderCardDto> GetOrderCardById(int orderId);



    // Employee
    Task<ICollection<OrderCardDto>> GetEmployeeInProgressOrders(int restaurantId);
    Task<Order> GetRestaurantOrderById(int orderId, int restaurantId);


    // Customer
    Task<CustomerLiveRestaurantOrdersDto> GetCustomerInProgressOrders(int restaurantId);

    Task<ICollection<OrderCardDto>> GetCustomerOrders(int customerId, string sq);
    Task<ICollection<OrderCardDto>> GetCustomerAcceptedOrders(int customerId, string sq);
    Task<ICollection<OrderCardDto>> GetCustomerDeclinedOrders(int customerId, string sq);



    // Za hubs
    void AddOrderConnection(OrderConnection orderConnection);
    void RemoveConnection(OrderConnection orderConnection);
    Task<OrderConnection> GetOrderConnectionByUserId(int userId);
}