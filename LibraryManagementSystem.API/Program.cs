using LibraryManagementSystem.Core;
using LibraryManagementSystem.Core.Interfaces;
using LibraryManagementSystem.DataAccess;
using LibraryManagementSystem.DataAccess.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<ApplicationDBContext>(
//     options => new SqlConnection(
//             builder.Configuration.GetConnectionString("macCon")
//         // Use this line if migrations are created in a different project.
//         // This tells Entity Framework where to find the migration files.
//         // b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)
//         )
//     );


// Register the generic repository with the dependency injection container
// builder.Services.AddTransient(
//     typeof(IGenericRepository<>), typeof(GenericRepository<>)
// );
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("macCon")));

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
