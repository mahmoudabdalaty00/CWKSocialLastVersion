using API.Options;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//Add Api Version
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;


    //config.ApiVersionReader = new QueryStringApiVersionReader();   to read query anfd get verion from it 
    //config.ApiVersionReader = new HeaderApiVersionReader("x-api-version"); to read version from header

    config.ApiVersionReader = new UrlSegmentApiVersionReader(); // to read version from url segment like api/v1/controller

})
    .AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";// Formats version as 'v1', 'v1.1', etc.
    options.SubstituteApiVersionInUrl = true;// This fixes the {version} in Swagger
});

builder.Services.ConfigureOptions<ConfigurationSwaggerOptions>();
//SwaggerGen
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    //Swagger 
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        //// Add both endpoints to the "Select a definition" dropdown
        //options.SwaggerEndpoint("/swagger/v1/swagger.json", "CWSocial API v1");
        //options.SwaggerEndpoint("/swagger/v2/swagger.json", "CWSocial API v2");

        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.ApiVersion.ToString());

        }
        ;

    });
   
}


app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
