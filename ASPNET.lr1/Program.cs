using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();

var app = builder.Build();

Dictionary<int, string> LoadUsers(string filePath)
{
    var json = File.ReadAllText(filePath);
    return JsonSerializer.Deserialize<Dictionary<int, string>>(json);
}

List<string> LoadBooks(string filePath)
{
    var json = File.ReadAllText(filePath);
    return JsonSerializer.Deserialize<List<string>>(json);
}

var users = LoadUsers("users.json");
var books = LoadBooks("books.json");

app.MapGet("/Library", async context =>
{
    await context.Response.WriteAsync("Welcome to the Library!");
});

app.MapGet("/Library/Books", async context =>
{
    var json = JsonSerializer.Serialize(books);
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(json);
});

app.MapGet("/Library/Profile/{id:int?}", async context =>
{
    if (context.Request.RouteValues.TryGetValue("id", out var idValue) && idValue is string idStr && int.TryParse(idStr, out int id))
    {
        if (users.ContainsKey(id))
        {
            await context.Response.WriteAsync(users[id]);
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("User not found.");
        }
    }
    else
    {
        await context.Response.WriteAsync("Default User: You are logged in as the default user. Idk this is some info");
    }
});

app.Run();
