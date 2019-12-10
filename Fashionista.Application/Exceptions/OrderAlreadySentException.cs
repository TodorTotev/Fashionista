namespace Fashionista.Application.Exceptions
{
    public class OrderAlreadySentException : BaseCustomException
    {
        public OrderAlreadySentException()
            : base("Order is already marked as sent!")
        {
        }
    }
}
