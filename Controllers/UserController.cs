using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectC_v2.Database;
using ProjectC_v2.Helpers;
using ProjectC_v2.Models;
using ProjectC_v2.Models.ViewModels;
using ProjectC_v2.Services;

namespace ProjectC_v2.Controllers
{
    public sealed partial class UserController : Controller
    {
        private readonly WebshopDbContext _db;

        public UserController(WebshopDbContext db)
        {
            _db = db;
        }

        // GET
        [Route("/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserService.Login(model.Email, model.Password, _db, out List<Claim> claims))
                {
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("Email", "Incorrect email or password");
            ModelState.AddModelError("Password", "Incorrect email or password");
            return View(model);
        }

        [Authorize]
        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("ShoppingCart");
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        [Authorize]
        [Route("/Profile")]
        public IActionResult Profile()
        {
            string guidString = User.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(s => s.Value)
                .SingleOrDefault();
            if (Guid.TryParse(guidString, out Guid userId))
            {
                return View(UserService.GetUser(userId, _db));
            }
            return Redirect("/");
        }
        // GET: Users/Create
        [Route("/Register")]
        public IActionResult Register()
        {
            ViewData["CityId"] = new SelectList(_db.City, "CityId", "CityName");
            ViewData["RoleId"] = new SelectList(_db.UserRole, "RoleId", "RoleName");
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel passwordViewModel){
            if(ModelState.IsValid){
                if(UserService.ChangePassword(User , passwordViewModel.Password , _db)){
                    return Redirect("/Profile");
                }
                return Unauthorized();
            }
            return BadRequest("Something went wrong , please return to the homepage");
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangeAddress(ChangeAddressViewModel addressViewModel){
            if(ModelState.IsValid){
                if(UserService.ChangeAddress(User , addressViewModel , _db)){
                    return Redirect("/Profile");
                }
                return Unauthorized();
            }
            return BadRequest("Something went wrong , please return to the homepage");
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangeName(ChangeNameViewModel nameViewModel){
            if(ModelState.IsValid){
                if(UserService.ChangeName(User , nameViewModel , _db)){
                    return Redirect("/Profile");
                }
                return Unauthorized();
            }
            return BadRequest("Something went wrong , please return to the homepage");
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Register")]
        public async Task<IActionResult> Register([Bind("UserId,Name,Surname,Street,PostalCode,Password,Birthdate,Email,CityId,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                user.UserId = Guid.NewGuid();
                user.Password = Hashing.HashPassword(user.Password);
                _db.Add(user);
                await _db.SaveChangesAsync();
                return Redirect("/Login");
            }
            ViewData["CityId"] = new SelectList(_db.City, "CityId", "CityName", user.CityId);
            ViewData["RoleId"] = new SelectList(_db.UserRole, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        [HttpPost]
        public ActionResult CheckExistingEmail(string Email)
        {
            try
            {
                return Json(!UserService.EmailExists(Email, _db));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
        
        [HttpPost]
        public async Task<ActionResult> EditExistingEmail(string Email , Guid UserId)
        {
            try
            {
                return Json(!await UserService.EmailExistsExceptOwn(UserId , Email, _db));
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
    /// <summary>
    /// GENERATED CODE BY VISUAL STUDIO STARTS HERE
    /// Some Crud actions might be altered so suffice PO needs
    /// </summary>
    public sealed partial class UserController : Controller
    {
        // GET: Users
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var webshopDbContext = _db.User.Include(u => u.City).Include(u => u.UserRole);
            return View(await webshopDbContext.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _db.User
                .Include(u => u.City)
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        // GET: Users/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //UserEditViewModel user = new UserEditViewModel(await _db.User.FindAsync(id));
            var user = await _db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_db.City, "CityId", "CityName", user.CityId);
            ViewData["RoleId"] = new SelectList(_db.UserRole, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,Name,Surname,Street,PostalCode,Password,Birthdate,Email,CityId,RoleId")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }
            ModelState.Remove("Email");
            if (ModelState.IsValid && !await UserService.EmailExistsExceptOwn(user.UserId , user.Email , _db))
            {
                try
                {
                    _db.Update<User>(user);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserService.UserExists(user.UserId, _db))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_db.City, "CityId", "CityName", user.CityId);
            ViewData["RoleId"] = new SelectList(_db.UserRole, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }
        // GET: Users/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _db.User
                .Include(u => u.City)
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var user = await _db.User.FindAsync(id);
            _db.User.Remove(user);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}