using System;

namespace Soloco.Composition.Container.IntegrationTests.Research
{
    public class ImplicitGenericThing : IGenericThing<string>
    {
        string IGenericThing<string>.Test(string lll)
        {
            throw new NotImplementedException();
        }
    }
}