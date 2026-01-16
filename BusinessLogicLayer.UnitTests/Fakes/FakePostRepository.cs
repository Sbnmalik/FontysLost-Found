using BusinessLogicLayer.Abstractions;

namespace BusinessLogicLayer.UnitTests.Fakes
{
    public sealed class FakePostRepository : IPostRepository
    {
        private readonly List<postDto> _store = new();
        private int _nextId = 1;

        public FakePostRepository(IEnumerable<postDto>? seed = null)
        {
            if (seed != null)
            {
                _store.AddRange(seed.Select(Clone));
                _nextId = _store.Count == 0 ? 1 : _store.Max(x => x.Id) + 1;
            }
        }

        public Task<int> InsertAsync(postDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var toSave = Clone(entity);
            toSave.Id = _nextId++;
            _store.Add(toSave);

            return Task.FromResult(toSave.Id);
        }
        public Task<List<postDto>> GetAllAsync()
            => Task.FromResult(_store.Select(Clone).ToList());

        public Task<postDto?> GetByIdAsync(int id)
        {
            var found = _store.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(found == null ? null : Clone(found));
        }

        public Task UpdateAsync(postDto entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var idx = _store.FindIndex(x => x.Id == entity.Id);
            if (idx < 0)
                throw new InvalidOperationException($"Post with Id={entity.Id} not found.");

            _store[idx] = Clone(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var removed = _store.RemoveAll(x => x.Id == id);
            if (removed == 0)
                throw new InvalidOperationException($"Post with Id={id} not found.");

            return Task.CompletedTask;
        }

        //“clone”: so tests don’t accidentally pass because they mutated the same reference in memory.
        private static postDto Clone(postDto x) => new postDto
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            DateCreated = x.DateCreated,
            CategoryId = x.CategoryId,
            Attachment = x.Attachment == null ? null : x.Attachment.ToArray(),
            FinderId = x.FinderId,
            RetrieverId = x.RetrieverId
        };
    }
}
