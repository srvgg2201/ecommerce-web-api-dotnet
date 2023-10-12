using BLL.Views;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface IitemRepo
    {
        public List<ItemView> GetAllItems();
        public ItemView GetItemById(int id);
        public List<ItemView> GetItemByName(string name);
        public List<ItemView> GetItemByCategory(string category);
        public string AddItem(Item item);
        public string UpdateItem(Item item);
        public string DeleteItem(int id);
    }
}
