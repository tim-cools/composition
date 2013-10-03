
namespace Soloco.Composition.Example.Repositories.Services
{
    public interface IEmployeeRepository : IRepository,
                                           IEmployeeQuerier,
                                           IAllEntiyRetriever<Employee>,
                                           IEntityCounter<Employee>,
                                           IEntityPersister<Employee>,
                                           IEntityDeleter<Employee, int>
    {
    }
}