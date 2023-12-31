using FreeCourse.Services.Order.Insfrastructure;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMediatR(typeof(FreeCourse.Services.Application.Handlers.CreateOrderHandler).Assembly);
builder.Services.AddScoped<ISharedIdentityService,SharedIdentityService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<OrderDbContext>(opt =>
{


    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {

        configure.MigrationsAssembly("FreeCourse.Services.Order.Insfrastructure");
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
