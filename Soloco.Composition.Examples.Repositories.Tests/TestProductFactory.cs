using System.Collections.Generic;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    public static class TestProductFactory
    {
        public static IEnumerable<Product> CreateList()
        {
            return new List<Product>
                       {
                           new Product { Name = "Product1", Price = 15}, 
                           new Product { Name = "Product2", Price = 20}
                       };
        }
        
        public static Product Create()
        {
            return new Product { Name = "Product1", Price = 15};
        }
    }
}