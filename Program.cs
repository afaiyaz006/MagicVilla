
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MagicVilla_API.Repository;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
// Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo
//     .File("log/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger();
// Add services to the container.
// builder.Host.UseSerilog();
builder.Services.AddDbContext<ApplicationDBContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpLogging(logging =>
    {
        logging.LoggingFields = HttpLoggingFields.All;
        logging.RequestHeaders.Add("sec-ch-ua");
        logging.ResponseHeaders.Add("MyResponseHeader");
        logging.MediaTypeOptions.AddText("application/javascript");
        logging.RequestBodyLogLimit = 4096;
        logging.ResponseBodyLogLimit = 4096;
        logging.CombineLogs = true;
    }
);
builder.Services.AddSwaggerGen();
// Replace this line:
// builder.Services.AddAutoMapper(typeof(MapperConfig));

// With this line:
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MapperConfig>());
// builder.Services.AddOpenApi();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    // app.UseSwaggerUI(options =>
    // {
    //     // options.SwaggerEndpoint("/openapi/v1.json", "VillaAPI"); 
    //     
    // });swagger using openapi json
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic Villa V1");
        options.RoutePrefix = string.Empty;
    });
  
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.UseHttpLogging();
app.Run();
