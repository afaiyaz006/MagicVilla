
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
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
