using System;
using System.Collections.Generic;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IReturnTypePart
    {
        int Test1();
        List<double> Test2();
        Exception Test3();
    }
}