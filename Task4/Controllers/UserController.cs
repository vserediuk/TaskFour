using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using Task4.Areas.Identity.Data.Models;

namespace Task4.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Delete(string[] id)
        {
            foreach (var i in id)
            {
                var user = await _userManager.FindByIdAsync(i);
                var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                if (user == null) return NotFound();
                await _userManager.DeleteAsync(user);
            }
                return RedirectToAction("Index", "Home");
            
        }
        public async Task<IActionResult> Block(string[] id)
        {
            foreach (var i in id)
            {
                var user = await _userManager.FindByIdAsync(i);
                if (user == null) return NotFound();
                await _userManager.SetLockoutEnabledAsync(user, true);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Unblock(string[] id)
        {
            foreach (var i in id)
            {
                var user = await _userManager.FindByIdAsync(i);
                if (user == null) return NotFound();
                await _userManager.SetLockoutEnabledAsync(user, false);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
