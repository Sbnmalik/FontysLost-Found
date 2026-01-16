using BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.UnitTests.Fakes
{
    public class PostServiceTests
    {
        [Fact]
        public async Task CreateAsync_ValidInput_ReturnsId_AndPersistsPost()
        {
            // Arrange
            var repo = new FakePostRepository();
            var service = new PostService(repo);

            var input = new postCreateDTO
            {
                Title = "Wallet",
                Description = "Black leather wallet"
            };

            // Act
            var id = await service.CreateAsync(input);
            var saved = await repo.GetByIdAsync(id);

            // Assert
            Assert.True(id > 0);
            Assert.NotNull(saved);
            Assert.Equal("Wallet", saved!.Title);
            Assert.Equal("Black leather wallet", saved.Description);
            Assert.True(saved.DateCreated <= DateTime.UtcNow);
        }
        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsPost()
        {
            // Arrange
            var repo = new FakePostRepository(new[]
            {
                new postDto
                {
                    Id = 10,
                    Title = "Bag",
                    Description = "Grey backpack",
                    DateCreated = DateTime.UtcNow
                }
            });

            var service = new PostService(repo);

            // Act
            var post = await service.GetByIdAsync(10);

            // Assert
            Assert.NotNull(post);
            Assert.Equal(10, post!.Id);
            Assert.Equal("Bag", post.Title);
        }
        [Fact]
        public async Task UpdateAsync_ExistingPost_UpdatesData()
        {
            // Arrange
            var repo = new FakePostRepository();
            var service = new PostService(repo);

            var id = await repo.InsertAsync(new postDto
            {
                Title = "Card",
                Description = "Student card",
                DateCreated = DateTime.UtcNow
            });

            var update = new postUpdatedDto
            {
                Id = id,
                Title = "Updated Card",
                Description = "Updated description",
                CategoryId = 2
            };

            // Act
            await service.UpdateAsync(update);
            var updated = await repo.GetByIdAsync(id);

            // Assert
            Assert.NotNull(updated);
            Assert.Equal("Updated Card", updated!.Title);
            Assert.Equal("Updated description", updated.Description);
            Assert.Equal(2, updated.CategoryId);
        }
        [Fact]
        public async Task DeleteAsync_ExistingPost_RemovesPost()
        {
            // Arrange
            var repo = new FakePostRepository();
            var service = new PostService(repo);

            var id = await repo.InsertAsync(new postDto
            {
                Title = "Bottle",
                Description = "Water bottle",
                DateCreated = DateTime.UtcNow
            });

            // Act
            await service.DeleteAsync(id);
            var deleted = await repo.GetByIdAsync(id);

            // Assert
            Assert.Null(deleted);
        }
    }
}
