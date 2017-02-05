using AegisToDo.Repository;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<ActionResult> Index()
        {
            var toDoItems = await repository.GetItems();

            if(!toDoItems.Any())
            {
                ViewBag.Message = "No items found. Please add your first item.";
            }

            ViewBag.Message = "Manage your ToDo items here";

            return View(toDoItems);
        }

        //GET: ToDoItems/Create
        public async Task<ActionResult> Create()
        {
            return PartialView("Create");
        }
    }
}