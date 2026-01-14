using BusinessLogicLayer.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace FontysLost_Found.Presentation.Pages.Posts
{
    public class DetailsModel : PageModel
    {
        private readonly PostService _postService;
        public DetailsModel(PostService postService)
        {
            _postService = postService;
        }
        public Post? Post { get; private set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Post = await _postService.GetByIdAsync(id);
            if (Post == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
