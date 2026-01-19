using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FontysLost_Found.Presentation.Pages.Posts
{
    [BindProperties]
    [Authorize]
    public class ViewModel : PageModel
    {
        private readonly IPostService _postService;
        public List<Post> Posts { get; private set; } = new();
        public ViewModel(IPostService postService)
        {
            _postService = postService;
        }
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }
        public async Task OnGet()
        {
            var posts = await _postService.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                posts = posts
                    .Where(p =>
                        p.Title.Contains(Search, StringComparison.OrdinalIgnoreCase) ||
                        p.Description.Contains(Search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            Posts = posts;
            Posts = await _postService.GetAllAsync();

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id) 
        {
            await _postService.DeleteAsync(id);
            return RedirectToPage("/Posts/View");
        }
    }
}
