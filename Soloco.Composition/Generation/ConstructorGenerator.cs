using System;
using System.CodeDom;

namespace Soloco.Composition.Generation
{
    internal class ConstructorGenerator
    {
        private readonly CodeTypeDeclaration _typeDeclaration;
        private CodeConstructor _constructor;

        public ConstructorGenerator(CodeTypeDeclaration typeDeclaration)
        {
            _typeDeclaration = typeDeclaration;
        }

        public void Build()
        {
            _constructor = new CodeConstructor
            {
               Attributes = MemberAttributes.Public
            };
            _typeDeclaration.Members.Add(_constructor);
        }

        public void AddInterface(CodeTypeReference @interface, CodeMemberField field)
        {
            var parameter = AddParameter(@interface, field);
            InitField(field, parameter.Name);
        }

        private void InitField(CodeTypeMember field, string parameterName)
        {
            var assignStatement = new CodeAssignStatement(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    field.Name),
                new CodeArgumentReferenceExpression(parameterName));
            _constructor.Statements.Add(assignStatement);
        }

        private CodeParameterDeclarationExpression AddParameter(CodeTypeReference @interface, CodeTypeMember field)
        {
            var parameter = new CodeParameterDeclarationExpression(@interface, field.Name);
            _constructor.Parameters.Add(parameter);
            return parameter;
        }
    }
}