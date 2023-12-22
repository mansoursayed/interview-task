using InterviewTask.Core.Constants;
using InterviewTask.Core.Entities;

namespace InterviewTask.Application.IRepositories;
public interface IDriverRepository
{
    Task<IEnumerable<Driver>> GetAllAsync(Constants.SortDirection sortDirection);
    Task<Driver> GetByIdAsync(int id, Constants.SortDirection sortdirection);
    Task<int> InsertAsync(Driver driver);
    Task<int> UpdateAsync(Driver driver);
    Task<int> DeleteAsync(int id);
    Task<int> BulkInsertAsync(List<Driver> drivers);
    Task InitDrivertableAsync();
}