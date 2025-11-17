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
