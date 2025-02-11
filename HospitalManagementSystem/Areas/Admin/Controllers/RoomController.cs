using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RoomController : Controller
    {
        private  IRoomService _room;

        // Correct constructor: properly initialize the private field
        public RoomController(IRoomService room)
        {
            _room = room;
        }

        // Action for listing hospitals with pagination
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
           return View(_room.GetAll(pageNumber, pageSize));
        }

        // GET: Edit hospital view
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _room.GetRoomById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(RoomViewModel vm)
        {
            _room.UpdateRoom(vm);
            return RedirectToAction("Index");
        }
        // POST: Create hospital - shows the form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create hospital - handles form submission
        [HttpPost]
        public IActionResult Create(RoomViewModel vm)
        {
            _room.InsertRoom(vm);
            return RedirectToAction("Index");
        }

        // DELETE: Delete a hospital by ID
        public IActionResult Delete(int id)
        {
            _room.DeleteRoom(id);
            return RedirectToAction("Index");
        }
    }
}
