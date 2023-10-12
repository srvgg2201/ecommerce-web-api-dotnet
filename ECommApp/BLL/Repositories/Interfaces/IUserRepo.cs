using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface IUserRepo
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public string AddUser(User user);
        public string UpdateUser(User user);
        public string DeleteUser(int id);


    }
}
