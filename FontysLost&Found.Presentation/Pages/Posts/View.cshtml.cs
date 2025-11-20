using BusinessLogicLayer.Services;
using BusinessLogicLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FontysLost_Found.Presentation.Pages.Posts
{
    [BindProperties]
    public class ViewModel : PageModel
    {
        private readonly PostService _postService;
        public List<Post> Posts { get; private set; } = new();
        public ViewModel(PostService postService)
        {
            _postService = postService;
        }   
        public async Task OnGet()
        {
            Posts =  await _postService.GetAllAsync();
            
        }
    }
}
