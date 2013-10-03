using System;
using System.CodeDom;

namespace Soloco.Composition.Generation
{
    internal class InterfaceFieldGenerator
    {
        private readonly CodeTypeDeclaration _typeDeclaration;
        private readonly CodeTypeReference _interfaceTypeReference;
        private readonly Type _interfaceType;

        public InterfaceFieldGenerator(CodeTypeDeclaration typeDeclaration, CodeTypeReference interfaceTypeReference, Type interfaceType)
        {
            _typeDeclaration = typeDeclaration;
            _interfaceTypeReference = interfaceTypeReference;
            _interfaceType = interfaceType;
        }

        public CodeMemberField Build()
        {
            var field = new CodeMemberField(_interfaceTypeReference, GetFieldName());
            _typeDeclaration.Members.Add(field);
            return field;
        }

        private string GetFieldName()
        {
            return new FieldNameGenerator().Get(_interfaceType);
        }
    }
}