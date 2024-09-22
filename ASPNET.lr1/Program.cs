var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

app.MapGet("/about", (IConfiguration config) =>
{
    var userInfo = config.GetSection("UserInfo").Get<UserInfo>();

    return $"��'�: {userInfo.Name}\n³�: {userInfo.Age}\n̳���: {userInfo.City}";
});

app.Run();

public class UserInfo
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
}
