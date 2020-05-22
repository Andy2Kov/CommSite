using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PhotoBank.Models;
using PhotoBank.ViewModels;

namespace PhotoBank.Controllers
{
    public class UsersController : Controller
    {
        UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email, BirthDay = model.BirthDay, LastName = model.LastName, FirstName = model.FirstName, Sex = model.Sex };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id = null, string email = null)
        {
            User user;

            if (id != null)
                user = await _userManager.FindByIdAsync(id);
            else
                user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                    return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, BirthDay = user.BirthDay, LastName = user.LastName, FirstName = user.FirstName, Sex = user.Sex, Check = (id != null) };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user;

                if (model.Id != null)
                    user = await _userManager.FindByIdAsync(model.Id);
                else
                    user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.LastName = model.LastName;
                    user.FirstName = model.FirstName;
                    user.Sex = model.Sex;
                    user.BirthDay = model.BirthDay;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (model.Check)
                            return RedirectToAction("Index");
                        else
                            return RedirectToAction("Cabinet", "Home", new { email = model.Email });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePassword(string id = null, string email = null)
        {
            User user;

            if (id != null)
                user = await _userManager.FindByIdAsync(id);
            else
                user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email, Check = (id != null)};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        if (model.Check)
                            return RedirectToAction("Index");
                        else
                            return RedirectToAction("Cabinet", "Home", new { email = model.Email });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string id)
        {
            if (id == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View("Error");
            }

            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                return View("Error");
        }

    }
}