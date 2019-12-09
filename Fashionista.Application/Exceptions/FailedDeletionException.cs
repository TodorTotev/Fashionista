namespace Fashionista.Application.Exceptions
{
    using Humanizer;

    public class FailedDeletionException : BaseCustomException
    {
        public FailedDeletionException(string name, object key, string message)
            : base("Deletion of entity \"{0}\" ({1}) failed. {2}".FormatWith(name, key, message))
        {
        }
    }
}
