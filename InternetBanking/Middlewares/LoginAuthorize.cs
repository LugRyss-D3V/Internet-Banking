using InternetBanking.Controllers;
using InternetBanking.Core.Application.Dtos.Account.Response;
using InternetBanking.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InternetBanking.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginAuthorize(ValidateUserSession userSession, IHttpContextAccessor httpContextAccessor)
        {
            _userSession = userSession;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {

                var controller = (UserController)context.Controller;
                AuthenticationResponse? User = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

                if(User.Roles.Any(role => role == "Administrator"))
                    context.Result = controller.RedirectToAction("Index", "Home");

                if (User.Roles.Any(role => role == "Client"))
                    context.Result = controller.RedirectToAction("Index", "Client");
            }
            else
            {
                await next();
            }
        }

    }
}
