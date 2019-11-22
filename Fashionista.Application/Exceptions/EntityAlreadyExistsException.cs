namespace Fashionista.Application.Exceptions
{
    using Humanizer;

    public class EntityAlreadyExistsException : BaseCustomException
    {
        public EntityAlreadyExistsException(string name, object key)
            : base("Entity \"{0}\" ({1}) already exists or is marked as deleted!".FormatWith(name, key))
        {
        }
    }
}
