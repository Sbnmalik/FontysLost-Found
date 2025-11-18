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
    }
}
