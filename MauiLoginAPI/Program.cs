using MauiLoginAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register SQL Server DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Configure Kestrel to listen on all IPs at port 39551
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(39551); // Binds to all interfaces
});

// ✅ Register CORS (Allow all for development)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ✅ Register services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Swagger for development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ❗ Optional: Disable HTTPS redirection if using only HTTP
// app.UseHttpsRedirection(); // Disable if not using HTTPS

// ✅ Use CORS (MUST be before Authorization and MapControllers)
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
