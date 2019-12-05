using System;
using System.Linq;
using Fashionista.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Fashionista.Application.Tests.Infrastructure.Seeding
{
    using System.Collections.Generic;
    using Fashionista.Domain.Entities;
    using Fashionista.Persistence;

    public class UserSeeder : ITestSeeder
    {
        public void Seed(ApplicationDbContext dbContext)
        {
            var user = new ApplicationUser
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Email = "Test@test.com",
                ShoppingCart = new ShoppingCart(),
                PhoneNumber = "12345678",
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            var city = dbContext.Cities.Add(new City
            {
                Name = "TestCity",
                Postcode = "TestPostCode",
                Addresses = new List<Address>(),
            });

            dbContext.SaveChanges();

            dbContext.Addresses.Add(new Address
            {
                Name = "TestAddress",
                Description = "2",
                CityId = city.Entity.Id,
                ApplicationUserId = user.Id,
            });

            dbContext.SaveChangesAsync();

            dbContext.FavoriteProducts.Add(new FavoriteProduct
            {
                ProductId = 1,
                ApplicationUserId = user.Id,
            });

            dbContext.SaveChanges();

            dbContext.ShoppingCartProducts.Add(new ShoppingCartProduct
            {
                ProductId = 1,
                Quantity = 1,
                ShoppingCartId = user.ShoppingCartId,
            });

            dbContext.Orders.Add(new Order
            {
                OrderState = OrderState.Processing,
                ApplicationUserId = user.Id,
                DeliveryAddressId = 1,
                Recipient = $"{user.FirstName} {user.LastName}",
                RecipientPhoneNumber = user.PhoneNumber,
                DeliveryFee = 7,
            });
            
            var order2 = dbContext.Orders.Add(new Order
            {
                OrderState = OrderState.Processing,
                ApplicationUserId = user.Id,
                DeliveryAddressId = 1,
                Recipient = $"{user.FirstName} {user.LastName}",
                RecipientPhoneNumber = user.PhoneNumber,
                DeliveryFee = 7,
            });

            var cartProducts = dbContext.ShoppingCartProducts
                .Where(x => x.ShoppingCartId == 1)
                .ToList();

            order2.Entity.OrderProducts = cartProducts
                .Select(currentProduct => new OrderProduct
                {
                    Order = order2.Entity,
                    ProductId = currentProduct.ProductId,
                    Quantity = currentProduct.Quantity,
                    Price = currentProduct.Product.Price,
                }).ToList();

            order2.Entity.TotalPrice = order2.Entity.OrderProducts.Sum(x => x.Quantity * x.Price) + 7;
            order2.Entity.OrderDate = DateTime.UtcNow;
            order2.Entity.InvoiceNumber = order2.Entity.Id.ToString().PadLeft(5, '0');
            order2.Entity.PaymentType = PaymentType.Card;
            order2.Entity.OrderState = OrderState.Processed;
            order2.Entity.PaymentState = PaymentState.AwaitingPayment;

            dbContext.SaveChanges();
        }
    }
}