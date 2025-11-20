using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;





namespace Persistence.Repositories
{
    public class PostRepository 
    {
        private readonly string _connectionString;
        public PostRepository(IConfiguration cfg)
        {
            _connectionString = cfg.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task<int> InsertAsync(postDto entity)
        {
            const string sql = @"
            INSERT INTO dbo.Posts
            (Title, Description, date_created, categoryId, Attachment, finderId, retrieverId)
            OUTPUT INSERTED.Id
            VALUES (@Title, @Description, @DateCreated, @CategoryId, @Attachment, @FinderId, @RetrieverId);";
            await using var conn = new SqlConnection(_connectionString);
            await conn .OpenAsync();

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 200) { Value = entity.Title });
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 200) { Value = entity.Description });
            cmd.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime) { Value = entity.DateCreated });
            cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = (object)entity.CategoryId ?? DBNull.Value});
            cmd.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.VarBinary, -1) { Value = (object?)entity.Attachment ?? DBNull.Value });
            cmd.Parameters.Add(new SqlParameter("@FinderId", SqlDbType.Int) { Value =(object) entity.FinderId ?? DBNull.Value});
            cmd.Parameters.Add(new SqlParameter("@RetrieverId", SqlDbType.Int) { Value = (object?)entity.RetrieverId ?? DBNull.Value });

            var newId = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(newId);
        }
        public async Task<List<postDto>> GetAllAsync()
        {
            const string sql = @"
                SELECT Id, Title, Description, date_created, categoryId, Attachment, finderId, retrieverId
                FROM dbo.Posts
                ORDER BY date_created DESC;";

            var result = new List<postDto>();

            await using var conn = new SqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new SqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var dto = new postDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                    DateCreated = reader.GetDateTime(reader.GetOrdinal("date_created")),
                    CategoryId = reader.IsDBNull(reader.GetOrdinal("categoryId"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("categoryId")),
                    Attachment = reader.IsDBNull(reader.GetOrdinal("Attachment"))
                        ? null
                        : (byte[])reader["Attachment"],
                    FinderId = reader.IsDBNull(reader.GetOrdinal("finderId"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("finderId")),
                    RetrieverId = reader.IsDBNull(reader.GetOrdinal("retrieverId"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("retrieverId")),
                };

                result.Add(dto);
            }

            return result;
        }
    }
}
