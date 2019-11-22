namespace Fashionista.Application.Exceptions
{
    using Humanizer;

    public class NotFoundException : BaseCustomException
    {
        public NotFoundException(string name, object key)
            : base("Entity \"{0}\" ({1}) was not found.".FormatWith(name, key))
        {
        }
    }
}
