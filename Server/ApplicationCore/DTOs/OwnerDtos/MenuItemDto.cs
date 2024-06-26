﻿namespace ApplicationCore.DTOs.OwnerDtos;

public class MenuItemDto
{

}

public class CreateMenuItemDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public bool HasSpecialOffer { get; set; }
    public double SpecialOfferPrice { get; set; }
}

public class EditMenuItemDto 
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool HasSpecialOffer { get; set; }
    public double SpecialOfferPrice { get; set; }
}

public class MenuItemCardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }
    public bool HasSpecialOffer { get; set; }
    public double SpecialOfferPrice { get; set; }
    public string Currency { get; set; }
}

public class GetMenuItemDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public bool HasSpecialOffer { get; set; }
    public double SpecialOfferPrice { get; set; }
    public string Description { get; set; }
    public int TodayOrders { get; set; }
}

public class GetMenuItemEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool HasSpecialOffer { get; set; }
    public double SpecialOfferPrice { get; set; }
    public ImageDto ProfileImage { get; set; }
}