using BusinessLogicLayer.Models;
using Persistence;
using Persistence.Repositories;

namespace BusinessLogicLayer.Services
{
    public class PostService
    { 
        private readonly PostRepository _postRepository;
        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<int> CreateAsync(postCreateDTO input)
        {


            var dto = new postDto
            {
                Title = input.Title,
                Description = input.Description,
                DateCreated = DateTime.UtcNow,
                Attachment = input.Attachment,
            };
            return await _postRepository.InsertAsync(dto);
        }
        public async Task<Post?> OnGetAsync(int id)
        {
            var dtos = await _postRepository.GetAllAsync();
            var dto = dtos.FirstOrDefault(d => d.Id == id);
            if (dto == null)
                return null;
            var post = new Post
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                DateCreated = dto.DateCreated,
                CategoryId = dto.CategoryId,
                Attachment = dto.Attachment,
                FinderId = dto.FinderId,
                RetrieverId = dto.RetrieverId
            };
            return post;
        }
        public async Task<List<Post>> GetAllAsync()
        {
            var dtos = await _postRepository.GetAllAsync();

            var posts = dtos.Select(d => new Post
            {
                Id = d.Id,
                Title = d.Title,
                Description = d.Description,
                DateCreated = d.DateCreated,
                CategoryId = d.CategoryId,
                Attachment = d.Attachment,
                FinderId = d.FinderId,
                RetrieverId = d.RetrieverId
            }).ToList();

            return posts;
        }
        public async Task UpdateAsync(postUpdatedDto input)
        {
            var dto = new postDto
            {
                Id = input.Id,
                Title = input.Title,
                Description = input.Description,
                CategoryId = input.CategoryId,
                Attachment = input.Attachment,
                FinderId = input.FinderId,
                RetrieverId = input.RetrieverId
            };
            await _postRepository.UpdateAsync(dto);
        }
    }
}
