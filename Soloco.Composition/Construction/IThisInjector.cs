
namespace Soloco.Composition.Construction
{
    /// <summary>
    /// Injects composite parts in other composite parts based on the specified mapping.
    /// </summary>
    public interface IThisInjector
    {
        /// <summary>
        /// Injects composite parts in other composite parts based on the specified mapping.
        /// </summary>
        /// <param name="parts">The parts.</param>
        void Inject(CompositeParts parts);
    }
}