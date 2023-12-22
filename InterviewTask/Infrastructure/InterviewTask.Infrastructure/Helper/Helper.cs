using InterviewTask.Application.Contract.IHelpers;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace InterviewTask.Infrastructure.Helper;
public class Helper : IHelper
{
    private readonly string? _connectionStrings;
    public Helper(IConfiguration configuration)
    {
        _connectionStrings = configuration.GetConnectionString("DefaultConnection");

    }
    private static Random random = new Random();
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public SqliteConnection GetDBConnection()
    {
        return new SqliteConnection(_connectionStrings);
    }
}