using api.Model;

namespace api.Repository
{
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
}