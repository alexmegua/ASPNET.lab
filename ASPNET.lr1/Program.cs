var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddXmlFile("companies.xml", optional: true, reloadOnChange: true)
    .AddIniFile("companies.ini", optional: true, reloadOnChange: true);

var app = builder.Build();

app.MapGet("/", (IConfiguration config) =>
{
    var companies = new List<Company>();

    // JSON-����
    companies.AddRange(config.GetSection("Companies").Get<List<Company>>());

    // XML-����
    var xmlCompanies = config.GetSection("Companies").GetChildren().Select(x => new Company
    {
        Name = x["Name"],
        Employees = int.Parse(x["Employees"])
    });
    companies.AddRange(xmlCompanies);

    // INI-����
    var iniCompanies = new List<Company>
    {
        new Company { Name = "Microsoft", Employees = int.Parse(config["Microsoft:Employees"]) },
        new Company { Name = "Apple", Employees = int.Parse(config["Apple:Employees"]) },
        new Company { Name = "Google", Employees = int.Parse(config["Google:Employees"]) }
    };
    companies.AddRange(iniCompanies);

    var topCompany = companies.OrderByDescending(c => c.Employees).FirstOrDefault();

    return $"������� � ��������� ������� �����������: {topCompany.Name}, {topCompany.Employees} �����������";
});

app.Run();

public class Company
{
    public string Name { get; set; }
    public int Employees { get; set; }
}
