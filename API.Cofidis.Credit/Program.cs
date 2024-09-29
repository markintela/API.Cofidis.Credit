using Cofidis.Data.Interfaces;
using Cofidis.Data.Repository;
using Cofidis.Services.Interfaces;
using Cofidis.Services.Services;
using Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//Services
builder.Services.AddScoped<ICreditRepository, CreditRepository>();


//Repositories
builder.Services.AddScoped<ICreditService, CreditService>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();

// SQL Database Configuration
var database = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(database);
});


var app = builder.Build();

// Swagger configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
