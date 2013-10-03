using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Soloco.Composition.UnitTests
{
    ///<summary>
    /// Provides the methods to enforce Arrange Act Assert tests.
    ///</summary>
    [TestClass]
    public abstract class UnitSpecification
    {
        /// <summary>
        /// Gets the exception thrown by Act(). Use ExpectException to 
        /// </summary>
        protected Exception Exception { get; private set; }

        /// <summary>
        /// Initializes the context of the test and calls the Act method.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "All exceptions are catched and stored in the Exception member to get a better AAA experience.")]
        [TestInitialize]
        public void Setup()
        {
            Arrange();
            Act();            
        }

        /// <summary>
        /// Cleans up any resources that used in the test.
        /// </summary>
        [TestCleanup]
        public void TestCleanUp()
        {
            CleanUp();
        }

        /// <summary>
        /// Executes an action when the exception.
        /// </summary>
        /// <param name="action">The action.</param>
        protected void ExpectException(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Exception = ex;
            }
            Assert.IsNotNull(Exception, "The act of {0} expects an exception.", GetType().Name);
        }

        /// <summary>
        /// Cleans up any resources that used in the test.
        /// </summary>
        protected virtual void CleanUp()
        {
        }

        /// <summary>
        /// Arranges the context of you test.
        /// </summary>
        protected virtual void Arrange() { }

        /// <summary>
        /// Performs actions on the subject under test.
        /// </summary>
        protected abstract void Act();
    }
}