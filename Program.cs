using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/product/{id}", ([FromRoute] string id) =>
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

app.MapDelete("/product/{id}", ([FromRoute] string id) =>
{
    var product = ProductRepository.GetById(id);

    if (product == null) return Results.NotFound();

    ProductRepository.Delete(product);

    return Results.Ok();
});

app.Run();

public static class ProductRepository
{
    public static List<Product>? Products { get; set; }

    public static void Add(Product product)
    {
        if (Products == null)
            Products = new();

        Products.Add(product);
    }

    public static Product? GetById(string id)
    {
        return Products?.FirstOrDefault(product => product.Id == id);
    }

    public static void Delete(Product product)
    {
        Products.Remove(product);
    }
}

