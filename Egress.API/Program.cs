using Egress.API.Middlewares;
using Egress.Infra.CrossCutting.IoC;
using Egress.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

#region Constants
const string EGRESS_CONNECTION_STRING = "EgressDb";
const string HEALTH_CHECK_ROUTE = "/health";
const string STATIC_FILE_REQUEST_PATH = "/archives";
const string STATIC_FILE_LOCAL_FILES_PATH = "files";
#endregion

var builder = WebApplication.CreateBuilder(args);

// Logging Configuration
builder.Logging.SerilogConfiguration(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// EF Configuration
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString(EGRESS_CONNECTION_STRING)!);

// Versioning Configuration
builder.Services.AddApiVersioningConfiguration(builder.Configuration);

// Healt Check Configuration
builder.Services.AddHealthChecks();

// Clear .NET Built-in Validator (in the action inside the controller)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Register services
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure static files
var completePath = Path.Combine(Path.GetPathRoot(Directory.GetCurrentDirectory())!, STATIC_FILE_LOCAL_FILES_PATH);

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(completePath),
    RequestPath = STATIC_FILE_REQUEST_PATH,
    EnableDefaultFiles = true
});

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks(HEALTH_CHECK_ROUTE);

app.MapControllers();

app.Run();