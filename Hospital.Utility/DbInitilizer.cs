using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Utility
{
    public class DbInitilizer : IDbInitilizer
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager <IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public DbInitilizer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, 
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public void Initilize()
        {
            try
            {
                if(_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch(Exception ex)
            {
                throw;
            }
            if(!_roleManager.RoleExistsAsync(WebsiteRoles.Website_Admin).GetAwaiter().GetResult() )
            {
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_Patient)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebsiteRoles.Website_Doctor)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName="Binod",
                    Email="binod@gmail.com"

                },"Binod@123").GetAwaiter().GetResult();

                var Appuser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == "binod@gmail.com");
                if(Appuser != null)
                {
                    _userManager.AddToRoleAsync(Appuser, WebsiteRoles.Website_Admin).GetAwaiter();

                }
            }
        }
    }
}
