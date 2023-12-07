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
        const string sqlGetCurrentAmount = $@"SELECT 
                                            bm_id as {nameof(Budget.Id)},
                                            start_amount as {nameof(Budget.StartAmount)},
                                            current_amount as {nameof(Budget.CurrentAmount)}
                                            FROM semester_project.budget_management WHERE bm_id = @bmId;";

        using (var conn = _dataSource.OpenConnection())
        {
            try
            {
                var bmId = conn.QueryFirstOrDefault<int>(sqlGetBmId, new { userId });
                return conn.QueryFirst<Budget>(sqlGetCurrentAmount, new { bmId });
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while executing the sql query");
                throw;
            }
        }
    }
    
    public Budget UpdateCurrentAmount(int userId, float newCurrentAmount)
    {
        const string sqlUpdate = $@"UPDATE semester_project.budget_management SET current_amount = @newCurrentAmount WHERE bm_id = @bmId
                                            RETURNING  
                                            bm_id as {nameof(Budget.Id)},
                                            start_amount as {nameof(Budget.StartAmount)},
                                            current_amount as {nameof(Budget.CurrentAmount)};";
        const string sqlGetBmId = $@"SELECT bm_id FROM semester_project.user_to_bm WHERE user_id = @userId;";

        using (var conn = _dataSource.OpenConnection())
        {
            try
            {
                var bmId = conn.QueryFirstOrDefault<int>(sqlGetBmId, new { userId });
                return conn.QueryFirst<Budget>(sqlUpdate, new { bmId, newCurrentAmount });
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while executing the sql query");
                throw;
            }
        }
    }

    public Budget GetStartAmount(int userId)
    {
        const string sqlBM = $@"SELECT bm_id FROM semester_project.user_to_bm WHERE user_id = @userId;";
        const string sqlStartAmount = $@"SELECT * FROM semester_project.budget_management WHERE bm_id = @bmId;";
        using (var conn = _dataSource.OpenConnection())
        {
            var bmId = conn.QueryFirstOrDefault<int>(sqlBM, new { userId });

            if (bmId != 0)
            {
                return conn.QueryFirst<Budget>(sqlStartAmount, new { bmId });
            }
            else
            {
                throw new Exception("No matching bm_id found for the given user_id");
            }
        }
    }

    
    /*
    public Budget UpdateStartAmount(int userId, float updatedStartAmount)
    {
        const string sqlUpdate = $@"UPDATE semester_project.budget_management SET start_amount = @updatedStartAmount WHERE user_id = @userId;";
        //const string sqlSel = $@"SELECT * from semester_project.budget_management where user_id = @userId;";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<Budget>(sqlUpdate, new
            {
                userId = userId,
                updatedStartAmount = updatedStartAmount
            });
            
            
        }
    }
    */
    
    public Budget UpdateStartAmount(int userId, float updatedStartAmount)
    {
        const string sqlUpdate = $@"UPDATE semester_project.budget_management SET start_amount = @updatedStartAmount WHERE bm_id = @bmId RETURNING *;";
        const string sqlBmId = $@"SELECT bm_id FROM semester_project.user_to_bm WHERE user_id = @userId;";

        using (var conn = _dataSource.OpenConnection())
        {
            var bmId = conn.QueryFirstOrDefault<int>(sqlBmId, new { userId });

            if (bmId != 0)
            {
                return conn.QueryFirst<Budget>(sqlUpdate, new { bmId, updatedStartAmount });
            }
            else
            {
                throw new Exception("No matching bm_id found for the given user_id");
            }
        }
    }
    
}