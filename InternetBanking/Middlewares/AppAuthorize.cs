using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Middlewares
{
    public class AppAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;

        public AppAuthorize(ValidateUserSession userSession)
        {
            _userSession = userSession;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                await next();
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
            }
        }
    }
}
