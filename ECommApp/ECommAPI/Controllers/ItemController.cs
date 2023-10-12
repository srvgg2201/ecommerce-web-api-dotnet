using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BLL.Repositories.Interfaces;
using BLL.Repositories;
using DAL.Models;

namespace ECommAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IitemRepo _repo;
        public ItemController(IitemRepo repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(_repo.GetAllItems());
        }

        [HttpGet()]
        [Route("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_repo.GetItemById(id));
        }

        [HttpGet()]
        [Route("GetByItemName/{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok(_repo.GetItemByName(name));
        }

        [HttpGet()]
        [Route("ByCategoryName/{categoryName}")]
        public IActionResult GetByCategory(string categoryName)
        {
            return Ok(_repo.GetItemByCategory(categoryName));
        }

        [HttpPost("AddNewItem")]
        public IActionResult Post([FromBody] Item item)
        {
            return Ok(_repo.AddItem(item));
        }

        [HttpPut("UpdateItem")]
        public IActionResult Put([FromBody] Item item)
        {
            return Ok(_repo.UpdateItem(item));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            return Ok(_repo.DeleteItem(id));
        }
        

        

    }
}
