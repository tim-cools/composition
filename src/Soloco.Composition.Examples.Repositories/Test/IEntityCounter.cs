using Remotion.Implementation;

namespace TC.Remixing.Repositories.Core
{
    [ConcreteImplementation(typeof(EntityCounter))]
    public interface IEntityCounter
    {
        long Count();
    }
}