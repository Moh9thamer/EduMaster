using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;


public class GlobalExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext ctx)
    {
        try
        {
            await next(ctx);
        }
        catch (AppException ex)
        {
            ctx.Response.StatusCode = ex.StatusCode;
            await WriteProblem(ctx, ex.StatusCode, ex.Message);
        }
        catch (Exception)
        {
            ctx.Response.StatusCode = 500;
            await WriteProblem(ctx, 500, "An unexpected error occurred.");
        }
    }

    private static Task WriteProblem(HttpContext ctx, int status, string title, string? detail = null)
    {
        ctx.Response.ContentType = "application/problem+json";
        return ctx.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail
        });
    }
}
