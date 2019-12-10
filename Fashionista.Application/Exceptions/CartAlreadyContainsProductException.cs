namespace Fashionista.Application.Exceptions
{
    using Humanizer;

    public class CartAlreadyContainsProductException : BaseCustomException
    {
        public CartAlreadyContainsProductException()
        : base("Product is already added in the cart!")
        {
        }
    }
}
