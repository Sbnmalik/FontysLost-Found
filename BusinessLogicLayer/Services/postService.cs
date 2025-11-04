using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services
{
    public class postService : IPostService
    {
        private readonly IPostRepository _postRepository;
        public postService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<int> CreateAsync(postCreateDTO input)
        {
            var post = new Models.Post
            {
                Title = input.Title,
                Description = input.Description,
                DateCreated = DateTime.UtcNow,
                Attachment = input.Attachment
            };
            return await _postRepository.InsertAsync(post);
        }
        public async Task<postDto?> GetAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return null;
            }

            return new postDto
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                DateCreated = post.DateCreated
            };
        }
    }
}
