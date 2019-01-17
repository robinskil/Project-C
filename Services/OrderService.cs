using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectC_v2.Database;
using ProjectC_v2.Models;
using ProjectC_v2.Models.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ProjectC_v2.Services
{
    public static class OrderService
    {
        public static Order[] GetOrders(Guid userId , WebshopDbContext db)
        {
            return db.Order
                .Include(orderStatus => orderStatus.OrderStatus)
                .Include(orderItem => orderItem.OrderItems).ThenInclude(inventory => inventory.Inventory).ThenInclude(product => product.Product).ThenInclude(image => image.ProductImages)
                .Include(orderItem => orderItem.OrderItems).ThenInclude(inventory => inventory.Inventory).ThenInclude(platform => platform.GamePlatform)
                .Where(o => o.User.UserId == userId).ToArray();
        }

        public static bool OrderShoppingCart(ShoppingCart shoppingCart, OrderTransactionViewModel orderTransactionViewModel, WebshopDbContext db, ClaimsPrincipal user)
        {
            Order order = new Order()
            {
                City = orderTransactionViewModel.City,
                Date = DateTime.Now,
                PostalCode = orderTransactionViewModel.PostalCode,
                Street = orderTransactionViewModel.Street,
            };
            if (user.Identity.IsAuthenticated)
            {
                string guidString = user.Claims.Where(claim => claim.Type == ClaimTypes.Sid).Select(s => s.Value)
                .SingleOrDefault();
                if(Guid.TryParse(guidString , out Guid userId))
                {
                    order.UserId = userId;
                }
            }
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach(var item in shoppingCart.ShoppingCartItems)
            {
                OrderItem orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    InventoryId = item.InventoryId,
                    PriceBought = item.Inventory.Price,
                };
                orderItems.Add(orderItem);
                db.OrderItem.Add(orderItem);
            }
            order.OrderItems = orderItems;
            order.StatusId = 1;
            db.Order.Add(order);
            db.SaveChanges();
            return true;
        }

        public static async Task SendConfirmEmail(ShoppingCart shoppingCart, OrderTransactionViewModel orderTransactionViewModel)
        {
            string htmlText = "<table> <tr> <th>Name</th> <th>Platform</th> <th>Amount</th><th>Price</th></tr>";
            foreach(var item in shoppingCart.ShoppingCartItems){
                htmlText += $"<tr> <td>{item.Inventory.Product.Name}</td> <td>{item.Inventory.GamePlatform.PlatformName}</td><td>{item.Amount}</td><td>€{item.Amount * item.Inventory.Price}</td></tr>";
            }
            htmlText += "</table>";
	    string apiKey = "";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("", "Game Now"),
                Subject = "Order",
                PlainTextContent = "We have received your order.",
                HtmlContent = "<h1>We received ur order</h1>" + htmlText + "<h1>Your order is being processed as we speak </h1>"
            };
            msg.AddTo(new EmailAddress(orderTransactionViewModel.Email, "Test User"));
            var response = await client.SendEmailAsync(msg);
        }
    }
}