using System.CodeDom;

namespace Soloco.Composition.Generation
{
    internal class NamespaceGenerator
    {
        private readonly CodeCompileUnit _unit;
        private readonly GenerationContext _context;

        public NamespaceGenerator(CodeCompileUnit unit, GenerationContext context)
        {
            _unit = unit;
            _context = context;
        }

        public void Build()
        {
            var codeNamespace = new CodeNamespace(_context.ClassNamespace);
            _unit.Namespaces.Add(codeNamespace);

            new ClassGenerator(codeNamespace, _context).Build();
        }
    }
}