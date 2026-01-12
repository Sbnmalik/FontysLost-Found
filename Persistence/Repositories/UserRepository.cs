using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Persistence.Data_Transfer_Objects;
using System.Data;

namespace Persistence.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository(IConfiguration cfg)
        {
            _connectionString = cfg.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task<UserDTO?> GetByUsernameAsync(string userName)
        {
            const string sql = @"
            SELECT Id, UserName, Email, PasswordHash, PasswordSalt
            FROM dbo.Users
            WHERE UserName = @UserName;";

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = userName });

            await using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null;

            return new UserDTO
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                PasswordHash = (byte[])reader["PasswordHash"],
                PasswordSalt = (byte[])reader["PasswordSalt"]
            };
        }
        public async Task<int> InsertAsync(UserDTO user)
        {
            const string sql = @"
            INSERT INTO dbo.Users (UserName, Email, PasswordHash, PasswordSalt)
            VALUES (@UserName, @Email, @PasswordHash, @PasswordSalt);
            SELECT SCOPE_IDENTITY();";
            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 50) { Value = user.UserName });
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = user.Email });
            cmd.Parameters.Add(new SqlParameter("@PasswordHash", SqlDbType.VarBinary, 256) { Value = user.PasswordHash });
            cmd.Parameters.Add(new SqlParameter("@PasswordSalt", SqlDbType.VarBinary, 128) { Value = user.PasswordSalt });
            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
    }
}
