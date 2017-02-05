using AegisToDo.Repository;
using System.Web.Mvc;

namespace AegisToDo.Controllers
{
    public class ToDoController : Controller
    {
        private IToDoItemsRepository repository;

        public ToDoController(IToDoItemsRepository toDoRepository)
        {
            repository = toDoRepository;
        }

        // GET: ToDoItems
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            var toDoItems = await repository.GetItems();

            return View(toDoItems);
        }
    }
}