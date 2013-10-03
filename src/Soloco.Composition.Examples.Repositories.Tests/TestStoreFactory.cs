using System.Collections.Generic;

using Soloco.Composition.Example.Repositories;

namespace Soloco.Composition.Examples.Repositories.Tests
{
    public static class TestStoreFactory
    {
        public static IEnumerable<Store> CreateList()
        {
            return new List<Store>
                       {
                           new Store { Name = "Store1" }, 
                           new Store { Name = "Store2" }
                       };
        }

        public static Store Create()
        {
            return new Store { Name = "Store1" };
        }
    }
}