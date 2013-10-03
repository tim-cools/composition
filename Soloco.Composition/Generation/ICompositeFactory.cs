
namespace Soloco.Composition.Generation
{
    /// <summary>
    /// Creates a composition.
    /// </summary>
    public interface ICompositeFactory
    {
        /// <summary>
        /// Creates a composite.
        /// </summary>
        object Create();

        /// <summary>
        /// Creates a composite based on the specified existing part instance.
        /// </summary>
        object Compose(object existing);
    }
}