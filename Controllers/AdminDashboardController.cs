using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectC_v2.Database;
using ProjectC_v2.Services;

namespace ProjectC_v2.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly WebshopDbContext _db;
        public AdminDashboardController(WebshopDbContext db)
        {
            _db = db;
        }
        [Route("/AdminDashboard")]
        public IActionResult AdminDashboard()
        {
            var products = AdminStatsService.GetProducts(_db);
            ViewBag.StartDate = DateTime.Parse("2018-09-01");
            ViewBag.EndDate = DateTime.Parse(DateTime.Today.ToString("yyyy-MM-dd"));

            // Charts
            var salesTotal = AdminStatsService.ProductSalesTotal(_db, ViewBag.StartDate, ViewBag.EndDate);
            ViewBag.SalesTotalDates = salesTotal.Item1;
            ViewBag.SalesTotalAmounts = salesTotal.Item2;

            var revenueTotal = AdminStatsService.ProductRevenueTotal(_db, ViewBag.StartDate, ViewBag.EndDate);
            ViewBag.RevenueTotalDates = revenueTotal.Item1;
            ViewBag.RevenueTotalAmounts = revenueTotal.Item2;

            var revenueIndividual = AdminStatsService.ProductRevenueIndividual(_db, ViewBag.StartDate,ViewBag.EndDate, "Call of Duty");
            ViewBag.SalesIndividualDates = revenueIndividual.Item1;
            ViewBag.SalesIndividualProducts = revenueIndividual.Item2;
            ViewBag.SalesIndividualAmounts = revenueIndividual.Item3;

            var ordersPerDateCount = AdminStatsService.OrdersPerDateCount(_db, ViewBag.StartDate, ViewBag.EndDate);
            ViewBag.OrderDates = ordersPerDateCount.Item1.ToArray();
            ViewBag.OrderCount = ordersPerDateCount.Item2.ToArray();

            var regioCount = AdminStatsService.ProductSalesPerRegio(_db, ViewBag.StartDate, ViewBag.EndDate);
            ViewBag.Regions = regioCount.Item1;
            ViewBag.RegioSales = regioCount.Item2;

            // Stats
            ViewBag.UsersCount = AdminStatsService.UsersCount(_db);
            ViewBag.ProductsCount = AdminStatsService.ProductsCount(_db);
            ViewBag.OrderProcessingCount = AdminStatsService.OrdersProcessingCount(_db);
            return View(products);
        }

        [HttpPost]
        [Route("/AdminDashboard")]
        public ActionResult AdminDashboard(string startDate, string endDate, string productName)
        {
            var products = AdminStatsService.GetProducts(_db);

            if (startDate == "" || endDate == "") return RedirectToAction("AdminDashboard", "AdminDashboard");

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

            // Charts
            var salesTotal = AdminStatsService.ProductSalesTotal(_db, DateTime.Parse(startDate), DateTime.Parse(endDate));
            ViewBag.SalesTotalDates = salesTotal.Item1;
            ViewBag.SalesTotalAmounts = salesTotal.Item2;

            var revenueTotal =
                AdminStatsService.ProductRevenueTotal(_db, DateTime.Parse(startDate), DateTime.Parse(endDate));
            ViewBag.RevenueTotalDates = revenueTotal.Item1;
            ViewBag.RevenueTotalAmounts = revenueTotal.Item2;

            if (productName != "" || productName != null)
            {
                var revenueIndividual = AdminStatsService.ProductRevenueIndividual(_db, DateTime.Parse(startDate), DateTime.Parse(endDate), productName);
                ViewBag.SalesIndividualDates = revenueIndividual.Item1;
                ViewBag.SalesIndividualProducts = revenueIndividual.Item2;
                ViewBag.SalesIndividualAmounts = revenueIndividual.Item3;
            }

            var ordersPerDateCount = AdminStatsService.OrdersPerDateCount(_db, DateTime.Parse(startDate), DateTime.Parse(endDate));
            ViewBag.OrderDates = ordersPerDateCount.Item1.ToArray();
            ViewBag.OrderCount = ordersPerDateCount.Item2.ToArray();

            // Stats
            ViewBag.UsersCount = AdminStatsService.UsersCount(_db);
            ViewBag.ProductsCount = AdminStatsService.ProductsCount(_db);
            ViewBag.OrderProcessingCount = AdminStatsService.OrdersProcessingCount(_db);

            return View(products);
        }
    }
}