using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace Bloggie.Web.Controllers {
    public class AdminTagsController : Controller {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagsController(BloggieDbContext bloggieDbContext) {
            this.bloggieDbContext = bloggieDbContext;
        }


        [HttpGet]
        public IActionResult Add() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest) {
            var tag = new Tag {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult List() {
            var tags = bloggieDbContext.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Edit(Guid id) {
            var tag = bloggieDbContext.Tags.SingleOrDefault(t => t.Id == id);
            var editTagRequest = new EditTagRequest {
                Id = id,
                Name = tag.Name,
                DisplayName = tag.DisplayName
            };
            return View(editTagRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest) {
            var tagTmp = new Tag {
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existTag = await bloggieDbContext.Tags.FindAsync(editTagRequest.Id);
            if (existTag != null) {
                existTag.Name = tagTmp.Name;
                existTag.DisplayName = tagTmp.DisplayName;
                await bloggieDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View("Edit", new { id = existTag.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id) {
            var tagFound = await bloggieDbContext.Tags.FindAsync(id);
            if (tagFound != null) {
                return View(tagFound);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDelete(Guid id) {
            var tagFound = await bloggieDbContext.Tags.FindAsync(id);
            if (tagFound != null) {
                bloggieDbContext.Remove(tagFound);
                await bloggieDbContext.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
    }
}
