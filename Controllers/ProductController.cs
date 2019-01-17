using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectC_v2.Database;
using ProjectC_v2.Models;
using ProjectC_v2.Models.ViewModels;
using ProjectC_v2.Services;

namespace ProjectC_v2.Controllers
{
    public sealed partial class ProductController : Controller
    {
        private readonly WebshopDbContext _db;

        public ProductController(WebshopDbContext db){
            _db = db;
        }

        //public IActionResult SearchProducts(string queryString)
        //{
        //    ViewData["queryString"] = queryString;
        //    return View();
        //}
        public async Task<IActionResult> Search(string query)
        {
            if (String.IsNullOrEmpty(query))
            {
                return Redirect("/");
            }
            ViewBag.Query = query;
            query = query.ToLower();
            List<Product> products = await ProductService.GetSearchProducts(query, _db);
            ViewBag.Publishers = ProductService.DynamicPublishers(products);
            ViewBag.GameTypes = ProductService.DynamicGameTypes(products);
            ViewBag.Platforms = ProductService.DynamicPlatforms(products);
            return View(products);
        }

        public async Task<IActionResult> SearchFilter(string query , List<string> publishersObject , List<string> gameTypesObject , List<string> platformsObject){
            if(publishersObject == null || platformsObject == null || gameTypesObject == null){
                return BadRequest("publisherObject null");
            }
            if (String.IsNullOrEmpty(query))
            {
                return Redirect("/");
            }
            ViewBag.Query = query;
            query = query.ToLower();
            List<Product> products = await ProductService.GetSearchProducts(query, _db);
            ViewBag.Publishers = ProductService.DynamicPublishers(products);
            ViewBag.GameTypes = ProductService.DynamicGameTypes(products);
            ViewBag.Platforms = ProductService.DynamicPlatforms(products);
            if(publishersObject.Any()){
                products.RemoveAll(p => !publishersObject.Any(po => po == p.Publisher.PublisherName));
            }
            if(platformsObject.Any()){
                products.RemoveAll(p => !p.Inventories.Any(i => platformsObject.Any(platform => platform == i.GamePlatform.PlatformName)));
            }
            if(gameTypesObject.Any()){
                products.RemoveAll(p => !gameTypesObject.Any(go => go == p.GameType.GenreName));
            }
            return View("Search",products);
        }

        public async Task<IActionResult> AllProducts(List<string> publishersObject , List<string> gameTypesObject , List<string> platformsObject)
        {
            IQueryable<Product> queryProducts = _db.Product.Include(p => p.GameType).Include(p => p.PGRating).Include(p => p.Publisher).Include(p => p.ProductImages).Include(p =>p.Inventories).ThenInclude(p => p.GamePlatform).Where(p => p.Inventories.Count > 0);//.Where(p => p.Inventories.Any(i => i.Stock > 0));
            List<Product> products = await queryProducts.ToListAsync();
            ViewBag.Publishers = ProductService.DynamicPublishers(products);
            ViewBag.GameTypes = ProductService.DynamicGameTypes(products);
            ViewBag.Platforms = ProductService.DynamicPlatforms(products);
            if(publishersObject.Any()){
                products.RemoveAll(p => !publishersObject.Any(po => po == p.Publisher.PublisherName));
            }
            if(platformsObject.Any()){
                products.RemoveAll(p => !p.Inventories.Any(i => platformsObject.Any(platform => platform == i.GamePlatform.PlatformName)));
            }
            if(gameTypesObject.Any()){
                products.RemoveAll(p => !gameTypesObject.Any(go => go == p.GameType.GenreName));
            }
            return View(products);
        }
        //public List<Product> GetFilteredProducts(string answer, IQueryable<Product> products)
        //{
        //    var productList = new List<Product>();
        //    foreach (var product in products.ToList())
        //    {
        //        if (product.Inventories.Any())
        //        {
        //            foreach (var product1Inventory in product.Inventories)
        //            {
        //                if (product1Inventory.GamePlatform.PlatformName == answer)
        //                {
        //                    productList.Add(product);
        //                }
        //            }
        //        }
        //    }
        //    return productList.ToList();
        //}
        
        [HttpGet]
        public IActionResult GetAllProducts(){
            return Ok(ProductService.GetProducts(_db));
        }

        [Route("/Product")]
        public IActionResult ViewProduct(string name , string platform)
        {
            if(!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(platform))
            {
                Product product = _db.Product.Include(p => p.GameType).Include(p => p.PGRating).Include(p => p.Publisher)
                                            .Include(p => p.ProductImages).Include(p => p.Inventories).ThenInclude(i => i.GamePlatform)
                                            .Include(p => p.ProductFeatures).Where(p=>p.Name == name).FirstOrDefault();
                if (product != null && product.Inventories != null && product.Inventories.Count > 0 && product.Inventories.Any(p => p.GamePlatform.PlatformName == platform))
                {
                    return View("ProductDisplay",product);
                }
            }
            return NotFound();
        }

    }
    /// <summary>
    /// Generated code by visual studio here
    /// </summary>
    public sealed partial class ProductController : Controller
    {

        /*GENERATED CODE BY VISUAL STUDIO*/
        // GET: Product
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var webshopDbContext = _db.Product.Include(p => p.GameType).Include(p => p.PGRating).Include(p => p.Publisher);
            return View(await webshopDbContext.ToListAsync());
        }

        // GET: Product/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Product
                .Include(p => p.GameType)
                .Include(p => p.PGRating)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["GameTypeId"] = new SelectList(_db.GameType, "GameTypeId", "GenreName");
            ViewData["PGRatingId"] = new SelectList(_db.PGRating, "PGRatingId", "Rating");
            ViewData["PublisherId"] = new SelectList(_db.Publisher, "PublisherId", "PublisherName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ReleaseDate,Description,PublisherId,GameTypeId,PGRatingId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Add(product);
                await _db.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return Redirect("/Product/Index");
            }
            ViewData["GameTypeId"] = new SelectList(_db.GameType, "GameTypeId", "GenreName", product.GameTypeId);
            ViewData["PGRatingId"] = new SelectList(_db.PGRating, "PGRatingId", "Rating", product.PGRatingId);
            ViewData["PublisherId"] = new SelectList(_db.Publisher, "PublisherId", "PublisherName", product.PublisherId);
            return View(product);
        }

        // GET: Product/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["GameTypeId"] = new SelectList(_db.GameType, "GameTypeId", "GenreName", product.GameTypeId);
            ViewData["PGRatingId"] = new SelectList(_db.PGRating, "PGRatingId", "Rating", product.PGRatingId);
            ViewData["PublisherId"] = new SelectList(_db.Publisher, "PublisherId", "PublisherName", product.PublisherId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ReleaseDate,Description,PublisherId,GameTypeId,PGRatingId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(product);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Product/Index");
            }
            ViewData["GameTypeId"] = new SelectList(_db.GameType, "GameTypeId", "GenreName", product.GameTypeId);
            ViewData["PGRatingId"] = new SelectList(_db.PGRating, "PGRatingId", "Rating", product.PGRatingId);
            ViewData["PublisherId"] = new SelectList(_db.Publisher, "PublisherId", "PublisherName", product.PublisherId);
            return View(product);
        }

        // GET: Product/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _db.Product
                .Include(p => p.GameType)
                .Include(p => p.PGRating)
                .Include(p => p.Publisher)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var product = await _db.Product.FindAsync(id);
            //_db.Product.Remove(product);
            //Omar said no deleting of products , only making them unavailable
            List<Inventory> inventories = await _db.Inventory.Where(i => i.ProductId == id).ToListAsync();
            foreach(Inventory inv in inventories)
            {
                inv.Stock = 0;
                _db.Update<Inventory>(inv);
            }
            await _db.SaveChangesAsync();
            return Redirect("/Product/Index");
        }

        private bool ProductExists(int id)
        {
            return _db.Product.Any(e => e.ProductId == id);
        }
    }
}