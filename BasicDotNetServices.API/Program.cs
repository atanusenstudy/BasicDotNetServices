using BasicDotNetServices.Core.Model;
using BasicDotNetServices.Core.Validator;
using BasicDotNetServices.DAL.Class;
using BasicDotNetServices.DAL.Data;
using BasicDotNetServices.DAL.Repository;
using BasicDotNetServices.DAL.Repository.IRepository;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();


/* Adding all the validations here */
{
    builder.Services.AddTransient<IValidator<Contact>, ContactValidator>();
}


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*      Implemented In memory Database
builder.Services.AddDbContext<ContactsAPIDbContext>(options => options.UseInMemoryDatabase("ContactsDb"));
*/


// Add-Migration "Initial Migration"   :-  Creating all the necessary Class for creating table on database
// Update-Database   :- To create Database on the given Connection String
// Microsoft.EntityFrameworkCore.Too
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
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
