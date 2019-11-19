namespace Fashionista.Web.Common
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    public static class ListSelectionAssistant
    {
        public static string IsActive(this IHtmlHelper helper, string controller, string action)
        {
            var routeData = helper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = controller == routeController && (action == routeAction || routeAction == "Details");

            return returnActive ? "active" : string.Empty;
        }
    }
}