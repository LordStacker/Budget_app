using Dapper;
using infrastructure.DataModels;
using infrastructure.DataSources;
using Npgsql;

namespace infrastructure.Repositories;

public class UserRepository
{
    private IDataSource _dataSource;

    public UserRepository(IDataSource dataSource)
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
            var user = conn.QueryFirst<User>(sql, new
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

            const string sqlBudget = @"
INSERT INTO semester_project.budget_management (start_amount, current_amount)
VALUES (0, 0)
RETURNING bm_id;
";

            var bmId = conn.QueryFirst<int>(sqlBudget);

            const string sqlUserToBm = @"
INSERT INTO semester_project.user_to_bm (user_id, bm_id)
VALUES (@userId, @bmId);
";

            conn.Execute(sqlUserToBm, new { userId = user.Id, bmId });

            return user;
        }
    }
    
    public User? GetById(int id)
    {
        const string sql = $@"
SELECT
    user_id as {nameof(User.Id)},
    user_email as {nameof(User.UserEmail)},
    user_role as {nameof(User.UserRole)},
    username as {nameof(User.Username)},
    firstname as {nameof(User.Firstname)},
    lastname as {nameof(User.Lastname)},
    education as {nameof(User.Education)},
    birth_date as {nameof(User.BirthDate)},
    profile_photo as {nameof(User.ProfilePhoto)}
FROM semester_project.user
WHERE user_id = @id;
";
        using (var conn = _dataSource.OpenConnection())
        {
            return conn.QueryFirstOrDefault<User>(sql, new { id });
        }
    }

    public User? UpdateUser(int userId, string newEmail, string newUsername, string newFirstName, string newLastName, string newEducation, DateTime newBirthDate, string newProfile)
    {
        const string sql = $@"UPDATE semester_project.user 
                    SET user_email = @newEmail,
                        username = @newUsername,
                        firstname = @newFirstName,
                        lastname = @newLastName,
                        education = @newEducation,
                        birth_date = @newBirthDate,
                        profile_photo = @newProfile
                    WHERE user_id = @userId
                    RETURNING  
                        user_id as {nameof(User.Id)},
                        user_email as {nameof(User.UserEmail)},
                        user_role as {nameof(User.UserRole)},
                        username as {nameof(User.Username)},
                        firstname as {nameof(User.Firstname)},
                        lastname as {nameof(User.Lastname)},
                        education as {nameof(User.Education)},
                        birth_date as {nameof(User.BirthDate)},
                        profile_photo as {nameof(User.ProfilePhoto)};";


        
        using (var conn = _dataSource.OpenConnection())
        {
            try
            {
                return conn.QueryFirst<User>(sql, new { userId, newEmail, newUsername, newFirstName, newLastName, newEducation, newBirthDate, newProfile });
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while executing the sql query");
                throw;
            }
        }
    }

    public User? UpdateProfilePhoto(int userId, string image)
    {
        Console.WriteLine("image = " + image);
        const string sql = $@"UPDATE semester_project.user
                SET profile_photo = @image
                WHERE user_id = @userId
                RETURNING
                    user_id as {nameof(User.Id)},
                    user_email as {nameof(User.UserEmail)},
                    user_role as {nameof(User.UserRole)},
                    username as {nameof(User.Username)},
                    firstname as {nameof(User.Firstname)},
                    lastname as {nameof(User.Lastname)},
                    education as {nameof(User.Education)},
                    birth_date as {nameof(User.BirthDate)},
                    profile_photo as {nameof(User.ProfilePhoto)};";

        using (var conn = _dataSource.OpenConnection())
        {
            try
            {
                return conn.QueryFirst<User>(sql, new { userId, image });
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while executing the sql query");
                throw;
            }
        }
    }
    
    public void DeleteUser(int userId)
    {
        const string sql = $@"
        DELETE FROM semester_project.password_hash
        WHERE user_id = @userId;
        
        DELETE FROM semester_project.user_to_bm
        WHERE user_id = @userId;
        
        DELETE FROM semester_project.budget_management
        WHERE bm_id = @userId;
        
        DELETE FROM semester_project.user
        WHERE user_id = @userId;";

        using (var conn = _dataSource.OpenConnection())
        {
            try
            {
                conn.Execute(sql, new { userId });
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while executing the SQL query for deleting the user and related records");
                throw;
            }
        }
    }

}