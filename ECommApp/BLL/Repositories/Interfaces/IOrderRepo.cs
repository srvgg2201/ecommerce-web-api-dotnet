using BLL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface IOrderRepo
    {
        public List<OrderView> GetAllOrders();
        public OrderView GetByOrderId(int orderid);
        public List<OrderView> GetOrderByUserId(int userid);
    }
}
