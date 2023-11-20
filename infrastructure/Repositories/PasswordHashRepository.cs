using Dapper;
using infrastructure.DataModels;
using Npgsql;


namespace infrastructure.Repositories;

public class PasswordHashRepository
{
    
    private NpgsqlDataSource _dataSource;

    public PasswordHashRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public PasswordHash GetByEmail(string email)
    {
        const string sql = $@"
SELECT 
    user_id as {nameof(PasswordHash.UserId)},
    hash as {nameof(PasswordHash.Hash)},
    salt as {nameof(PasswordHash.Salt)},
    algorithm as {nameof(PasswordHash.Algorithm)}
FROM password_hash
JOIN users ON password_hash.user_id = users.id
WHERE email = @email;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<PasswordHash>(sql, new { email });
        }
    }

    public void Create(int userId, string hash, string salt, string algorithm)
    {
        const string sql = $@"
INSERT INTO password_hash (user_id, hash, salt, algorithm)
VALUES (@userId, @hash, @salt, @algorithm)
";
        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new { userId, hash, salt, algorithm });
        }
    }

    public void Update(int userId, string hash, string salt, string algorithm)
    {
        const string sql = $@"
UPDATE password_hash
SET hash = @hash, salt = @salt, algorithm = @algorithm
WHERE user_id = @userId
";
        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new { userId, hash, salt, algorithm });
        }
    }
}