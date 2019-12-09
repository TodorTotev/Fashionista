// ReSharper disable InconsistentNaming
namespace Fashionista.Common
{
    public class GlobalConstants
    {
        public const string AdministratorRoleName = "Administrator";
        
        public const string ShoppingCartKey = "shoppingCart";
        
        #region Brands

        public const int BrandImageHeight = 184;
        public const int BrandImageWidth = 184;

        #endregion

        #region Exceptions

        public const string EntityAlreadyDeletedMessage = "Entity is already deleted";
        
        #endregion

        #region Notifications
        
        public const string CreatedNotificationMsg = "has been created successfully!";
        public const string DeletedNotificationMsg = "has been deleted successfully!";
        public const string EditedNotificationMsg = "has been modified successfully!";
        public const string NotificationHeaderMsg = "{0} [Id:{1}]";

        #endregion
    }
}