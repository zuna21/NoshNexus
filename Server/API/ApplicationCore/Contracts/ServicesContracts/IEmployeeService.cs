namespace API;

public interface IEmployeeService
{
    Task<Response<string>> Create(CreateEmployeeDto createEmployeeDto);
    Task<Response<ICollection<EmployeeCardDto>>> GetEmployees();
}
