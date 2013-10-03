using System;
using System.Collections.Generic;

namespace Soloco.Composition.UnitTests.Generation.CompositionFactoryBuilderSpecifications
{
    public interface IParameterPart
    {
        void TestValueType(int value);
        void TestGenericType(List<double> value);
        void TestReferenceType(Exception value);
        void TestMany(
            int value1
            , int value2
            , int value3
            , int value4
            , int value5
            , int value6
            , int value7
            , int value8
            , int value9
            , int value10);
    }
}