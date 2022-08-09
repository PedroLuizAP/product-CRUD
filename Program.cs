using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!"); //Get Hello Word example

app.MapGet("/user", () => new{ Name="Pedro Luiz", Age=22}); //Get Json return example

app.MapGet("/userHeader", (HttpResponse response) => response.Headers.Add("Name", "Pedro Luiz")); //Get Response Example

app.MapPost("/saveproduct", (Product product) => {  //post example
    return product.Id;
});

app.MapGet("/GetAllProduct", (Product product) => {  //return object example
    return product;
});

app.MapGet("/getproduct", ([FromQuery]string dateStart, [FromQuery]string dateEnd) => {  //get from query example
    return $"{dateStart}-{dateEnd}";
});

app.MapGet("/getproduct/{code}", ([FromRoute]string code) => { //get by route example
    return code;
});

app.MapGet("/getproductbyheader", (HttpRequest request) => { // get by request example
    return request.Headers["product-code"];
});



app.Run();

public class Product {
    public string Id { get; set; }
    public string Name { get; set; }
}

