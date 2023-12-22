using InterviewTask.Application.Contract.Driver;
using InterviewTask.Core.Constants;

namespace InterviewTask.Application.IServices;
public interface IDriverService
{
    Task<IEnumerable<DriverDto>> GetAllAsync(Constants.SortDirection sortDirection);
    Task<DriverDto> GetByIdAsync(int id, Constants.SortDirection sortDirection);
    Task<int> CreateAsync(CreateDriverDto driver);
    Task<int> UpdateAsync(UpdateDriverDto driver);
    Task<int> DeleteAsync(int id);
    Task<int> BulkCreateAsync(int length);

}