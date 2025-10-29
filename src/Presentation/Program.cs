using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infraestructure.Data;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add aplicationDbContext al contenedor de servicios
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<ApplicationDbContext>(options=>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(conectionString)));


// Servicios de aplicación
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReadingListService, ReadingListService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVoteService, VoteService>();

// Repositorios
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReadingListRepository, ReadingListRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
