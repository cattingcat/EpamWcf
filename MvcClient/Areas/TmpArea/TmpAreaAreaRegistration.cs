using System.Web.Mvc;

namespace MvcClient.Areas.TmpArea
{
    public class TmpAreaAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "TmpArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "TmpArea_default",
                "TmpArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
