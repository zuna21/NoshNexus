﻿namespace API;

public interface IEmployeeService
{
    Task<Response<string>> Create(CreateEmployeeDto createEmployeeDto);
    Task<Response<ICollection<EmployeeCardDto>>> GetEmployees();
    Task<Response<GetEmployeeEditDto>> GetEmployeeEdit(int id);
    Task<Response<EmployeeDetailsDto>> GetEmployee(int id);
}
