namespace Recipe.CustomMiddlewares
{
    public class AuthenticatorMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("/Account/Login"); // Change the URL to your login page URL
                return;
            }
            await _next(context);
        }
    }

}
