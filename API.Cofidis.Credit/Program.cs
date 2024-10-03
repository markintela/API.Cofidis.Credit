using Cofidis.Data.Interfaces;
using Cofidis.Data.Repository;
using Cofidis.Manager.Interfaces;
using Cofidis.Manager.Managment;
using Cofidis.Manager.Mapping;
using Cofidis.Services.Interfaces;
using Cofidis.Services.Services;
using Data.DBContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//Repositories
builder.Services.AddScoped<ICreditRepository, CreditRepository>();


//Services
builder.Services.AddScoped<ICreditService, CreditService>();
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<ICreditValidatorService, CreditValidatorService>();

//Managers
builder.Services.AddScoped<ICreditManager, CreditManager>();
builder.Services.AddScoped<IHttpClientManager, HttpClientManager>();

//Mappers
builder.Services.AddAutoMapper(typeof(ClientUserMappingProfile));

// Cors to call External API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000/")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// SQL Database Configuration
var database = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(database);
});


var app = builder.Build();

// App Cors
app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
  .AllowAnyHeader());

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
