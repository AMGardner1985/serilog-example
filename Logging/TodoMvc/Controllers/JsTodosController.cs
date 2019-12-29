using System.Web.Mvc;

namespace TodoMvc.Controllers
{
    public class JsTodosController : Controller
    {        
        public ActionResult Index()
        {            
            return View();
        }
    }
}