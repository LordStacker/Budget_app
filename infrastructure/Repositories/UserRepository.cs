using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure.Repositories;

public class UserRepository
{
    private NpgsqlDataSource _dataSource;

    public UserRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public User Create(string user_email, string hashed , int user_role, string user_name, string first_name, string last_name, string edu, DateTime birth_date, string profile_photo)
    {
        const string sql = $@"
INSERT INTO semester_project.user (user_email, hash, user_role, username, firstname, lastname, education, birth_date, profile_photo)
VALUES (@userEmail,@hash , @userRole, @username, @firstname, @lastname, @education, @birthDate, @profilePhoto)
RETURNING
    user_id as {nameof(User.Id)},
    user_email as {nameof(User.UserEmail)},
    user_role as {nameof(User.UserRole)},
    username as {nameof(User.Username)},
    firstname as {nameof(User.Firstname)},
    lastname as {nameof(User.Lastname)},
    education as {nameof(User.Education)},
    birth_date as {nameof(User.BirthDate)},
    profile_photo as {nameof(User.ProfilePhoto)}
    ;
";

        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new
            {
                userEmail = user_email,
                hash = hashed, 
                userRole = user_role,
                username = user_name, 
                firstname = first_name,
                lastname = last_name,
                education = edu,
                birthDate = birth_date,
                profilePhoto = profile_photo,
                
            });
        }

    }
    
    public User? GetById(int id)
    {
        const string sql = $@"
SELECT
    *
FROM semester_project.user
WHERE user_id = @id;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<User>(sql, new { id });
        }
    }

/*
    public User Update(int id, string fullName, string email, string? avatarUrl, bool admin = false)
    {
        const string sql = $@"
UPDATE users SET full_name = @fullName, email = @email, avatar_url = @avatarUrl, admin = @admin
WHERE id = @id
RETURNING
    id as {nameof(User.Id)},
    full_name as {nameof(User.FullName)},
    email as {nameof(User.Email)},
    avatar_url as {nameof(User.AvatarUrl)},
    admin as {nameof(User.IsAdmin)}
    ;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirst<User>(sql, new { id, fullName, email, avatarUrl, admin });
        }
    }

    
    public IEnumerable<User> GetByIds(IEnumerable<int> ids)
    {
        const string sql = $@"
SELECT
    id as {nameof(User.Id)},
    full_name as {nameof(User.FullName)},
    email as {nameof(User.Email)},
    avatar_url as {nameof(User.AvatarUrl)},
    admin as {nameof(User.IsAdmin)}
FROM users
WHERE id IN @ids;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<User>(sql, new { ids });
        }
    }

    public IEnumerable<User> GetAll()
    {
        const string sql = $@"
SELECT
    id as {nameof(User.Id)},
    full_name as {nameof(User.FullName)},
    email as {nameof(User.Email)},
    avatar_url as {nameof(User.AvatarUrl)},
    admin as {nameof(User.IsAdmin)}
FROM users
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.Query<User>(sql);
        }
    }

    public int Count()
    {
        const string sql = $@"
SELECT count(*)
FROM users
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.ExecuteScalar<int>(sql);
        }
    }
    */
}