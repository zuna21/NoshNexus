
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs.CustomerDtos;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;
    public CustomerRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void Create(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public async Task<GetAccountDetailsDto> GetAccountDetails(int customerId)
    {
        return await _context.Customers
            .Where(x => x.Id == customerId)
            .Select(x => new GetAccountDetailsDto
            {
                City = x.City,
                Country = x.Country.Name,
                Description = x.Description,
                FirstName = x.FirstName,
                Id = x.Id,
                IsActivated = x.IsActivated,
                Joined = x.CreatedAt,
                LastName = x.LastName,
                ProfileImage = x.AppUser.AppUserImages
                    .Where(im => im.IsDeleted == false && im.Type == AppUserImageType.Profile)
                    .Select(im => im.Url)
                    .FirstOrDefault() ?? "https://noshnexus.com/images/default/default-profile.png",
                Username = x.UniqueUsername
            })
            .FirstOrDefaultAsync();
    }

    public async Task<GetAccountEditDto> GetAccountEdit(int customerId)
    {
        return await _context.Customers.Where(x => x.Id == customerId)
            .Select(x => new GetAccountEditDto
            {
                Id = x.Id,
                City = x.City,
                CountryId = x.CountryId ?? -1,
                Description = x.Description,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.UniqueUsername,
                ProfileImage = x.AppUser.AppUserImages
                    .Where(im => im.IsDeleted == false && im.Type == AppUserImageType.Profile)
                    .Select(im => new ProfileImageDto
                    {
                        Id = im.Id,
                        Url = im.Url
                    })
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Customer> GetCustomerByUsername(string username)
    {
        return await _context.Customers
            .Where(x => x.UniqueUsername == username)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
