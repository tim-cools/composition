using System;
using System.CodeDom;
using System.Reflection;

namespace Soloco.Composition.Generation
{
    internal class ClassGenerator
    {
        private readonly CodeNamespace _codeNamespace;
        private readonly GenerationContext _context;
        private CodeTypeDeclaration _typeDeclaration;
        private ConstructorGenerator _constructorGenerator;

        public ClassGenerator(CodeNamespace codeNamespace, GenerationContext context)
        {
            if (codeNamespace == null) throw new ArgumentNullException("codeNamespace");
            if (context == null) throw new ArgumentNullException("context");

            _codeNamespace = codeNamespace;
            _context = context;
        }

        public void Build()
        {
            _typeDeclaration = BuildTypeDeclaration();
            _constructorGenerator = new ConstructorGenerator(_typeDeclaration);
            _constructorGenerator.Build();

            ImplementInterfaces();
        }

        private void ImplementInterfaces()
        {
            if (_context.PartInterfaces == null) throw new InvalidOperationException("_context.PartInterfaces should not be null"); 
            foreach (var @interface in _context.PartInterfaces)
            {
                ImplementInterface(@interface);
            }
        }

        private void ImplementInterface(Type @interface)
        {
            var field = CreateInterfaceField(@interface);

            ImplementInterfaceMethods(@interface, field);
            ImplementInterfaceProperties(@interface, field);
            ImplementInterfaceEvents(@interface, field);
        }

        private CodeMemberField CreateInterfaceField(Type @interface)
        {
            var reference = GetInterfaceTypeReference(@interface);
            _typeDeclaration.BaseTypes.Add(reference);
            var field = new InterfaceFieldGenerator(_typeDeclaration, reference, @interface).Build();
            _constructorGenerator.AddInterface(reference, field);
            return field;
        }

        private static CodeTypeReference GetInterfaceTypeReference(Type @interface)
        {
            return @interface.IsGenericType
                ? GetGenericInterfaceTypeReference(@interface)
                : new CodeTypeReference(@interface);
        }

        private static CodeTypeReference GetGenericInterfaceTypeReference(Type @interface)
        {
            var typeReference = new CodeTypeReference(@interface, CodeTypeReferenceOptions.GenericTypeParameter);
            foreach (var argument in @interface.GetGenericArguments())
            {
                typeReference.TypeArguments.Add(new CodeTypeReference(argument));
            }
            return typeReference;
        }

        private void ImplementInterfaceMethods(Type @interface, CodeMemberField field)
        {
            var methodResolver = new MethodResolver(@interface);
            var methods = methodResolver.Resolve();
            foreach (var method in methods)
            {
                new MethodImplementor(_typeDeclaration, field, @method).Build();
            }
        }

        private void ImplementInterfaceProperties(Type @interface, CodeMemberField field)
        {
            var resolver = new PropertyResolver(@interface);
            var properties = resolver.Resolve();
            foreach (var property in properties)
            {
                new PropertyImplementor(_typeDeclaration, field, property).Build();
            }
        }

        private void ImplementInterfaceEvents(Type @interface, CodeMemberField field)
        {
            var resolver = new EventResolver(@interface);
            var events = resolver.Resolve();
            foreach (var @event in events)
            {
                new EventImplementor(_typeDeclaration, field, @event).Build();
            }
        }

        private CodeTypeDeclaration BuildTypeDeclaration()
        {
            var typeDeclaration = new CodeTypeDeclaration(_context.ClassName)
                                      {
                                          TypeAttributes = TypeAttributes.Public
                                      };
            typeDeclaration.BaseTypes.Add(_context.CompositionType);
            _codeNamespace.Types.Add(typeDeclaration);
            return typeDeclaration;
        }
    }
}