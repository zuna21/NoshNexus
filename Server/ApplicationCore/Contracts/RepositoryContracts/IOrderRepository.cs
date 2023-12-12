using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IOrderRepository
{
    void Create(Order order);
    void CreateOrderMenuItems(ICollection<OrderMenuItem> orderMenuItems);
    Task<bool> SaveAllAsync();

    Task<ICollection<OrderCardDto>> GetOwnerInProgressOrders(int ownerId);



    // Employee
    Task<ICollection<OrderCardDto>> GetEmployeeInProgressOrders(int restaurantId);
}