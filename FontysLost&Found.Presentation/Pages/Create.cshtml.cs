using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer.Abstractions;
using BusinessLogicLayer.DTOs;

namespace FontysLost_Found.Presentation.Pages

{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IPostService _postService;
        public CreateModel(IPostService postService)
        {
            _postService = postService;
        }
        [BindProperty]
        public postCreateDTO input { get; set; } = new();
        public Task<IActionResult> OnGetAsync() 
        { 
            return Task.FromResult<IActionResult>(Page());
        }
        public async Task<IActionResult> OnPostAsync([FromForm] string Title)
        {
            if (!ModelState.IsValid) return Page();

            var id = await _postService.CreateAsync(input);
            TempData["Flash.Success"] = "Lost object post created!";
            return RedirectToPage("Details", new { id });
        }
    }
}
