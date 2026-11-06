using Microsoft.Data.SqlClient;
using System.Data;

namespace MyProject.DAL
{
    public class AuditLogDAL
    {
        public void Insert(int? userId, string eventType, string? description)
        {
            const string sql = @"
                INSERT INTO AuditLogs (UserId, EventType, Description)
                VALUES (@UserId, @EventType, @Description)";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.Add("@UserId", SqlDbType.Int).Value =
                (object?)userId ?? DBNull.Value;
            cmd.Parameters.Add("@EventType", SqlDbType.NVarChar, 50).Value = eventType;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 500).Value =
                (object?)description ?? DBNull.Value;

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}