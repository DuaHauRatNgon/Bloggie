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
        public IActionResult Add(AddTagRequest addTagRequest) {
            var tag = new Tag {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };
            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();
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
        public IActionResult Edit(EditTagRequest editTagRequest) {
            var tagTmp = new Tag {
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existTag = bloggieDbContext.Tags.Find(editTagRequest.Id);
            if (existTag != null) {
                existTag.Name = tagTmp.Name;
                existTag.DisplayName = tagTmp.DisplayName;
                bloggieDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return View("Edit", new { id = existTag.Id });
        }

        [HttpGet]
        public IActionResult Delete(Guid id) {
            var tagFound = bloggieDbContext.Tags.Find(id);
            if (tagFound != null) {
                return View(tagFound);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult ConfirmDelete(Guid id) {
            var tagFound = bloggieDbContext.Tags.Find(id);
            if (tagFound != null) {
                bloggieDbContext.Remove(tagFound);
                bloggieDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("List");
        }
    }
}
