using Admin.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Scans the assembly for every IWebApplicationBuilderRegister (MvcRegister,
// AuthenticationRegister, SessionRegister, ...) and runs RegisterServices() on each.
// Add a new registrar class anywhere in the project and it's picked up automatically.
builder.RegisterServices(typeof(Program));

var app = builder.Build();

// Scans the assembly for every IWebApplicationRegister (MvcPipelineRegister,
// AuthenticationRegister, SessionRegister, ...) and runs RegisterPipelineComponents() on each.
app.RegisterPipelineComponents(typeof(Program));

app.Run();