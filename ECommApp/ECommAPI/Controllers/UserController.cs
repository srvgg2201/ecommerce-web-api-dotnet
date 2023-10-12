using BLL.Repositories.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _repo;
        public UserController(IUserRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_repo.GetAllUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            return Ok(_repo.GetUserById(id));
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            return Ok(_repo.AddUser(user));
        }

        [HttpPut]
        public IActionResult Put(User user)
        {
            return Ok(_repo.UpdateUser(user));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_repo.DeleteUser(id));
        }

    }
}
