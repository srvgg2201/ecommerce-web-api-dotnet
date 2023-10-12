using BLL.Repositories.Interfaces;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepo _repo;
        public CartController(ICartRepo repo)
        {
            _repo = repo;
        }



        [HttpGet("ViewCart")]
        public IActionResult GetCart(int userid)
        {
            return Ok(_repo.ViewCart(userid));
        }

        //[HttpGet("ViewCartItems")] 
        /*public IActionResult ViewCartItems(int userid)
        {
            return Ok(_repo.ViewCartItems(userid));
        }*/

        [HttpPost("AddCartItem")]
        public IActionResult AddItemById(int userid, int itemid, int quantity)
        {
            return Ok(_repo.AddItemById(userid, itemid, quantity));
        }

        [HttpGet("RemoveCartItem")]
        public IActionResult RemoveCartItem(int userid, int itemid)
        {
            return Ok(_repo.RemoveCartItem(userid, itemid));
        }

        [HttpGet("EmptyCart")]
        public IActionResult EmptyCart(int userid)
        {
            return Ok(_repo.EmptyCart(userid));
        }

        [HttpPut("UpdateCartItem")]
        public IActionResult UpdateCart(int userid, int itemid, int quantity)
        {
            return Ok(_repo.UpdateCart(userid, itemid, quantity));
        }

        [HttpGet("Checkout")]
        public IActionResult Checkout(int userid, double amount, string paymentMode)
        {
            return Ok(_repo.CheckOut(userid, amount, paymentMode));
        }
    }
}
