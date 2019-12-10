namespace Fashionista.Application.Exceptions
{
    public class ProductContainsAttributeException : BaseCustomException
    {
        public ProductContainsAttributeException()
        : base("Product already contains the selected attributes")
        {
        }
    }
}
