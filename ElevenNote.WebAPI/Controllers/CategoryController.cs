using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private CategoryService CreateCategoryService()
        {
            var UserId = Guid.Parse(User.Identity.GetUserId());
            var CategoryService = new CategoryService(UserId);
            return CategoryService;

        }
        public IHttpActionResult GET()
        {
            CategoryService categoryService = CreateCategoryService();
            var Categories = categoryService.GetCAtegories();
            return Ok(Categories);
        }
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateCategoryService();
            if (!service.CreateCategory(category))
                return InternalServerError();
            return Ok();
        }

        public IHttpActionResult Get(int id)
        {
            CategoryService categoryService = CreateCategoryService();
            var Category = categoryService.GetCategoryById(id);
            return Ok(Category);
        }

        public IHttpActionResult Put(CategoryEdit category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Category = CreateCategoryService();
            if (!Category.UpdateCategory(category))
                return InternalServerError();
            
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var category = CreateCategoryService();
            if (!category.DeleteCategory(id))
                return InternalServerError();
            return Ok();
        }
    }
}
