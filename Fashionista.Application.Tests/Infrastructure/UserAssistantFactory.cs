namespace Fashionista.Application.Tests.Infrastructure
{
    using Fashionista.Application.Interfaces;
    using Moq;

    public class UserAssistantFactory
    {
        public static Mock<IUserAssistant> Create(
            int shoppingCartId,
            string userId,
            string userFirstName,
            string userLastName,
            string userPhoneNumber)
        {
            var userAssistantMock = new Mock<IUserAssistant>();
            userAssistantMock.Setup(x => x.ShoppingCartId).Returns(shoppingCartId);
            userAssistantMock.Setup(x => x.UserId).Returns(userId);
            userAssistantMock.Setup(x => x.FullName).Returns($"{userFirstName} {userLastName}");
            userAssistantMock.Setup(x => x.PhoneNumber).Returns(userPhoneNumber);

            return userAssistantMock;
        }
    }
}