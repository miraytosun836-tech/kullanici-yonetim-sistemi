using Microsoft.Data.SqlClient;
using System.Data;
using MyProject.Entities;

namespace MyProject.DAL
{
    public class PasswordResetTokenDAL
    {
        public void Insert(int userId, string tokenHash, DateTime expiresAt)
        {
            const string sql = @"
                INSERT INTO PasswordResetTokens (UserId, TokenHash, ExpiresAt)
                VALUES (@UserId, @TokenHash, @ExpiresAt)";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@TokenHash", SqlDbType.Char, 64).Value = tokenHash;
            cmd.Parameters.Add("@ExpiresAt", SqlDbType.DateTime2).Value = expiresAt;

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public PasswordResetToken? GetValid(string tokenHash)
        {
            const string sql = @"
        SELECT TokenId, UserId, TokenHash, ExpiresAt, UsedAt, CreatedAt
        FROM PasswordResetTokens
        WHERE TokenHash = @TokenHash
          AND UsedAt IS NULL
          AND ExpiresAt > SYSDATETIME()";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add("@TokenHash", SqlDbType.Char, 64).Value = tokenHash;

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new PasswordResetToken
            {
                TokenId = reader.GetInt64(reader.GetOrdinal("TokenId")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                TokenHash = reader.GetString(reader.GetOrdinal("TokenHash")),
                ExpiresAt = reader.GetDateTime(reader.GetOrdinal("ExpiresAt")),
                UsedAt = reader.IsDBNull(reader.GetOrdinal("UsedAt"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("UsedAt")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
            };
        }

        public void MarkUsed(long tokenId)
        {
            const string sql = @"
        UPDATE PasswordResetTokens
        SET UsedAt = SYSDATETIME()
        WHERE TokenId = @TokenId";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add("@TokenId", SqlDbType.BigInt).Value = tokenId;

            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void InvalidateOthers(int userId, long tokenId)
        {
            const string sql = @"
        UPDATE PasswordResetTokens
        SET UsedAt = SYSDATETIME()
        WHERE UserId = @UserId
          AND UsedAt IS NULL
          AND TokenId <> @TokenId";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
            cmd.Parameters.Add("@TokenId", SqlDbType.BigInt).Value = tokenId;

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}