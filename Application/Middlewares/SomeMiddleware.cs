public class SomeMiddleware
{
    private readonly RequestDelegate _next;
    
    public SomeMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("SomethingMiddleware In");
        await _next(context);
        Console.WriteLine("SomethingMiddleware Out");
    }
}
public static class SomethingMiddlewareExtensions
{
    public static IApplicationBuilder UseSomething(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SomeMiddleware>();
    }
}