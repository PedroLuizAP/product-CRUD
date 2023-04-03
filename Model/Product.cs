namespace api.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Product() { }
        public Product(string name, Category category, string description, string code)
        {
            Name = name;
            Code = code;
            Description = description;
            Category = category;
        }
    }
}
