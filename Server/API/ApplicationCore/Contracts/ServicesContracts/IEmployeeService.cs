namespace API;

public interface IEmployeeService
{
    Task<Response<int>> Create(CreateEmployeeDto createEmployeeDto);
    Task<Response<int>> Update(int employeeId, EditEmployeeDto editEmployeeDto);
    Task<Response<ICollection<EmployeeCardDto>>> GetEmployees();
    Task<Response<GetEmployeeEditDto>> GetEmployeeEdit(int id);
    Task<Response<EmployeeDetailsDto>> GetEmployee(int id);
}
