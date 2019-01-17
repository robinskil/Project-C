using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectC_v2.Database;
using ProjectC_v2.Models;

namespace ProjectC_v2.Services
{
    public class AdminStatsService
    {
        public static int UsersCount(WebshopDbContext db) => db.User.Count();
        public static int ProductsCount(WebshopDbContext db) => db.Product.Count();
        public static int OrdersProcessingCount(WebshopDbContext db) => db.Order.Count(x => x.StatusId == 1);

        public static IEnumerable<Product> GetProducts(WebshopDbContext db) => db.Product.Include(p => p.GameType)
            .Include(p => p.PGRating).Include(p => p.Publisher);

        public static (List<string>, List<int>) OrdersPerDateCount(WebshopDbContext db, DateTime startDate,
            DateTime endDate)
        {
            var orderDateTimeArray = db.Order.Where(x => x.Date >= startDate && x.Date <= endDate).Select(x => x.Date)
                .Distinct().ToArray();
            var orderDatesToStringsList = new List<string>();
            var orderCountList = new List<int>();
            foreach (DateTime item in orderDateTimeArray)
            {
                orderDatesToStringsList.Add(item.ToShortDateString());
                orderCountList.Add(db.Order.Count(x => x.Date == item));
            }

            return (orderDatesToStringsList, orderCountList);
        }

        public static (List<string>, List<int>) OrdersPerDateCountv1(WebshopDbContext db)
        {
            var orderDateTimeArray = db.Order.Select(x => x.Date).Distinct().ToArray();
            var orderDatesToStringsList = new List<string>();
            var orderCountList = new List<int>();
            foreach (DateTime item in orderDateTimeArray)
            {
                orderDatesToStringsList.Add(item.ToShortDateString());
                orderCountList.Add(db.Order.Count(x => x.Date == item));
            }
            return (orderDatesToStringsList, orderCountList);
        }

        public static (List<string>, string, List<float>) ProductRevenueIndividual(WebshopDbContext db, DateTime startDate, DateTime endDate, string productName)
        {
            var query =
                from o in db.Order.ToList()
                join oi in db.OrderItem.ToList() on o.OrderId equals oi.OrderId
                where o.Date >= startDate && o.Date <= endDate
                group oi.PriceBought by new { oi.InventoryId, o.Date } into g
                select new
                {
                    InvIDdAndDate = g.Key,
                    Amount = g.Sum()
                };

            var datesList = new List<string>();
            var inventoryIdList = new List<int>();
            var amountsList = new List<float>();

            foreach (var item in query)
            {
                datesList.Add(item.InvIDdAndDate.Date.ToShortDateString());
                inventoryIdList.Add(item.InvIDdAndDate.InventoryId);
                amountsList.Add(item.Amount);
            }

            var productsList = new List<string>();

            foreach (var invId in inventoryIdList)
            {
                var invModel = db.Inventory.Single(x => x.InventoryId == invId);
                var productIdList = new List<int> { invModel.ProductId };
                foreach (var prodId in productIdList)
                {
                    var productModel = db.Product.Single(x => x.ProductId == prodId);
                    productsList.Add(productModel.Name);
                }
            }

            var productDatesList = new List<string>();
            var productAmountsList = new List<float>();
            var product = "No sales found";

            for (var i = 0; i < productsList.Count; i++)
            {
                var date = datesList[i];
                var amount = amountsList[i];
                var name = productsList[i];

                if (name == productName)
                {
                    product = name;
                    productDatesList.Add(datesList[i]);
                    productAmountsList.Add(amountsList[i]);
                }
            }

            return (productDatesList, product, productAmountsList);
        }

        public static (List<string>, List<int>) ProductSalesTotal(WebshopDbContext db, DateTime startDate, DateTime endDate)
        {
            var query =
                from o in db.Order.ToList()
                join i in db.OrderItem.ToList() on o.OrderId equals i.OrderId
                where o.Date >= startDate && o.Date <= endDate
                group i.Amount by o.Date into g
                select new
                {
                    Date = g.Key,
                    SoldAmount = g.Sum()
                };

            var datesList = new List<string>();
            var amountsList = new List<int>();

            foreach (var item in query)
            {

                datesList.Add(item.Date.ToShortDateString());
                amountsList.Add(item.SoldAmount);
            }
            return (datesList, amountsList);
        }

        public static (List<string>, List<float>) ProductRevenueTotal(WebshopDbContext db, DateTime startDate, DateTime endDate)
        {
            var query =
                from o in db.Order.ToList()
                join i in db.OrderItem.ToList() on o.OrderId equals i.OrderId
                where o.Date >= startDate && o.Date <= endDate
                group i.PriceBought by o.Date into g
                select new
                {
                    Date = g.Key,
                    PriceBought = g.Sum()
                };

            var datesList = new List<string>();
            var revenueList = new List<float>();

            foreach (var item in query)
            {

                datesList.Add(item.Date.ToShortDateString());
                revenueList.Add(item.PriceBought);
            }
            return (datesList, revenueList);
        }

        public static (List<string>, List<int>) ProductSalesPerRegio(WebshopDbContext db, DateTime startDate, DateTime endDate)
        {
            var orderCount = new List<int>();
            var cities = (from t in db.Order
                select t.City).Distinct().ToList();

            var orders = db.Order.ToList();

            foreach (var city in cities)
            {
                orderCount.Add(orders.Count(t => t.City == city));
            }

            return (cities, orderCount);
        }

    }
}

