using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SomeNamespace.Models;
using System;

var company = new Company
{
    Name = "Tech Solutions",
    Address = "123 Main St, Tech City",
    EmployeesCount = 100
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/company", () => company);

app.MapGet("/random", () =>
{
    var random = new Random();
    int randomNumber = random.Next(0, 101);
    return randomNumber;
});

app.Run();
