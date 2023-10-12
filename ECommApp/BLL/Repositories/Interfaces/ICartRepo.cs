using BLL.Views;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface ICartRepo
    {
        public CartView ViewCart(int userid);
        public string AddItemById(int userid, int itemid, int quantity);
        public string EmptyCart(int userid);
        public string UpdateCart(int userid, int itemid, int quantity);
        public CartView RemoveCartItem(int userid, int itemid);

        public string CheckOut(int userid, double amount, string mode);

    }
}
