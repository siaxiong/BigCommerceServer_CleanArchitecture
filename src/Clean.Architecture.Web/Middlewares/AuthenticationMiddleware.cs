namespace Clean.Architecture.Web.Middlewares;

public class AuthenticationMiddleware
{
  private readonly RequestDelegate _next;

  public AuthenticationMiddleware(RequestDelegate requestDelegate)
  {
    _next = requestDelegate;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    var username =  context.Request.Headers["username"];
    var password =  context.Request.Headers["password"];

    //validate username and password


    await _next(context);
  }
}

public static class AuthenticationMiddlewareExtension
{
  public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder app)
  {
    return app.UseMiddleware<AuthenticationMiddleware>();
  }
}
