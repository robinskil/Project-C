using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectC_v2.Database;
using ProjectC_v2.Models;
using ProjectC_v2.Models.ServerModels;
using ProjectC_v2.Services;

namespace ProjectC_v2.Controllers
{
    public sealed class ShoppingCartController : Controller
    {
        private WebshopDbContext _db;

        public ShoppingCartController(WebshopDbContext db){
            _db = db;
        }

        public IActionResult AddToCart(int inventoryId)
        {
            if (Request.Cookies.TryGetValue("ShoppingCart", out string value))
            {
                if (ShoppingCartService.CookieToShoppingCart(value, out ShoppingCartCookie shoppingCartCookie))
                {
                    ShoppingCartService.AddProductToShoppingCart(shoppingCartCookie, inventoryId);
                    Response.Cookies.Append("ShoppingCart", shoppingCartCookie.ToJson());
                    return Ok();
                }
                //Reset cookie string cause it dont make sense
                Response.Cookies.Delete("ShoppingCart");
                return BadRequest();
            }
            else
            {
                Response.Cookies.Append("ShoppingCart", ShoppingCartService.CreateNewShoppingCart(inventoryId));
                return Ok("New cookie set");
            }
        }

        public IActionResult DecreaseAmount(int inventoryId){
            if (Request.Cookies.TryGetValue("ShoppingCart", out string value))
            {
                if (ShoppingCartService.CookieToShoppingCart(value, out ShoppingCartCookie shoppingCartCookie))
                {
                    ShoppingCartService.DecreaseAmount(inventoryId , shoppingCartCookie);
                    Response.Cookies.Append("ShoppingCart", shoppingCartCookie.ToJson());
                    return Ok();
                }
                //Reset cookie string cause it dont make sense
                Response.Cookies.Delete("ShoppingCart");
                return BadRequest();
            }
            else
            {
                return BadRequest("Dafuq u doing");
            }
        }

        public IActionResult RemoveFromCart(int inventoryId){
            if (Request.Cookies.TryGetValue("ShoppingCart", out string value))
            {
                if (ShoppingCartService.CookieToShoppingCart(value, out ShoppingCartCookie shoppingCartCookie))
                {
                    ShoppingCartService.RemoveProduct(inventoryId , shoppingCartCookie);
                    if(shoppingCartCookie.Products.Count > 0){
                        Response.Cookies.Append("ShoppingCart", shoppingCartCookie.ToJson());
                        return Ok();
                    }
                    Response.Cookies.Delete("ShoppingCart");
                    Response.Redirect("/ShoppingCart");
                    return Ok();
                }
                //Reset cookie string cause it dont make sense
                Response.Cookies.Delete("ShoppingCart");
                Response.Redirect("/ShoppingCart");
                return BadRequest();
            }
            else
            {
                return BadRequest("Dafuq u doing");
            }
        }

        public IActionResult ClearCart(){
            Response.Cookies.Delete("ShoppingCart");
            return Ok();
        }

        [Route("/ShoppingCart")]
        public async Task<IActionResult> ShoppingCart()
        {
            if(Request.Cookies.TryGetValue("ShoppingCart", out string value)){
                if(ShoppingCartService.CookieToShoppingCart(value , out ShoppingCartCookie shoppingCartCookie))
                {
                    ShoppingCart shoppingCart = await ShoppingCartService.CookieToShoppingCartAsync(shoppingCartCookie , _db);
                    return View(shoppingCart);
                }
                else
                {
                    Response.Cookies.Delete("ShoppingCart");
                    return View(null);
                }
            }
            else
            {
                return View(null);
            }
        }
        [Authorize]
        [Route("/ShoppingCart/WishList")]
        public IActionResult ShoppingCartWishList(){
            ShoppingCart shoppingCart = ShoppingCartService.ShoppingCartWishList(User , _db);
            if(shoppingCart != null){
                return View(shoppingCart);
            }
            return View(null);
        }
    }
}