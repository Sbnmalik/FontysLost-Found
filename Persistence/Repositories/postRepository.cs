using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;



namespace Persistence.Repositories
{
    public class postRepository : IPostRepository
    {
        private readonly string _connectionString;
        public postRepository(IConfiguration cfg)
        {
            _connectionString = cfg.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task<int> InsertAsync(Post entity)
        {
            const string sql = @"
            INSERT INTO dbo.Posts
            (Title, Description, date_created, categoryId, Attachment, finderId, retrieverId)
            OUTPUT INSERTED.Id
            VALUES (@Title, @Description, @DateCreated, @CategoryId, @Attachment, @FinderId, @RetrieverId);";
            await using var conn = new SqlConnection(_connectionString);
            await conn .OpenAsync();

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 20) { Value = entity.Title });
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 50) { Value = entity.Description });
            cmd.Parameters.Add(new SqlParameter("@DateCreated", SqlDbType.DateTime) { Value = entity.DateCreated });
            //cmd.Parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = entity.CategoryId });
            cmd.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.VarBinary, -1) { Value = (object?)entity.Attachment ?? DBNull.Value }); 
            //cmd.Parameters.Add(new SqlParameter("@FinderId", SqlDbType.Int) { Value = entity.FinderId });
            //cmd.Parameters.Add(new SqlParameter("@RetrieverId", SqlDbType.Int) { Value = (object?) entity.RetrieverId ?? DBNull.Value });

            var newId = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(newId);
        }
        public Task<Post?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
