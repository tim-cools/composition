using System;
using System.CodeDom;
using System.Reflection;

namespace Soloco.Composition.Generation
{
    internal class PropertyImplementor
    {
        private readonly CodeTypeDeclaration _typeDeclaration;
        private readonly CodeMemberField _field;
        private readonly PropertyInfo _property;
        private readonly Type _interfaceType;
        private CodeMemberProperty _generatedProperty;

        public PropertyImplementor(
            CodeTypeDeclaration typeDeclaration, 
            CodeMemberField field, 
            PropertyInfo property)
        {
            if (typeDeclaration == null) throw new ArgumentNullException("typeDeclaration");
            if (field == null) throw new ArgumentNullException("field");
            if (property == null) throw new ArgumentNullException("property");

            _typeDeclaration = typeDeclaration;
            _field = field;
            _property = property;
            _interfaceType = property.DeclaringType;
        }

        public void Build()
        {
            CreateProperty();
            AddIndexParameters();
            AddSetter();
            AddGetter();
        }

        private void AddIndexParameters()
        {
            foreach (var method in _property.GetIndexParameters())
            {
                AddIndexParameter(method);
            }
        }

        private void AddIndexParameter(ParameterInfo parameter)
        {
            _generatedProperty.Parameters.Add(
                    new CodeParameterDeclarationExpression(
                        GetParameterType(parameter.ParameterType),
                        parameter.Name));
        }

        private static CodeTypeReference GetParameterType(Type parameterType)
        {
            return new CodeTypeReference(parameterType);
        }

        private void CreateProperty()
        {
            _generatedProperty = new CodeMemberProperty
            {
                PrivateImplementationType = new CodeTypeReference(GetInterfaceTypeName()),
                Name = _property.Name,
                Type = GetReturnType(),
                Attributes = MemberAttributes.Private,                
                HasGet = _property.CanRead,
                HasSet = _property.CanWrite
            };
            _typeDeclaration.Members.Add(_generatedProperty);
        }

        private CodeTypeReference GetReturnType()
        {
            var type = _property.PropertyType;
            return new CodeTypeReference(type);
        }

        private string GetInterfaceTypeName()
        {
            return new CodeClassNameFormatter(_interfaceType).GetFullClassName();
        }

        private void AddGetter()
        {
            if (!_property.CanRead) return;

            _generatedProperty.GetStatements.Add(
                new CodeMethodReturnStatement(
                    BuildPropertyReferenceExpression()));

        }

        private void AddSetter()
        {
            if (!_property.CanWrite) return;

            _generatedProperty.SetStatements.Add(
                new CodeAssignStatement(
                    BuildPropertyReferenceExpression(),
                    new CodePropertySetValueReferenceExpression()));
        }

        private CodeExpression BuildPropertyReferenceExpression()
        {
            var parameters = _property.GetIndexParameters();
            if (parameters.Length != 0)
            {
                return new CodeIndexerExpression(
                    new CodeFieldReferenceExpression(
                        new CodeThisReferenceExpression(),
                        _field.Name
                        ), GetIndexerParameters(parameters));
            }
            return new CodePropertyReferenceExpression(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    _field.Name
                    ), _property.Name);
        }

        private static CodeExpression[] GetIndexerParameters(ParameterInfo[] parameters)
        {
            var expressions = new CodeExpression[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var name = parameters[i].Name;
                expressions[i] = new CodeArgumentReferenceExpression(name);
            }
            return expressions;
        }
    }
}