using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer;

namespace FontysLost_Found.Presentation.Pages.Posts
{
    public class EditModel : PageModel
    {
        private readonly PostService _postService;
        public EditModel(PostService postService)
        {
            _postService = postService;
        }
        [BindProperty]
        public postUpdatedDto Post { get; set; } = default;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var existing = await _postService.OnGetAsync(id); // whatever your "get one" returns
            if (existing == null)
                return NotFound();

            // Map from whatever your GetAsync returns into PostUpdateDTO
            Post = new postUpdatedDto
            {
                Id = existing.Id,
                Title = existing.Title,
                Description = existing.Description,
                CategoryId = existing.CategoryId,
                Attachment = existing.Attachment,
                FinderId = existing.FinderId,
                RetrieverId = existing.RetrieverId
            };

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _postService.UpdateAsync(Post);
            return RedirectToPage("./Index");
        }
    }
}
