using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using PhotoBank.Models;
using PhotoBank.ViewModels;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace PhotoBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> Cabinet(string email)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Sex = user.Sex, BirthDay = user.BirthDay, Check = false };
            return View(model);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize(Roles = "user")]
        public async Task<IActionResult> Feedback(string email, string text = null)
        {
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }
            RegisterViewModel model = new RegisterViewModel { Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, Sex = user.Sex, BirthDay = user.BirthDay};

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(text))
            {
                IList<User> admins = await _userManager.GetUsersInRoleAsync("admin");
                text = "Собщение от " + email + " " + Environment.NewLine + text;

                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(admins[0].Email, "Фотобанк - Обратная связь " + email, text);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
