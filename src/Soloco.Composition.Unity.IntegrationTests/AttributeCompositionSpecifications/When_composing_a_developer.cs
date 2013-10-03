using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Soloco.Composition.Unity;

namespace Soloco.Composition.Unity.IntegrationTests.AttributeCompositionSpecifications
{
    [TestClass]
    public class When_composing_a_developer
    {
        [TestMethod]
        public void given_a_part_is_injected_then_developer_should_speak_and_develop()
        {
            const string someMessage = "This is some message";

            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create(behaviorChecker.Object);

            //Let create a developer
            var developer = container.Compose<IDeveloper>();

            //Assert
            Assert.IsNotNull(developer);

            developer.Say(someMessage);
            behaviorChecker.Verify(p => p.HasSayed(someMessage), Times.Once());

            developer.Develop();
            behaviorChecker.Verify(p => p.HasDeveloped(), Times.Once());
        }

        [TestMethod]
        public void given_a_part_exists_then_developer_should_speak_and_develop()
        {
            const string someMessage = "This is some message";

            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create();

            //Let create a developer
            var developer = container.Compose<IDeveloper>(behaviorChecker.Object);

            //Assert
            Assert.IsNotNull(developer);

            developer.Say(someMessage);
            behaviorChecker.Verify(p => p.HasSayed(someMessage), Times.Once());

            developer.Develop();
            behaviorChecker.Verify(p => p.HasDeveloped(), Times.Once());
        }

        [TestMethod]
        public void given_it_is_partial_overriden_and_a_part_is_injected_then_developer_should_speak_and_develop()
        {
            const string someMessage = "This is some message";

            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create(behaviorChecker.Object);

            //Let's turn it in a developer
            var developer = container.Compose<IDeveloperPartialOverride>();

            //Assert
            Assert.IsNotNull(developer);

            developer.Say(someMessage);
            behaviorChecker.Verify(p => p.HasSayedOverride(someMessage), Times.Once());

            developer.Develop();
            behaviorChecker.Verify(p => p.HasDeveloped(), Times.Once());
        }

        [TestMethod]
        public void given_it_is_partial_overriden_and_a_part_exists_then_developer_should_speak_and_develop()
        {
            const string someMessage = "This is some message";

            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create();

            //Let's turn it in a developer
            var developer = container.Compose<IDeveloperPartialOverride>(behaviorChecker.Object);

            //Assert
            Assert.IsNotNull(developer);

            developer.Say(someMessage);
            behaviorChecker.Verify(p => p.HasSayedOverride(someMessage), Times.Once());

            developer.Develop();
            behaviorChecker.Verify(p => p.HasDeveloped(), Times.Once());
        }

        [TestMethod]
        public void given_it_is_full_overriden_and_a_part_is_injected_then_developer_should_speak_and_develop()
        {
            const string someMessage = "This is some message";

            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create(behaviorChecker.Object);

            //Let's turn it in a developer
            var developer = container.Compose<IDeveloperFullOverride>();

            //Assert
            Assert.IsNotNull(developer);

            developer.Say(someMessage);
            behaviorChecker.Verify(p => p.HasSayedOverride(someMessage), Times.Once());

            developer.Develop();
            behaviorChecker.Verify(p => p.HasDevelopedOverride(), Times.Once());
        }

        [TestMethod]
        public void given_it_is_full_overriden_and_a_part_exists_then_developer_should_speak_and_develop()
        {
            const string someMessage = "This is some message";

            var behaviorChecker = new Mock<IPersonBehaviorChecker>();

            //Create the container
            var container = UnityContainerFactory.Create();

            //Let's turn it in a developer
            var developer = container.Compose<IDeveloperFullOverride>(behaviorChecker.Object);

            //Assert
            Assert.IsNotNull(developer);

            developer.Say(someMessage);
            behaviorChecker.Verify(p => p.HasSayedOverride(someMessage), Times.Once());

            developer.Develop();
            behaviorChecker.Verify(p => p.HasDevelopedOverride(), Times.Once());
        }
    }
}