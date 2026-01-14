using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FontysLost_Found.Presentation.Pages.Posts
{
    [BindProperties]
    public class ViewModel : PageModel
    {
        private readonly IPostService _postService;
        public List<Post> Posts { get; private set; } = new();
        public ViewModel(IPostService postService)
        {
            _postService = postService;
        }
        public async Task OnGet()
        {
            Posts = await _postService.GetAllAsync();

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id) 
        {
            await _postService.DeleteAsync(id);
            return RedirectToPage("/Posts/View");
        }
    }
}
