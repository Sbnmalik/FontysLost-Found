using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogicLayer;
using BusinessLogicLayer.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace FontysLost_Found.Presentation.Pages.Posts

{
    [BindProperties]
    [Authorize]
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
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // Handle file upload and convert to byte[]
            var file = Request.Form.Files["Attachment"];

            if (file != null && file.Length > 0){
                await using var ms = new MemoryStream();
                await file.CopyToAsync(ms);
                input.Attachment = ms.ToArray();
            }
            var id = await _postService.CreateAsync(input);
            TempData["Flash.Success"] = "Lost object post created!";
            return RedirectToPage("Index", new { id });
        }
    }
}
