using Fashionista.Domain.Entities.Enums;

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

            dbContext.SaveChanges();
        }
    }
}