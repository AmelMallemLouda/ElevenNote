using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;

        public CategoryService (Guid userId)
        {
            _userId = userId;
        }

        //Create an instance of category
        public bool CreateCategory(CategoryCreate model)
        {
            var entity1 = new Category() { OwnerId = _userId, CategoryName = model.Name, CategoryType = model.Type,CreatedUtc=DateTimeOffset.Now };
            using (var ctx=new ApplicationDbContext())
            {
                ctx.Categories.Add(entity1);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CategoryList> GetCAtegories()
        {
            using (var ctx=new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Categories
                    .Where(e => e.OwnerId == _userId)//filter the database
                    .Select(
                        e =>
                        new CategoryList { CategoryId = e.CategoryId, Name = e.CategoryName, CreatedUtc = e.CreatedUtc });
                return query.ToArray();

            }
        }

        public CategoryDetails GetCategoryById(int Id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                      .Categories
                      .Single(e => e.CategoryId == Id && e.OwnerId == _userId);

                return new CategoryDetails
                {
                    CategoryId = entity.CategoryId,
                    CategoryName = entity.CategoryName,
                    CategoryType = entity.CategoryType,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
                    
            }
        }
        public bool UpdateCategory(CategoryEdit model)
        {
            using(var ctx=new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Categories
                   .Single(e => e.CategoryId == model.CategoryId && e.OwnerId == _userId);
                entity.CategoryName = model.Name;
                entity.CategoryType = model.Type;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCategory (int CategoryId)
        {
            using(var ctx= new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Categories
                    .Single(e => e.CategoryId == CategoryId && e.OwnerId == _userId);
                ctx.Categories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
