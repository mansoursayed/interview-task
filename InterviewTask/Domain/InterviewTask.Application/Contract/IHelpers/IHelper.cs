using Microsoft.Data.Sqlite;

namespace InterviewTask.Application.Contract.IHelpers;
public interface IHelper
{
    SqliteConnection GetDBConnection();
}