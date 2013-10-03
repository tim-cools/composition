
namespace Soloco.Composition.Example.Repositories.Services
{
    public interface IProductRepository : IRepository,
                                          IEntityByIdRetriever<Product, int>,
                                          IAllEntiyRetriever<Product>,
                                          IEntityCounter<Product>
    {
    }
}