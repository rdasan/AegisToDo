using AegisToDo.Repository;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Linq;
using AegisToDo.Models;
using System.Net;

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
        public ActionResult Create()
        {
            return PartialView("Create");
        }

        //POST: ToDoItems/AddItem
        [HttpPost]
        public async Task<ActionResult> AddItem(ToDoItem itemToAdd)
        {
            if(!ModelState.IsValid || itemToAdd == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var newItem = await repository.AddItem(itemToAdd);

            return RedirectToAction("Index");
        }

        //GET: ToDoItems/Get/5
        public async Task<ActionResult> Get(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var item = await repository.GetItemById(id.Value);

            return PartialView("Edit", item);
        }

        //POST: ToDoItems/UpdateItem
        public async Task<ActionResult> UpdateItem(ToDoItem itemToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var updatedItem = await repository.UpdateItem(itemToUpdate);

            return RedirectToAction("Index");
        }

        //GET: ToDoItems/UpdateState/5
        public async Task<ActionResult> UpdateState(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var updatedItem = await repository.UpdateItemState(id.Value);

            return RedirectToAction("Index");
        }

        // GET: ToDoItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await repository.DeleteItem(id.Value);

            return RedirectToAction("Index");
        }
    }
}