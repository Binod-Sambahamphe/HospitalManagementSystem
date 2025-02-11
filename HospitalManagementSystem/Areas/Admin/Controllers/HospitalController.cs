using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("admin")]
    public class HospitalController : Controller
    {
        private readonly IHospitalInfo _hospitalInfo;

        // Correct constructor: properly initialize the private field
        public HospitalController(IHospitalInfo hospitalInfo)
        {
            _hospitalInfo = hospitalInfo;
        }

        // Action for listing hospitals with pagination
        public IActionResult Index(int pageNumber = 1, int pageSize = 10)
        {
            var hospitals = _hospitalInfo.GetAll(pageNumber, pageSize);
            return View(hospitals);
        }

        // GET: Edit hospital view
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _hospitalInfo.GetHospitalById(id);
            if (viewModel == null)
            {
                return NotFound(); // Handle case when hospital is not found
            }
            return View(viewModel);
        }

        // POST: Create hospital - shows the form
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create hospital - handles form submission
        [HttpPost]
        public IActionResult Create(HospitalInfoViewModel vm)
        {
            if (ModelState.IsValid) // Validate the model
            {
                _hospitalInfo.InsertHospitalInfo(vm);
                return RedirectToAction("Index");
            }
            return View(vm); // Return the same view if the model is invalid
        }

        // DELETE: Delete a hospital by ID
        public IActionResult Delete(int id)
        {
            var hospital = _hospitalInfo.GetHospitalById(id);
            if (hospital == null)
            {
                return NotFound(); // Handle case when hospital is not found
            }

            _hospitalInfo.DeleteHospitalInfo(id);
            return RedirectToAction("Index");
        }
    }
}
