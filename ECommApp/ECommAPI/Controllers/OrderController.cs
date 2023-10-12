using BLL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _repo;
        public OrderController(IOrderRepo repo)
        {
            _repo = repo;
                
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            return Ok(_repo.GetAllOrders());
        }

        [HttpGet("GetByUser")]
        public IActionResult GetByUserId(int userid)
        {
            return Ok(_repo.GetOrderByUserId(userid));
        }

        [HttpGet("GetByOrderId")]
        public IActionResult GetByOrderId(int orderid)
        {
            return Ok(_repo.GetByOrderId(orderid));
        }
    }
}
