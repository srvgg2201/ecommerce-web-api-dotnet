using BLL.Repositories.Interfaces;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _repo;
        public CategoryController(ICategoryRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var res = _repo.GetCategories().ToList();
            return Ok(res);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_repo.GetCategoryById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Category category)
        {
            return Ok(_repo.AddCategory(category));
        }

        [HttpPut]
        public IActionResult Put([FromBody]Category category)
        {
            return Ok(_repo.UpdateCategory(category));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(_repo.RemoveCategory(id));
        }

    }
}
