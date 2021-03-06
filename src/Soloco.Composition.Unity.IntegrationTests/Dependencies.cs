﻿namespace Soloco.Composition.Unity.IntegrationTests
{
    public class UnknowType
    {
        private readonly IDependency dependency;

        public UnknowType(IDependency dependency)
        {
            this.dependency = dependency;
        }

        public IDependency Dependency
        {
            get { return dependency; }
        }
    }
}