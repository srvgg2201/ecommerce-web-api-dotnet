using DAL;
using DAL.Models;
using BLL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly DataContext _dbContext;

        public CategoryRepo(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string AddCategory(Category category)
        {
            if (_dbContext.Categories.Where(x => x.Name == category.Name).FirstOrDefault() != null) 
                return "Category " + category.Name + " Already Exists";

            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
            return "Category " + category.Name + " Created Successfully";
        }

        public List<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _dbContext.Categories.Find(id);
        }

        public string RemoveCategory(int categoryid)
        {
            var res = _dbContext.Categories.Where(x => x.Id == categoryid).FirstOrDefault();
            if (res == null) 
                return "Category Does Not Exist";
            _dbContext.Categories.Remove(res);
            _dbContext.SaveChanges();
            return "Category Removed Successfully";
        }

        public string UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
            return "Category Details Updated Successfully";
        }
    }
}
