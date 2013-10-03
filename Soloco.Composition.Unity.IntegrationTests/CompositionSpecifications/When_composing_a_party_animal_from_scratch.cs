using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using Soloco.Composition.Unity;

namespace Soloco.Composition.Unity.IntegrationTests.CompositionSpecifications
{
    [TestClass]
    public class When_composing_a_party_animal_from_scratch
    {
        [TestMethod]
        public void then_party_developer_should_dance_and_develop()
        {
            const string someMessage = "This is some message";

            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create(behaviorChecker.Object);

            //Let's turn it in a party animal
            var partyDeveloper = container.Compose<IPartyDeveloper>();

            //Assert
            Assert.IsNotNull(partyDeveloper);

            partyDeveloper.Say(someMessage);
            behaviorChecker.Verify(p => p.HasSayed(someMessage), Times.Once());

            partyDeveloper.Dance();
            behaviorChecker.Verify(p => p.HasDanced(), Times.Once());

            partyDeveloper.Develop();
            behaviorChecker.Verify(p => p.HasDeveloped(), Times.Once());
        }
    }
}