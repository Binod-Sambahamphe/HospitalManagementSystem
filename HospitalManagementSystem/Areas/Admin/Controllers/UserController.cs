using Hospital.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private IApplicationUserService _userService;

        public UserController(IApplicationUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(int PageNumber=1,int PageSize=10)
        {
            return View(_userService.GetAll(PageNumber,PageSize));
        }
    }
}
