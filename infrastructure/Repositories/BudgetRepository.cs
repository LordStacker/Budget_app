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
        const string sqlGetBmId = $@"SELECT bm_id FROM semester_project.user_to_bm WHERE user_id = @userId;";
        const string sqlGetCurrentAmount = $@"SELECT * FROM semester_project.budget_management WHERE bm_id = @bmId;";

        using (var conn = _dataSource.OpenConnection())
        {
            var bmId = conn.QueryFirstOrDefault<int>(sqlGetBmId, new { userId });

            if (bmId != 0)
            {
                return conn.QueryFirst<Budget>(sqlGetCurrentAmount, new { bmId });
            }
            else
            {
                throw new Exception("No matching bm_id found for the given user_id");
            }
        }
    }
    
    public Budget UpdateCurrentAmount(int userId, float newCurrentAmount)
    {
        const string sql = $@"UPDATE semester_project.budget_management SET current_amount = @newCurrentAmount WHERE user_id = @userId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Budget>(sql, new
            {
                userId = userId,
                newCurrentAmount = newCurrentAmount
            });
        }
    }
}