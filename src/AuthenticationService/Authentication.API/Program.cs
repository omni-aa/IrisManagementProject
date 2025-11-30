using Authentication.API.DbContexts;
using Common.Logging;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Debug NLog setup
Console.WriteLine("=== NLOG SETUP ===");
Console.WriteLine($"Working Directory: {Directory.GetCurrentDirectory()}");

LoggingService.ConfigureLogging(builder);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options => 
{
    options.AddDefaultPolicy(policy => 
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 21))));

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("=== APPLICATION STARTED ===");
logger.LogInformation("Log directory: {CurrentDirectory}", Directory.GetCurrentDirectory());

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseRouting();
app.UseCors();
app.MapControllers();
app.MapGet("/", () => "✅ Authentication.API Server is running!");

// Test with logger instead of Console
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        var canConnect = dbContext.Database.CanConnect();
        logger.LogInformation("Database connection: {Status}", (canConnect ? "SUCCESS" : "FAILED"));
        
        if (canConnect)
        {
            var userCount = dbContext.Users.Count();
            logger.LogInformation("Database is ready with {UserCount} users", userCount);
        }
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Database connection error");
    }
}

app.UseHttpsRedirection();
app.Lifetime.ApplicationStopping.Register(LoggingService.Shutdown);

logger.LogInformation("=== STARTING SERVER ===");
app.Run();