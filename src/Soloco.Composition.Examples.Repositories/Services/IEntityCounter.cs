
namespace Soloco.Composition.Example.Repositories.Services
{
    [Implementation(typeof(EntityCounter<>))]
    public interface IEntityCounter<TEntity>
        where TEntity : class, new()
    {
        int Count();
    }
}