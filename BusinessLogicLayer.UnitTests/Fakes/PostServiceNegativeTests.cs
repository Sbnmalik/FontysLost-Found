using BusinessLogicLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.UnitTests.Fakes
{
    public class PostServiceNegativeTests
    {
        [Fact]
        public async Task GetByIdAsync_WhenPostDoesNotExist_ReturnsNull()
        {
            // Arrange
            var repo = new FakePostRepository();
            var service = new PostService(repo);

            // Act
            var result = await service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task UpdateAsync_WhenPostDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var repo = new FakePostRepository();
            var service = new PostService(repo);

            var input = new postUpdatedDto
            {
                Id = 999,
                Title = "Does not exist",
                Description = "Invalid update",
                CategoryId = 1
            };

            // Act + Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.UpdateAsync(input));
        }
        [Fact]
        public async Task DeleteAsync_WhenPostDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var repo = new FakePostRepository();
            var service = new PostService(repo);

            // Act + Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(999));
        }
        [Fact]
        public async Task OnGetAsync_WhenPostDoesNotExist_ReturnsNull()
        {
            // Arrange
            var repo = new FakePostRepository();
            var service = new PostService(repo);

            // Act
            var result = await service.OnGetAsync(999);

            // Assert
            Assert.Null(result);
        }
    }
}
