using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
    Task<ICollection<MenuCardDto>> GetMenus(int ownerId);
    Task<MenuDetailsDto> GetMenu(int menuId, int ownerId);
    Task<GetMenuEditDto> GetMenuEdit(int menuId, int ownerId);
    Task<bool> SaveAllAsync();


    // Employee
    Task<ICollection<MenuCardDto>> GetEmployeeMenuCardDtos(int restaurantId);
    Task<MenuDetailsDto> GetEmployeeMenu(int menuId, int restaurantId);

    // For global
    Task<Menu> GetOwnerMenu(int menuId, int ownerId);
}