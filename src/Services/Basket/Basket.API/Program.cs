

using Basket.API.DiscountGrpcServices;
using Basket.API.Entities;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBasketRepository, BasketRepostory>();
ConfigurationManager configuration = builder.Configuration;


builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options=>options.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));
//"GrpcSettings:DiscountUrl"
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetValue<string>("CacheSetting:ConnectionString");
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
