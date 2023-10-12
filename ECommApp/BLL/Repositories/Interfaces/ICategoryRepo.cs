using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface ICategoryRepo
    {
        public string AddCategory(Category category);
        public List<Category> GetCategories();

        public string RemoveCategory(int categoryid);
        public Category GetCategoryById(int id);
        public string UpdateCategory(Category category);

    }
}
