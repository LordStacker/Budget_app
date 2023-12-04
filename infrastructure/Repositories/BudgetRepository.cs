using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class BudgetRepository
{
    private NpgsqlDataSource _dataSource;

    public BudgetRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public Budget GetCurrentAmount(int userId)
    {
        const string sql = $@"SELECT * FROM semester_project.budget_management WHERE user_id = @userId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Budget>(sql, new
            {
                userId = userId
            });
        }
    }

    public Budget GetStartAmount(int userId)
    {
        const string sql = $@"SELECT * From semester_project.budget_management WHERE user_id = @userId:";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Budget>(sql, new
            {
                userId = userId
            });
        }
    }

    public Budget UpdateStartAmount(int userId, float updatedStartAmount)
    {
        const string sql = $@"UPDATE semester_project.budget_management SET start_amount = @updatedStartAmount WHERE user_id = @userId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Budget>(sql, new
            {
                userId = userId,
                updatedStartAmount = updatedStartAmount
            });
        }
    }
    
}