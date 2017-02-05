using System.Web.Mvc;

namespace AegisToDo.Controllers
{
    public class ToDoController : Controller
    {
        // GET: ToDoItems
        public ActionResult Index()
        {
            return View();
        }
    }
}