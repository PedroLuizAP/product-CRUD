using api.Data;
using api.Dto;
using api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

var configuration = app.Configuration;

app.MapGet("/product/{id}", ([FromRoute] long id, DataContext context) =>
{
    var product = context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

    if (product == null) return Results.NotFound();

    return Results.Ok(product);
});

app.MapPost("/product", (ProductDto productDto, DataContext context) =>
{
    var category = context.Category.First(c => c.Id == productDto.CategoryId);

    var product = new Product()
    {
        Name = productDto.Name,
        Category = category,
        Description = productDto.Description,
        Code = productDto.Code
    };

    context.Products.Add(product);

    context.SaveChanges();

    return Results.Created($"/product/{product.Id}", product.Id);
});

app.MapPut("/product/{id}", ([FromRoute] long id, ProductDto productDto, DataContext context) =>
{
    var productSave = context.Products.FirstOrDefault(p => p.Id == id);

    if (productSave == null) return Results.NotFound();

    var category = context.Category.First(c => c.Id == productDto.CategoryId);

    productSave.Name = productDto.Name;
    productSave.Category = category;
    productSave.Description = productDto.Description;
    productSave.Code = productDto.Code;

    context.Products.Update(productSave);

    context.SaveChanges();

    return Results.Ok();
});

app.MapDelete("/product/{id}", ([FromRoute] long id, DataContext context) =>
{
    var product = context.Products.FirstOrDefault(p => p.Id == id);

    if (product == null) return Results.NotFound();

    context.Products.Remove(product);

    context.SaveChanges();

    return Results.Ok();
});

app.Run();