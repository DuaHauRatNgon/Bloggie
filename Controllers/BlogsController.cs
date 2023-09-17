using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers {
    public class BlogsController : Controller {
        private readonly IBlogPostRepository blogPostRepository;
        public BlogsController(IBlogPostRepository blogPostRepository) {
            this.blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string UrlHandle) {
            var blogPost = await blogPostRepository.GetByUrlHandleAsync(UrlHandle);
            return View(blogPost);
        }
    }
}
