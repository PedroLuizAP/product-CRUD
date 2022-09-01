using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
/*
app.MapGet("/", () => "Hello World!"); //Get Hello Word example

app.MapGet("/user", () => new { Name = "Pedro Luiz", Age = 22 }); //Get Json return example

app.MapGet("/userHeader", (HttpResponse response) => response.Headers.Add("Name", "Pedro Luiz")); //Get Response Example


//app.MapGet("/GetAllProduct", (Product product) => {  //return object example
//  return product;
//});

app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{  //get from query example
    return $"{dateStart}-{dateEnd}";
});

app.MapGet("/getproductbyheader", (HttpRequest request) =>
{ // get by request example
    return request.Headers["product-code"];
});
*/

app.MapGet("/getproduct/{id}", ([FromRoute] string id) =>
{ //get by route example
    var product = ProductRepository.GetById(id);
    return product;
});


app.MapPost("/saveproduct", (Product product) =>
{  //post example
    ProductRepository.Add(product);
});

app.MapPut("/updateproduct", (Product product) =>
{
    var productSave = ProductRepository.GetById(product.Id);

    if (productSave != null)
        productSave.Name = product.Name;
});

app.MapDelete("/deleteproduct/{id}", ([FromRoute] string id) =>
{ //get by route example
    var product = ProductRepository.GetById(id);

    if (product != null)
        ProductRepository.Delete(product);
});

app.Run();

public static class ProductRepository
{
    public static List<Product> Products { get; set; }

    public static void Add(Product product)
    {
        if (Products == null)
            Products = new();

        Products.Add(product);
    }

    public static Product? GetById(string id)
    {
        return Products.FirstOrDefault(product => product.Id == id);
    }

    public static void Delete(Product product)
    {
        Products.Remove(product);
    }
}

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
}

