using DataAccessors.Accessors;
using DataAccessors.Entity;
using NLog;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcClient.Controllers
{
    public class PhoneController : Controller
    {
        private Logger _logger;
        private IPhoneBll _phoneBll;
        private IPersonBll _personBll;

        public PhoneController(IPhoneBll phoneBll, IPersonBll personBll)
        {         
            _phoneBll = phoneBll;
            _personBll = personBll;
            _logger = LogManager.GetCurrentClassLogger();

            _logger.Trace("Phone controller created");
        }

        // GET: /Phone/
        public ActionResult Index()
        {
            _logger.Trace("Phone controller /Index");

            return View(_phoneBll.GetPhones());
        }
        
        // GET: /Phone/Details/5
        // Redirect to owner-person details
        public ActionResult Details(int id)
        {
            _logger.Trace("Phone controller /Details/{0}", id);

            Phone phone = _phoneBll.GetPhones().SingleOrDefault(p => p.Id == id);   
            var dict = new System.Web.Routing.RouteValueDictionary();
            dict.Add("id", phone.PersonId);
            return RedirectToAction("Details", "Person", dict);        
        }
        
        // GET: /Phone/Create
        public ActionResult Create()
        {
            _logger.Trace("Phone controller /Create");

            ViewBag.Persons = _personBll.GetPersons();
            return View();
        }
        
        // POST: /Phone/Create
        [HttpPost]
        public ActionResult Create(Phone phone, int? captcha)
        {
            _logger.Trace("Phone controller /Create POST {0}", phone.Id);
            if (CaptchaValid(captcha))
            {
                _phoneBll.AddPhone(phone);
                return RedirectToAction("Index");
            }
            return View();
        }
        
        // POST: /Phone/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int? captcha)
        {
            _logger.Trace("Phone controller /Delete/{0}", id);

            if (CaptchaValid(captcha))
            {
                try
                {
                    _phoneBll.DeletePhone(id);
                    return RedirectToAction("Index");
                }
                catch (SqlException e)
                {
                    Session.Add("Exception", e);
                    return RedirectToAction("Index", "Exception");
                }
            }
            return RedirectToAction("Index");
        }

        private bool CaptchaValid(int? captcha)
        {
            object cpt = Session["captcha"];
            return (cpt != null && captcha.HasValue && captcha.Value == (int)cpt);
        }
    }
}
