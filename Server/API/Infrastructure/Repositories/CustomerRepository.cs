﻿
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