
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TC.Unity.Composition.IntegrationTests.CompositionSpecifications;

namespace TC.Unity.Composition.IntegrationTests.Examples
{
    [TestClass]
    public class When_composing_a_party_animal_given_a_person_exists
    {
        [TestMethod]
        public void then_party_animal_should_dance()
        {
            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create(behaviorChecker.Object);

            //Create a person (can come from anywere)
            var person = container.Resolve<IPerson>();
            
            //Let's turn it in a party animal
            var partyAnimal = container.Compose<IPartyAnimal>(person);

            //Assert
            Assert.IsNotNull(partyAnimal);
            partyAnimal.Dance();

            behaviorChecker.Verify(p => p.HasDanced(), Times.Once());
        }

        [TestMethod]
        public void then_party_developer_should_dance_and_develop()
        {
            const string someMessage = "This is some message";

            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create(behaviorChecker.Object);

            //Create a person (can come from anywere)
            var person = container.Resolve<IPerson>();

            //Let's turn it in a party animal
            var partyDeveloper = container.Compose<IPartyDeveloper>(person);

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