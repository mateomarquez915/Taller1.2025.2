using Microsoft.EntityFrameworkCore;
using Taller1.Backend.Data;
using Taller1.Backend.Repositories.Implementations;
using Taller1.Backend.Repositories.Interfaces;
using Taller1.Backend.UnitsOfWork.Implementations;
using Taller1.Backend.UnitsOfWork.Interfaces;
using Taller1.Shared.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("name=LocalConnection"));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericUnitOfWork<>), typeof(GenericsUnitOfWork<>));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();