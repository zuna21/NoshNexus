﻿using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IOrderService
{
    Task<Response<bool>> CreateOrder(int restaurantId, CustomerDtos.CreateOrderDto createOrderDto);
    Task<Response<int>> BlockCustomer(int orderId);
    Task<Response<ICollection<OrderCardDto>>> GetOwnerInProgressOrders(OwnerQueryParams.OrdersQueryParams ordersQueryParams);
    Task<Response<PagedList<OrderCardDto>>> GetOrdersHistory(OwnerQueryParams.OrdersHistoryQueryParams ordersHistoryQueryParams);



    // Employee
    Task<Response<ICollection<OrderCardDto>>> GetEmployeeInProgressOrders();




    // Employee Or Owner
    Task<Response<int>> AcceptOrder(int orderId);
    Task<Response<int>> DeclineOrder(int orderId, DeclineReasonDto declineReasonDto);



    // Customer
    Task<Response<ICollection<OrderCardDto>>> GetCustomerOrders(CustomerQueryParams.OrdersQueryParams ordersQueryParams);    
}

