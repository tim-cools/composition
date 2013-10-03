
using Iesi.Collections.Generic;

namespace Soloco.Composition.Example.Repositories
{
    public class Store
    {
        private ISet<Product> _products;
        private ISet<Employee> _staff;

        public virtual int Id { get; private set; }
        public virtual string Name { get; set; }
        
        public virtual ISet<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }

        public virtual ISet<Employee> Staff
        {
            get { return _staff; }
            set { _staff = value; }
        }

        public Store()
        {
            Products = new HashedSet<Product>();
            Staff = new HashedSet<Employee>();
        }

        public virtual void AddProduct(Product product)
        {
            product.StoresStockedIn.Add(this);
            Products.Add(product);
        }

        public virtual void AddEmployee(Employee employee)
        {
            employee.Store = this;
            Staff.Add(employee);
        }
    }
}