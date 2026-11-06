using Microsoft.Data.SqlClient;
using MyProject.Entities;
using System.Data;

namespace MyProject.DAL
{
    public class PasswordHistoryDAL
    {
        public void Add(int userId, string hash, string? salt, int? iterations)
        {
            const string sql = @"
                INSERT INTO PasswordHistories
                (UserId, PasswordHash, PasswordSalt, PasswordIterations)
                VALUES
                (@UserId, @PasswordHash, @PasswordSalt, @PasswordIterations)";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@PasswordHash", SqlDbType.NVarChar, 500).Value = hash;
            cmd.Parameters.Add("@PasswordSalt", SqlDbType.NVarChar, 500).Value =
                (object?)salt ?? DBNull.Value;
            cmd.Parameters.Add("@PasswordIterations", SqlDbType.Int).Value =
                (object?)iterations ?? DBNull.Value;

            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public List<PasswordHistory> GetLast(int userId, int count)
        {
            const string sql = @"
                SELECT TOP (@Count)
                       PasswordHistoryId, UserId, PasswordHash, PasswordSalt,
                       PasswordIterations, CreatedAt
                FROM PasswordHistories
                WHERE UserId = @UserId
                ORDER BY CreatedAt DESC";

            var list = new List<PasswordHistory>();

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add("@Count", SqlDbType.Int).Value = count;
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new PasswordHistory
                {
                    PasswordHistoryId = reader.GetInt64(reader.GetOrdinal("PasswordHistoryId")),
                    UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                    PasswordSalt = reader.IsDBNull(reader.GetOrdinal("PasswordSalt"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("PasswordSalt")),
                    PasswordIterations = reader.IsDBNull(reader.GetOrdinal("PasswordIterations"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("PasswordIterations")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                });
            }

            return list;
        }
    }
}