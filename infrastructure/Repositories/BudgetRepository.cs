using Dapper;
using infrastructure.DataModels;
using infrastructure.DataSources;
using Npgsql;

namespace infrastructure.Repositories;

public class BudgetRepository
{
    private IDataSource _dataSource;

    public BudgetRepository(IDataSource dataSource)
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
                Console.WriteLine("An error occured while executing the sql query "+ e);
                throw;
            }
        }
    }

    public Budget UpdateCurrentAmount(int userId, float newCurrentAmount)
    {
        const string sqlUpdate =
            $@"UPDATE semester_project.budget_management SET current_amount = @newCurrentAmount WHERE bm_id = @bmId
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
    public Budget UpdateStartAmount(int userId, float updatedStartAmount)
    {
        const string sqlUpdate =
            $@"UPDATE semester_project.budget_management SET start_amount = @updatedStartAmount, current_amount = @updatedStartAmount WHERE bm_id = @bmId RETURNING bm_id as {nameof(Budget.Id)}, start_amount as {nameof(Budget.StartAmount)},
                                            current_amount as {nameof(Budget.CurrentAmount)}";
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

    public IEnumerable<Transaction> GetTransactions(int userId)
    {
        const string sqlBmId = $@"SELECT bm_id FROM semester_project.user_to_bm WHERE user_id = @userId;";
        const string sql = $@"SELECT transaction_id as {nameof(Transaction.id)}, item_name as {nameof(Transaction.ItemName)}, item_amount as {nameof(Transaction.ItemAmount)}, total_cost as {nameof(Transaction.TotalCost)} FROM semester_project.transaction where bm_id= @bmId";
        using (var conn = _dataSource.OpenConnection())
        {
            var bmId = conn.QueryFirstOrDefault<int>(sqlBmId, new { userId });
            if (bmId != 0 && bmId != null)
            {
                return conn.Query<Transaction>(sql, new { bmId });
            }
            else
            {
                throw new Exception("No matching bm_id found for the given user_id");
            }

        }
    }

    public Transaction PostTransactions(int userId, int ItemAmount, string ItemName, float TotalCost)
    { 
        const string sqlBmId = $@"SELECT bm_id FROM semester_project.user_to_bm WHERE user_id = @userId;";
        const string sql = $@"
            INSERT INTO semester_project.transaction (item_name, item_amount, total_cost, bm_id) 
            VALUES (@ItemName, @ItemAmount, @TotalCost, @bm_id)
            RETURNING
    item_name as {nameof(Transaction.ItemName)},
    item_amount as {nameof(Transaction.ItemAmount)},
    total_cost as {nameof(Transaction.TotalCost)};
";
        using (var conn = _dataSource.OpenConnection())
        {
            var bmId = conn.QueryFirstOrDefault<int>(sqlBmId, new { userId });
            if (bmId != 0 && bmId != null)
            {
                Console.WriteLine((sql, new { ItemName, ItemAmount, TotalCost, bmId }));
                return conn.QueryFirst<Transaction>(sql, new { ItemName, ItemAmount, TotalCost, bm_id = bmId });
            }
            else
            {
                throw new Exception("No matching bm_id found for the given user_id");
            }
        }
    }
    
    public Transaction UpdateTransactions(int userId,int id,int ItemAmount, string ItemName, float TotalCost)
    { 
        const string sqlBmId = $@"SELECT bm_id FROM semester_project.user_to_bm WHERE user_id = @userId;";
        const string sql = $@"
    UPDATE semester_project.transaction 
    SET 
        item_name = @ItemName, 
        item_amount = @ItemAmount, 
        total_cost = @TotalCost 
    WHERE 
        bm_id = @bm_id and
        transaction_id = @transaction_id
    RETURNING
        item_name as {nameof(Transaction.ItemName)},
        item_amount as {nameof(Transaction.ItemAmount)},
        total_cost as {nameof(Transaction.TotalCost)};
";

        using (var conn = _dataSource.OpenConnection())
        {
            var bmId = conn.QueryFirstOrDefault<int>(sqlBmId, new { userId });
            if (bmId != 0 && bmId != null)
            {
                Console.WriteLine((sql, new { ItemName, ItemAmount, TotalCost,transaction_id = id, bm_id = bmId }));
                return conn.QueryFirst<Transaction>(sql, new { ItemName, ItemAmount, TotalCost,transaction_id = id, bm_id = bmId });
            }
            else
            {
                throw new Exception("No matching bm_id found for the given user_id");
            }
        }
    }

    public void DeleteTransaction(int userId, int id)
    {
        const string sqlBmId = "SELECT bm_id FROM semester_project.user_to_bm WHERE user_id = @userId;";
        const string sql = @"
        DELETE FROM semester_project.transaction 
        WHERE 
            bm_id = @bm_id AND
            transaction_id = @transaction_id;
    ";

        using (var conn = _dataSource.OpenConnection())
        {
            var bmId = conn.QueryFirstOrDefault<int>(sqlBmId, new { userId });
            if (bmId != 0 && bmId != null)
            {
                conn.Execute(sql, new { transaction_id = id, bm_id = bmId });
            }
            else
            {
                throw new Exception("No matching bm_id found for the given user_id");
            }
        }
    }
}