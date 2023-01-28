using System.Reflection;
using Catalog.Application;
using Catalog.Application.Data;
using FSH.Core.Mediator;
using FSH.Infrastructure.Logging.Serilog;
using FSH.Infrastructure.Swagger;
using FSH.Infrastructure.Validations;
using FSH.Persistence.MongoDb;

var builder = WebApplication.CreateBuilder(args);
var appName = builder.RegisterSerilog();
builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions();
builder.Services.AddMongoDbContext<CatalogDbContext>(builder.Configuration);

///
var assembly = typeof(CatalogApplicationRoot).GetTypeInfo().Assembly;
builder.Services.AddAutoMapper(assembly);
builder.Services.RegisterMediatR(assembly);
builder.Services.RegisterSwagger(appName);
builder.Services.RegisterValidators(assembly);
///
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
///
app.ConfigureSerilog();
app.ConfigureSwagger();
///
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.MapGet("/", () => "Hello From Catalog Service!");
app.Run();