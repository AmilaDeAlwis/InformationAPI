using InformationAPI.Impl;
using InformationAPI.interfaces;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string safely
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found in configuration.");
}

// Register interfaces and implementations
builder.Services.AddHttpClient<IManageService, ManageService>();
builder.Services.AddScoped<IManageConnection>(sp => new ManageConnection(connectionString));
builder.Services.AddScoped<IManageService, ManageService>();
builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;

        await context.Response.WriteAsJsonAsync(new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An unexpected error occurred. Please try again later."
        });
    });
});


app.UseHttpsRedirection();

// Enable serving default file (like index.html)
app.UseDefaultFiles();
app.UseStaticFiles();   // Serve static files from wwwroot

app.UseAuthorization();

app.MapControllers();

app.Run();
