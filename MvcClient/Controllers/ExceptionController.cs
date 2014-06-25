using System;
using System.Web.Mvc;

using NLog;

namespace MvcClient.Controllers
{
    public class ExceptionController: Controller
    {
        private Logger _logger;

        public ExceptionController()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public ViewResult Index()
        {            
            Exception e = Session["Exception"] as Exception;

            _logger.Warn("Exception controller called, message: {0}", e.Message);

            return View(e);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            Session.Remove("Exception");

            _logger.Trace("Exception controller finished, session cleared");
        }
    }
}