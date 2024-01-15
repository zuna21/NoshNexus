using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

public class MenusController(IMenuService menuService) : DefaultCustomerController
{
    private readonly IMenuService _menuService = menuService;


}
