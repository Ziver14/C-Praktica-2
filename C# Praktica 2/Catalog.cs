using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace C__Praktica_2
{
    public class Catalog
    {
        public List<Product> products = new List<Product>();
        public void AddProduct(Product product) {  products.Add(product); }
        public void RemoveProduct(int id) 
        {
            products.RemoveAll(p => p.Id == id);
        }
        public void UpdateProduct(Product product)
        {
            var existing = products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null) 
            {
                existing.Name = product.Name;
                existing.Price = product.Price;
                existing.Category = product.Category;
            }
        }
        public void SaveToJson(string path)
        {
            string json = JsonConvert.SerializeObject(products);
            File.WriteAllText(path, json);
        }

        public void LoadFromJson(string path)
        {
            string json = File.ReadAllText(path);
            products = JsonConvert.DeserializeObject<List<Product>>(json);
        }
        public void SaveToXml(string path) 
        {
            XElement xml = new XElement("Products",
                from product in products select new XElement("Product", 
                new XAttribute("Id",product.Id),
                new XAttribute("Name",product.Name),
                new XAttribute("Price",product.Price),
                new XAttribute("Category",product.Category)));
            xml.Save(path);
        }
        public void LoadFromXml(string path) 
        {
            var culture = System.Globalization.CultureInfo.InvariantCulture;
            XDocument xml = XDocument.Load(path);
            products = xml.Descendants("Product")
                .Select(p=> new Product
                {
                    Id = int.Parse(p.Attribute("Id").Value),
                    Name = p.Attribute("Name").Value,
                    Price = double.Parse(p.Attribute("Price").Value,culture),
                    Category = p.Attribute("Category").Value
                }).ToList();


        }

        public IEnumerable<Product> FiltrProductsByCategory(string category)
        {
            return products.Where(p => p.Category.ToLower() == category.ToLower());
        }

        public IEnumerable<Product> FiltrProductsByPrice(double min, double max)
        {
            return products.Where(p => p.Price>=min && p.Price<=max);
        }

        public IEnumerable<Product> SortedByPrice()
        {
            return products.OrderBy(p=> p.Price);
        }
        public IEnumerable<Product> SortedByName()
        {
            return products.OrderBy(p => p.Name);
        }

    }

}
