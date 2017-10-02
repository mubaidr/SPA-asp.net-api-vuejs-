using System.Web.Mvc;

namespace MBO_API.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            return RedirectToAction("index", "help");
        }
    }
}
