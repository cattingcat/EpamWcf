using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

using NLog;

using DataAccessors.Accessors;
using DataAccessors.Entity;

using BusinessLogic;


namespace MvcClient.Controllers
{
    public class PersonController : Controller
    {
        private Logger _logger;
        private IPersonBll _personBll;

        public PersonController(IPersonBll personBll)
        {            
            _personBll = personBll;
            _logger = LogManager.GetCurrentClassLogger();

            _logger.Trace("Person controller created");
        }

        // GET: /Person/
        public ActionResult Index()
        {            
            _logger.Trace("Person controller /Index");

            return View(_personBll.GetPersons());
        }
        
        // GET: /Person/Details/5
        public ActionResult Details(int id)
        {
            _logger.Trace("Person controller /Details/{0}", id);     
   
            return View(_personBll.GetPerson(id));
        }
        
        // GET: /Person/Create
        public ActionResult Create()
        {
            _logger.Trace("Person controller /Create");

            return View();
        }
        
        // POST: /Person/Create
        [HttpPost]
        public ActionResult Create(Person person, int? captcha)
        {
            _logger.Trace("Person controller /Create/{0} POST", person.Id);

            if (CaptchaValid(captcha))
            {
                _personBll.AddPerson(person);
                return RedirectToAction("Index");
            }
            return View();
        }
        
        // POST: /Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int? captcha)
        {
            _logger.Trace("Person controller /Delete/{0}", id);

            if (CaptchaValid(captcha))
            {
                try
                {
                    _personBll.DeletePerson(id);
                    return RedirectToAction("Index");
                }
                catch (SqlException e)
                {
                    Session.Add("Exception", e);
                    return RedirectToAction("Index", "Exception");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private bool CaptchaValid(int? captcha)
        {
            object cpt = Session["captcha"];
            return (cpt != null && captcha.HasValue && captcha.Value == (int)cpt);
        }
    }
}
