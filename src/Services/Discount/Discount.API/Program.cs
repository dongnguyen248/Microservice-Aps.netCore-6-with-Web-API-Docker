using Discount.API.Data;
using Discount.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DiscountContext>(options =>
{
    var connectstring = builder.Configuration.GetValue<string>("DatabaseSettings:ConnectString");
    options.UseNpgsql(builder.Configuration.GetValue<string>("DatabaseSettings:ConnectString"));
});


builder.Services.AddTransient<IDiscountRepository, DiscountRepository>();
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
