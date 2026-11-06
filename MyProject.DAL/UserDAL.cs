using Microsoft.Data.SqlClient;
using MyProject.Entities;
using System.Data;

namespace MyProject.DAL
{
    public class UserDAL
    {
        public User? GetByEmail(string email)
        {
            const string sql = @"
    SELECT u.UserId, u.Email, u.FirstName, u.LastName, u.BirthDate,
           u.PasswordHash, u.PasswordSalt, u.PasswordIterations, u.RoleId,
           u.IsActive, u.MustChangePassword, u.CreatedAt, u.CreatedBy,
           r.RoleName
    FROM Users u
    INNER JOIN Roles r ON r.RoleId = u.RoleId
    WHERE u.Email = @Email";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = email;

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            return Map(reader);
        }
        public User? GetById(int userId)
        {
            const string sql = @"
        SELECT u.UserId, u.Email, u.FirstName, u.LastName, u.BirthDate,
               u.PasswordHash, u.PasswordSalt, u.PasswordIterations, u.RoleId,
               u.IsActive, u.MustChangePassword, u.CreatedAt, u.CreatedBy,
               r.RoleName
        FROM Users u
        INNER JOIN Roles r ON r.RoleId = u.RoleId
        WHERE u.UserId = @UserId";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read())
                return null;

            return Map(reader);
        }

        private static User Map(SqlDataReader reader)
        {
            return new User
            {
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                BirthDate = reader.IsDBNull(reader.GetOrdinal("BirthDate"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                PasswordSalt = reader.IsDBNull(reader.GetOrdinal("PasswordSalt"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("PasswordSalt")),
                PasswordIterations = reader.IsDBNull(reader.GetOrdinal("PasswordIterations"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("PasswordIterations")),
                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                MustChangePassword = reader.GetBoolean(reader.GetOrdinal("MustChangePassword")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                    RoleName = reader.GetString(reader.GetOrdinal("RoleName"))
            }; 
        }
        public int InsertUser(User user)
        {
            const string sql = @"
        INSERT INTO Users
        (
            Email, FirstName, LastName, BirthDate,
            PasswordHash, PasswordSalt, PasswordIterations,
            RoleId, IsActive, MustChangePassword, CreatedBy
        )
        VALUES
        (
            @Email, @FirstName, @LastName, @BirthDate,
            @PasswordHash, @PasswordSalt, @PasswordIterations,
            @RoleId, @IsActive, @MustChangePassword, @CreatedBy
        );
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = user.Email;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 60).Value = user.FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 60).Value = user.LastName;
            cmd.Parameters.Add("@BirthDate", SqlDbType.Date).Value =
                user.BirthDate.HasValue ? user.BirthDate.Value : DBNull.Value;
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 500).Value = user.PasswordHash;
            cmd.Parameters.Add("@PasswordSalt", SqlDbType.NVarChar, 500).Value =
                (object?)user.PasswordSalt ?? DBNull.Value;
            cmd.Parameters.Add("@PasswordIterations", SqlDbType.Int).Value =
                (object?)user.PasswordIterations ?? DBNull.Value;
            cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = user.RoleId;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = user.IsActive;
            cmd.Parameters.Add("@MustChangePassword", SqlDbType.Bit).Value = user.MustChangePassword;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value =
                (object?)user.CreatedBy ?? DBNull.Value;

            connection.Open();
            return (int)cmd.ExecuteScalar();
        }
        public List<User> GetAll()
        {
            const string sql = @"
        SELECT u.UserId, u.Email, u.FirstName, u.LastName, u.BirthDate,
               u.RoleId, u.IsActive, u.MustChangePassword, u.CreatedAt, u.CreatedBy,
               r.RoleName
        FROM Users u
        INNER JOIN Roles r ON r.RoleId = u.RoleId
        ORDER BY u.Email";

            var list = new List<User>();

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new User
                {
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    BirthDate = reader.IsDBNull(reader.GetOrdinal("BirthDate"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("BirthDate")),
                    RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    MustChangePassword = reader.GetBoolean(reader.GetOrdinal("MustChangePassword")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                    RoleName = reader.GetString(reader.GetOrdinal("RoleName"))
                });
            }

            return list;
        }

        public void UpdatePassword(int userId, string hash, string salt, int iterations, bool mustChangePassword)
        {
            const string sql = @"
                UPDATE Users
                SET PasswordHash = @PasswordHash,
                    PasswordSalt = @PasswordSalt,
                    PasswordIterations = @PasswordIterations,
                    MustChangePassword = @MustChangePassword
                WHERE UserId = @UserId";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 500).Value = hash;
            cmd.Parameters.Add("@PasswordSalt", SqlDbType.NVarChar, 500).Value = salt;
            cmd.Parameters.Add("@PasswordIterations", SqlDbType.Int).Value = iterations;
            cmd.Parameters.Add("@MustChangePassword", SqlDbType.Bit).Value = mustChangePassword;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void SetActive(int userId, bool isActive)
        {
            const string sql = @"
        UPDATE Users
        SET IsActive = @IsActive
        WHERE UserId = @UserId";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = isActive;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void UpdateRole(int userId, int roleId)
        {
            const string sql = @"
        UPDATE Users
        SET RoleId = @RoleId
        WHERE UserId = @UserId";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public void SavePasswordChange(
    int userId,
    string oldHash,
    string? oldSalt,
    int? oldIterations,
    string newHash,
    string newSalt,
    int newIterations,
    long? usedTokenId)
        {
            using SqlConnection connection = DbConnectionFactory.Create();
            connection.Open();
            using SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                using (SqlCommand history = new SqlCommand(@"
            INSERT INTO PasswordHistories
            (UserId, PasswordHash, PasswordSalt, PasswordIterations)
            VALUES (@UserId, @OldHash, @OldSalt, @OldIterations)", connection, transaction))
                {
                    history.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    history.Parameters.Add("@OldHash", SqlDbType.NVarChar, 500).Value = oldHash;
                    history.Parameters.Add("@OldSalt", SqlDbType.NVarChar, 500).Value =
                        (object?)oldSalt ?? DBNull.Value;
                    history.Parameters.Add("@OldIterations", SqlDbType.Int).Value =
                        (object?)oldIterations ?? DBNull.Value;
                    history.ExecuteNonQuery();
                }

                using (SqlCommand update = new SqlCommand(@"
            UPDATE Users
            SET PasswordHash = @NewHash,
                PasswordSalt = @NewSalt,
                PasswordIterations = @NewIterations,
                MustChangePassword = 0
            WHERE UserId = @UserId", connection, transaction))
                {
                    update.Parameters.Add("@NewHash", SqlDbType.NVarChar, 500).Value = newHash;
                    update.Parameters.Add("@NewSalt", SqlDbType.NVarChar, 500).Value = newSalt;
                    update.Parameters.Add("@NewIterations", SqlDbType.Int).Value = newIterations;
                    update.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                    update.ExecuteNonQuery();
                }

                if (usedTokenId.HasValue)
                {
                    using (SqlCommand mark = new SqlCommand(@"
                UPDATE PasswordResetTokens
                SET UsedAt = SYSDATETIME()
                WHERE TokenId = @TokenId", connection, transaction))
                    {
                        mark.Parameters.Add("@TokenId", SqlDbType.BigInt).Value = usedTokenId.Value;
                        mark.ExecuteNonQuery();
                    }

                    using (SqlCommand others = new SqlCommand(@"
                UPDATE PasswordResetTokens
                SET UsedAt = SYSDATETIME()
                WHERE UserId = @UserId
                  AND UsedAt IS NULL
                  AND TokenId <> @TokenId", connection, transaction))
                    {
                        others.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                        others.Parameters.Add("@TokenId", SqlDbType.BigInt).Value = usedTokenId.Value;
                        others.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}