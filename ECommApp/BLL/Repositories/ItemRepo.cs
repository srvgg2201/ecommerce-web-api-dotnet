using BLL.Views;
using BLL.Repositories.Interfaces;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class ItemRepo : IitemRepo
    {
        private readonly DataContext _dbContext;

        public ItemRepo(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public string AddItem(Item item)
        {
            if(item.Quantity > 0) item.IsAvailable = true;
            else item.IsAvailable = false;
            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();
            return item.Name + " Added Successfully";
        }

        public string DeleteItem(int id)
        {
            var res = _dbContext.Items.Where(x => x.Id == id).FirstOrDefault();
            if(res != null)
            {
                _dbContext.Items.Remove(res);
                _dbContext.SaveChanges();
                return res.Name + " Removed From Store Successfully";
            }
            return "Invalid Item ID/ Item Not Found";
        }

        public List<ItemView> GetAllItems()
        {
            var res = (from item in _dbContext.Items
                       join cat in _dbContext.Categories on item.CategoryId equals cat.Id
                       select new ItemView
                       {
                           Id = item.Id,
                           Name = item.Name,
                           Description = item.Description,
                           Price = item.Price,
                           Quantity = item.Quantity,
                           CategoryId = cat.Id,
                           CategoryName = cat.Name,
                           CreatedDate = item.CreatedDate,
                           IsAvailable = item.IsAvailable,

                       }).ToList();

            return res;    
                

        }

        public List<ItemView> GetItemByCategory(string category)
        {
            var res = (from item in _dbContext.Items
                       join cat in _dbContext.Categories on item.CategoryId equals cat.Id
                       where cat.Name == category
                       select new ItemView
                       {
                           Id = item.Id,
                           Name = item.Name,
                           Description = item.Description,
                           Price = item.Price,
                           Quantity = item.Quantity,
                           CategoryId = cat.Id,
                           CategoryName = cat.Name,
                           CreatedDate = item.CreatedDate,
                           IsAvailable = item.IsAvailable,

                       }).ToList();

            return res;

        }

        public ItemView GetItemById(int id)
        {
            var res = (from item in _dbContext.Items
                       join cat in _dbContext.Categories on item.CategoryId equals cat.Id
                       where item.Id == id
                       select new ItemView
                       {
                           Id = item.Id,
                           Name = item.Name,
                           Description = item.Description,
                           Price = item.Price,
                           Quantity = item.Quantity,
                           CategoryId = cat.Id,
                           CategoryName = cat.Name,
                           CreatedDate = item.CreatedDate,
                           IsAvailable = item.IsAvailable,

                       }).FirstOrDefault();

            return res;
        }

        public List<ItemView> GetItemByName(string name)
        {
            var res = (from item in _dbContext.Items
                       join cat in _dbContext.Categories on item.CategoryId equals cat.Id
                       where item.Name == name
                       select new ItemView
                       {
                           Id = item.Id,
                           Name = item.Name,
                           Description = item.Description,
                           Price = item.Price,
                           Quantity = item.Quantity,
                           CategoryId = cat.Id,
                           CategoryName = cat.Name,
                           CreatedDate = item.CreatedDate,
                           IsAvailable = item.IsAvailable,

                       }).ToList();

            return res;
        }

        public string UpdateItem(Item item)
        {
            if (item.Quantity == 0) item.IsAvailable = false;
            _dbContext.Items.Update(item);
            _dbContext.SaveChanges();
            return "Item Details Updated Successfully";
        }
    }
}
