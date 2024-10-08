using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();

var app = builder.Build();

// Middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred");

        await File.AppendAllTextAsync("errors.log", $"{DateTime.Now}: {ex.Message}\n");

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An error occurred, please try again.");
    }
});

app.MapPost("/set-cookie", async context =>
{
    var form = await context.Request.ReadFormAsync();
    var value = form["value"].ToString();
    var expiryDateTime = DateTime.Parse(form["expiry"]);

    var cookieOptions = new CookieOptions
    {
        Expires = expiryDateTime,
        HttpOnly = true
    };

    var encodedValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    context.Response.Cookies.Append("MyCookie", encodedValue, cookieOptions);

    await context.Response.WriteAsync("Cookie has been successfully saved!");
});

app.MapGet("/check-cookie", context =>
{
    if (context.Request.Cookies.TryGetValue("MyCookie", out var cookieValue))
    {
        try
        {
            var decodedValue = Encoding.UTF8.GetString(Convert.FromBase64String(cookieValue));
            return context.Response.WriteAsync($"Value in Cookies: {decodedValue}");
        }
        catch (FormatException)
        {
            return context.Response.WriteAsync("Invalid encoding in Cookies.");
        }
    }
    else
    {
        return context.Response.WriteAsync("Cookies not found.");
    }
});

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync(File.ReadAllText("index.html"));
});
app.Run();
