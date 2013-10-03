using System;

namespace Soloco.Composition.LinFu.IntegrationTests.Research
{
    public class ImplicitGenericThing : IGenericThing<string>
    {
        string IGenericThing<string>.Test(string lll)
        {
            throw new NotImplementedException();
        }
    }
}