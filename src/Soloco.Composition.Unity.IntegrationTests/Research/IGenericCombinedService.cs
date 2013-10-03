namespace Soloco.Composition.Unity.IntegrationTests.Research
{
    /// Combined composite service
    /// - IGenericCombinedServiceCore
    /// - IGenericPart1<TestEntity> 
    /// - IGenericPart2
    ///////////////////////////////////////

    public interface IGenericCombinedService : IGenericCombinedServiceCore, IGenericPart1<TestEntity>, INonGenericPart2 {}
}