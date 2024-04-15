using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Interfaces.Services;
using InternetBanking.Core.Application.ViewModels.User;
using InternetBanking.Middlewares;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InternetBanking.Core.Application.Dtos.Account.Request;
using Azure;

namespace InternetBanking.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);

                if(userVm.Roles.Any(role => role == "Administrator"))
                    return RedirectToRoute(new { controller = "Home", action = "Index" });

                if (userVm.Roles.Any(role => role == "Client"))
                    return RedirectToRoute(new { controller = "Client", action = "Index" });

                vm.HasError = userVm.HasError;
                vm.Error = "No cuenta con un rol dentro de la aplicacion";
                return View(vm);
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUsersAsync());
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [Authorize(Roles ="Administrator")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
             if (!ModelState.IsValid)
            {
                vm.HasError = true;
                vm.Error = "Validaciones Incorrectas";
                return View(vm);
            }

            RegisterResponse response = await _userService.RegisterAsync(vm);
            if (response.HasError)
                return View(vm);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return View(await _userService.GetUserEditAsync(id));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.HasError = true;
                vm.Error = "Validaciones Incorrectas";
                return View(vm);
            }

            UpdateResponse response = await _userService.EditAsync(vm);

            if (response.HasError)
                return View(vm);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(string id)
        {
            var logged = HttpContext.Session.Get<AuthenticationResponse>("user");

            if (logged.Id == id)
            {
                return RedirectToAction("Index");
            }

            ChangeStatusRequest request = new ChangeStatusRequest(id);

            ChangeStatusResponse response = await _userService.ChangeStatus(request);

            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Login" });
        }
    }
}
