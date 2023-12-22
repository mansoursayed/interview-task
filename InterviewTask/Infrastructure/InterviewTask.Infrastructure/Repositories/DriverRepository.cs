using Dapper;
using InterviewTask.Application.Contract.IHelpers;
using InterviewTask.Application.IRepositories;
using InterviewTask.Core.Constants;
using InterviewTask.Core.Entities;

namespace InterviewTask.Infrastructure.Repositories;
public class DriverRepository : IDriverRepository
{
    private readonly IHelper _helper;

    public DriverRepository(IHelper helper)
    {
        _helper = helper;
    }

    public async Task<int> BulkInsertAsync(List<Driver> drivers)
    {
        var sql = "Insert into Driver (FirstName,LastName,Email,PhoneNumber) VALUES (@FirstName,@LastName,@Email,@PhoneNumber)";

        using (var connection = _helper.GetDBConnection())
        {
            connection.Open();
            return await connection.ExecuteAsync(sql, drivers);
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        var sql = "DELETE FROM Driver WHERE Id = @Id";

        using (var connection = _helper.GetDBConnection())
        {
            connection.Open();
            return await connection.ExecuteAsync(sql, new { Id = id });
        }
    }

    public async Task<IEnumerable<Driver>> GetAllAsync(Constants.SortDirection sortDirection)
    {
        var sql = "SELECT * FROM Driver";

        using (var connection = _helper.GetDBConnection())
        {
            connection.Open();
            var results = await connection.QueryAsync<Driver>(sql);
            if (sortDirection == Constants.SortDirection.Ascending)
            {
                results = results.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
            }
            else if (sortDirection == Constants.SortDirection.Descending)
            {
                results = results.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);
            }
            return results;
        }
    }

    public async Task<Driver> GetByIdAsync(int id, Constants.SortDirection sortdirection)
    {
        var sql = "SELECT * FROM Driver WHERE Id = @Id";

        using (var connection = _helper.GetDBConnection())
        {
            connection.Open();
            var result = await connection.QuerySingleOrDefaultAsync<Driver>(sql, new { Id = id });
            if (result != null)
            {
                if (sortdirection == Constants.SortDirection.Ascending)
                {
                    result.FirstName = string.Concat(result.FirstName.ToList().OrderBy(c => c));
                    result.LastName = string.Concat(result.LastName.OrderBy(c => c));
                }
                else if (sortdirection == Constants.SortDirection.Descending)
                {
                    result.FirstName = string.Concat(result.FirstName.OrderByDescending(c => c));
                    result.LastName = string.Concat(result.LastName.OrderByDescending(c => c));
                }
            }
            return result;
        }
    }

    public async Task InitDrivertableAsync()
    {
        var sql = @"
                CREATE TABLE IF NOT EXISTS 
                Driver (
                    Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    FirstName TEXT NOT NULL,
                    LastName TEXT NOT NULL,
                    Email TEXT,
                    PhoneNumber TEXT
                );
            ";

        using (var connection = _helper.GetDBConnection())
        {
            connection.Open();
            await connection.ExecuteAsync(sql);
        }
    }

    public async Task<int> InsertAsync(Driver driver)
    {
        var sql = "Insert into Driver (FirstName,LastName,Email,PhoneNumber) VALUES (@FirstName,@LastName,@Email,@PhoneNumber)";

        using (var connection = _helper.GetDBConnection())
        {
            connection.Open();
            return await connection.ExecuteAsync(sql, driver);
        }

    }

    public async Task<int> UpdateAsync(Driver driver)
    {
        var sql = "UPDATE Driver SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber  WHERE Id = @Id";


        using (var connection = _helper.GetDBConnection())
        {
            connection.Open();
            return await connection.ExecuteAsync(sql, driver);
        }
    }
}