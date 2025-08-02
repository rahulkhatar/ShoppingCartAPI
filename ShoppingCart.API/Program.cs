using Azure.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using ShoppingCart.Core;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Core.Middleware;
using ShoppingCart.Core.Services;
using ShoppingCart.Infrastucture.Data;
using ShoppingCart.Infrastucture.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbCon")));

//builder.Services.AddMediatRConfiguration();

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<ProductService>();

/*builder.Configuration.AddUserSecrets<Program>();
var keyVaultUrl = builder.Configuration["KeyVault:KeyVaultUri"];
if (!string.IsNullOrEmpty(keyVaultUrl))
{
    var credential = new DefaultAzureCredential();
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credential);
}*/

//var keyVaultUrl = builder.Configuration["KeyVault:KeyVaultUri"];
//if (!string.IsNullOrEmpty(keyVaultUrl))
//{
//    var credential = new DefaultAzureCredential();
//    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), credential);
//}

var app = builder.Build();

//app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
