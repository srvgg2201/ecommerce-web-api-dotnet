using DAL;
using BLL.Repositories.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UserRepo : IUserRepo
    {
        private readonly DataContext _dbContext;
        public UserRepo(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.Carts.Add(new Cart { User = user, CartItemCount = 0, GrandTotal = 0, UserId = user.Id, CartItems = new List<CartItem>() });
            _dbContext.SaveChanges();
            return "New User " + user.Name + " Created Successfully";
        }

        public string DeleteUser(int id)
        {
            var res = _dbContext.Users.Find(id);
            if(res != null)
            {
                _dbContext.Users.Remove(res);
                _dbContext.SaveChanges();
                return "User Deleted Successfully";
            }
            return "Invalid User ID/ User Not Found";
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(int id)
        {
            return _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
        }

        public string UpdateUser(User user)
        {
            var res = _dbContext.Users.Where(x => x.Id == user.Id).FirstOrDefault();
            if(res != null)
            {
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();
                return "User Details Updated Successfully";
            }
            return "User Not Found";
        }
    }
}
