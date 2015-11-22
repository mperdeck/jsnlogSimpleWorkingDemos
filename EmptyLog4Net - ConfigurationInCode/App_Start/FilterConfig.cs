using System.Web;
using System.Web.Mvc;

namespace EmptyLog4Net___ConfigurationInCode
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}