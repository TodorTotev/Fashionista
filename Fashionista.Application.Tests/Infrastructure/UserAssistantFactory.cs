namespace Fashionista.Application.Tests.Infrastructure
{
    using Fashionista.Application.Interfaces;
    using Fashionista.Infrastructure;
    using Moq;

    public class UserAssistantFactory
    {
        public static Mock<IUserAssistant> Create(int id)
        {
            var userAssistantMock = new Mock<IUserAssistant>();
            userAssistantMock.Setup(x => x.ShoppingCartId).Returns(id);

            return userAssistantMock;
        }
    }
}