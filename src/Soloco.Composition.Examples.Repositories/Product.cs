using System.Collections.Generic;

namespace Soloco.Composition.Example.Repositories
{
    public class Product
    {
        public virtual int Id { get; private set; }
        public virtual string Name { get; set; }
        public virtual double Price { get; set; }
        public virtual IList<Store> StoresStockedIn { get; private set; }

        public Product()
        {
            StoresStockedIn = new List<Store>();
        }
    }
}