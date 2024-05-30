using System.Data.SqlTypes;

namespace C__Praktica_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
           Catalog catalog = new Catalog();
            catalog.AddProduct(new Product { Id = 1, Name = "Moloko", Price = 78.50, Category = "MilkProduct" });
            catalog.AddProduct(new Product { Id = 2, Name = "Bread" , Price = 58.20, Category = "Bacaley" });
            catalog.AddProduct(new Product { Id = 3, Name = "Fish", Price = 45.20, Category = "FishProduct" });

            catalog.SaveToJson("products.json");
            catalog.LoadFromJson("products.json");
            foreach(var i in catalog.products) 
            { 
                Console.WriteLine($"ID: {i.Id}, Name:{i.Name},Price: {i.Price},Category: {i.Category}");
            }
            Console.WriteLine();

            catalog.SaveToXml("products.xml");
            catalog.LoadFromXml("products.xml");
            foreach (var i in catalog.products)
            {
                Console.WriteLine($"ID: {i.Id}, Name:{i.Name},Price: {i.Price},Category: {i.Category}");
            }
            Console.WriteLine();

            IEnumerable<Product> filteredProduct = catalog.FiltrProductsByCategory("MilkProduct");
            foreach (var i in filteredProduct)
            {
                Console.WriteLine($"ID: {i.Id}, Name:{i.Name},Price: {i.Price},Category: {i.Category}");
            }
            Console.WriteLine();

            IEnumerable<Product> filteredPrice = catalog.FiltrProductsByPrice(20.50,60.50);
            foreach (var i in filteredPrice)
            {
                Console.WriteLine($"ID: {i.Id}, Name:{i.Name},Price: {i.Price},Category: {i.Category}");
            }
        }

        
    }
}
