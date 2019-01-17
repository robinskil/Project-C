using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectC_v2.Database;
using ProjectC_v2.Models;
using ProjectC_v2.Services;

namespace ProjectC_v2.Controllers
{
    [Authorize]
    public sealed class WishListController : Controller
    {
        private WebshopDbContext _db;

        public WishListController(WebshopDbContext db){
            _db = db;
        }
        [Route("/WishList")]
        public IActionResult WishList()
        {
            WishList list = WishListService.GetWishList(User, _db);
            if (list == null)
            {
                return View(null);
            }
            return View(list);
        }

        public IActionResult AddToWishList(int inventoryId)
        {
            if(WishListService.AddInventory(inventoryId , User , _db)){
                return Ok();
            }
            return BadRequest();
        }
        public IActionResult RemoveFromWishList(int inventoryId)
        {
            if(WishListService.RemoveInventory(inventoryId , User, _db)){
                return Ok();
            }
            return BadRequest();
        }
        public IActionResult DecreaseAmount(int inventoryId)
        {
            if(WishListService.DecreaseAmountInventory(inventoryId , User, _db)){
                return Ok();
            }
            return BadRequest();
        }
    }
}