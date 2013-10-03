
namespace Soloco.Composition.Example.Repositories.Services
{
    public interface IStoreRepository : IRepository,
                                        IEntityByIdRetriever<Store, int>,
                                        IAllEntiyRetriever<Store>,
                                        IEntityCounter<Store>
    {
    }
}