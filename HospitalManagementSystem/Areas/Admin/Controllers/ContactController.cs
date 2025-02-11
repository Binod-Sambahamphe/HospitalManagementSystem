//using Hospital.Services;
//using Hospital.ViewModels;
//using Microsoft.AspNetCore.Mvc;

//namespace HospitalManagementSystem.Areas.Admin.Controllers
//{
//    [Area("admin")]
//    public class ContactController : Controller
//    {
//        private  IContactService _contact;
//        private IHospitalInfo _hospitalInfo;

//        // Correct constructor: properly initialize the private field
//        public ContactController(IContactService contact, IHospitalInfo hospitalInfo)
//        {
//            _contact = contact;
//            _hospitalInfo = hospitalInfo;
//        }

//        // Action for listing hospitals with pagination
//        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
//        {
//           return View(_contact.GetAll(pageNumber, pageSize));
//        }

//        // GET: Edit hospital view
//        [HttpGet]
//        public IActionResult Edit(int id)
//        {
//            var viewModel = _contact.GetHospitalById(id);
//            return View(viewModel);
//        }

//        [HttpPost]
//        public IActionResult Edit(ContactViewModel vm)
//        {
//            _contact.UpdateHospitalInfo(vm);
//            return RedirectToAction("Index");
//        }
//        // POST: Create hospital - shows the form
//        [HttpGet]
//        public IActionResult Create()
//        {
//            return View();
//        }

//        // POST: Create hospital - handles form submission
//        [HttpPost]
//        public IActionResult Create(ContactViewModel vm)
//        {
//            _contact.InsertHospitalInfo(vm);
//            return RedirectToAction("Index");
//        }

//        // DELETE: Delete a hospital by ID
//        public IActionResult Delete(int id)
//        {
//            _contact.DeleteHospitalInfo(id);
//            return RedirectToAction("Index");
//        }
//    }
//}
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ContactController : Controller
    {
        private readonly IContactService _contact;
        private readonly IHospitalInfo _hospitalInfo;

        public ContactController(IContactService contact, IHospitalInfo hospitalInfo)
        {
            _contact = contact;
            _hospitalInfo = hospitalInfo;
        }

        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            return View(_contact.GetAll(pageNumber, pageSize));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _contact.GetHospitalById(id);
            if (viewModel == null)
            {
                return NotFound(); // Handle missing data
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ContactViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _contact.UpdateHospitalInfo(vm);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ContactViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _contact.InsertHospitalInfo(vm);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _contact.DeleteHospitalInfo(id);
            return RedirectToAction("Index");
        }
    }
}
