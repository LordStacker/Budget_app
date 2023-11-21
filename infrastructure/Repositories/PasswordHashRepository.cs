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
    semester_project.password_hash.user_id as {nameof(PasswordHash.UserId)},
    semester_project.password_hash.hash as {nameof(PasswordHash.Hash)},
    semester_project.password_hash.salt as {nameof(PasswordHash.Salt)},
    semester_project.password_hash.algorithm as {nameof(PasswordHash.Algorithm)}
FROM semester_project.password_hash
JOIN semester_project.user ON semester_project.password_hash.user_id = semester_project.user.user_id
WHERE semester_project.user.user_email = @email;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<PasswordHash>(sql, new { email });
        }
    }

    public void Create(int userId, string hash, string salt, string algorithm)
    {
        const string sql = $@"
INSERT INTO semester_project.password_hash (user_id, hash, salt, algorithm)
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
UPDATE semester_project.password_hash
SET hash = @hash, salt = @salt, algorithm = @algorithm
WHERE user_id = @userId
";
        using (var conn = _dataSource.OpenConnection())
        {
            conn.Execute(sql, new { userId, hash, salt, algorithm });
        }
    }
}