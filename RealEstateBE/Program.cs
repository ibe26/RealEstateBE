using Microsoft.EntityFrameworkCore;
using RealEstateBE.Dal.Abstract;
using RealEstateBE.Dal.Concrete;
using RealEstateBE.Data;
using RealEstateBE.Service.Abstract;
using RealEstateBE.Service.Concrete;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPropertyTypeDal, PropertyTypeDal>();
builder.Services.AddScoped<IPropertyTypeService,PropertyTypeService>();
//builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(m => m.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication(); //first line should be
app.UseAuthorization();

app.MapControllers();

app.Run();
