using api.Data;
using api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

var configuration = app.Configuration;

if (app.Environment.IsDevelopment())
    ProductRepository.Init(configuration);
else
    ProductRepository.Products = new();
    
app.MapGet("/product/{id}", ([FromRoute] long id) =>
{
    var product = ProductRepository.GetById(id);

    if (product == null) return Results.NotFound();

    return Results.Ok(product);
});

app.MapPost("/product", (Product product) =>
{
    ProductRepository.Add(product);

    return Results.Created($"/product/{product.Id}", product.Id);
});

app.MapPut("/product", (Product product) =>
{
    var productSave = ProductRepository.GetById(product.Id);

    if (productSave == null) return Results.NotFound();

    productSave.Name = product.Name;

    return Results.Ok();
});

app.MapDelete("/product/{id}", ([FromRoute] long id) =>
{
    var product = ProductRepository.GetById(id);

    if (product == null) return Results.NotFound();

    ProductRepository.Delete(product);

    return Results.Ok();
});

app.MapGet("configuration/application", (IConfiguration configuration) =>
{
    return Results.Ok(configuration["Application"]);
});

app.Run();

public static class ProductRepository
{
    public static void Init(IConfiguration configuration)
    {
        var mockProducts = configuration.GetSection("MockProducts").Get<List<Product>>();

        Products = mockProducts;
    }
    public static List<Product>? Products { get; set; }

    public static void Add(Product product)
    {
        Products?.Add(product);
    }

    public static Product? GetById(long id)
    {
        return Products?.FirstOrDefault(product => product.Id == id);
    }

    public static void Delete(Product product)
    {
        Products?.Remove(product);
    }
}

