using Microsoft.Data.SqlClient;
using MyProject.Entities;
using System.Data;

namespace MyProject.DAL
{
    public class RoleDAL
    {
        public Role? GetByName(string roleName)
        {
            const string sql = @"
                SELECT RoleId, RoleName
                FROM Roles
                WHERE RoleName = @RoleName";

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.Add("@RoleName", SqlDbType.NVarChar, 30).Value = roleName;

            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return new Role
            {
                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                RoleName = reader.GetString(reader.GetOrdinal("RoleName"))
            };
        }
        public List<Role> GetAll()
        {
            const string sql = "SELECT RoleId, RoleName FROM Roles ORDER BY RoleName";
            var list = new List<Role>();

            using SqlConnection connection = DbConnectionFactory.Create();
            using SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new Role
                {
                    RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                    RoleName = reader.GetString(reader.GetOrdinal("RoleName"))
                });
            }

            return list;
        }
    }
}