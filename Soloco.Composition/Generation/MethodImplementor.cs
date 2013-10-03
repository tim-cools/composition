using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;

namespace Soloco.Composition.Generation
{
    internal class MethodImplementor
    {
        private readonly CodeTypeDeclaration _typeDeclaration;
        private readonly CodeMemberField _field;
        private readonly MethodInfo _invokingMethod;
        private readonly Type _interfaceType;
        private CodeMemberMethod _generatedMethod;

        public MethodImplementor(
            CodeTypeDeclaration typeDeclaration, 
            CodeMemberField field, 
            MethodInfo method)
        {
            _typeDeclaration = typeDeclaration;
            _field = field;
            _invokingMethod = method;
            _interfaceType = method.DeclaringType;
        }

        public void Build()
        {
            CreateMethod();
            AddParameters();
            AddGenericArguments();
            AddMethodInvokation();
        }

        private void AddGenericArguments()
        {
            foreach (var argument in _invokingMethod.GetGenericArguments())
            {
                AddGenericArgument(argument);
            }
        }

        private void AddGenericArgument(Type argument)
        {
            _generatedMethod.TypeParameters.Add(
                new CodeTypeParameter(argument.Name));
        }

        private void AddParameters()
        {
            foreach (var method in _invokingMethod.GetParameters())
            {
                AddParameter(method);
            }
        }

        private void AddParameter(ParameterInfo parameter)
        {
            if (parameter.IsOut)
            {
                AddOutParameter(parameter);
            }
            else if (parameter.ParameterType.IsByRef)
            {
                AddRefParameter(parameter);
            }
            else
            {
                _generatedMethod.Parameters.Add(
                    new CodeParameterDeclarationExpression(
                        GetParameterType(parameter.ParameterType),
                        parameter.Name));
            }
        }

        private static CodeTypeReference GetParameterType(Type parameterType)
        {
            return parameterType.IsGenericParameter
               ? GetGenericParameterType(parameterType)
               : new CodeTypeReference(parameterType);
        }

        private static CodeTypeReference GetGenericParameterType(Type parameterType)
        {
            return new CodeTypeReference(parameterType.Name, CodeTypeReferenceOptions.GenericTypeParameter);
        }

        private void AddRefParameter(ParameterInfo parameter)
        {
            var parameterDeclarationExpression = new CodeParameterDeclarationExpression(
                GetParameterType(parameter.ParameterType.GetElementType()),
                parameter.Name) { Direction = FieldDirection.Ref };
            _generatedMethod.Parameters.Add(
                parameterDeclarationExpression);
        }

        private void AddOutParameter(ParameterInfo parameter)
        {
            var parameterDeclarationExpression = new CodeParameterDeclarationExpression(
                GetParameterType(parameter.ParameterType.GetElementType()),
                parameter.Name) { Direction = FieldDirection.Out };
            _generatedMethod.Parameters.Add(
                parameterDeclarationExpression);
        }

        private void CreateMethod()
        {
            _generatedMethod = new CodeMemberMethod
            {
                PrivateImplementationType = new CodeTypeReference(GetInterfaceTypeName()),
                Name = _invokingMethod.Name,
                ReturnType = GetReturnType(),
                Attributes = MemberAttributes.Private,
            };
            _typeDeclaration.Members.Add(_generatedMethod);
        }

        private CodeTypeReference GetReturnType()
        {
            var type = _invokingMethod.ReturnType;
            return !type.IsGenericParameter 
                ? new CodeTypeReference(type) 
                : new CodeTypeReference(type.Name, CodeTypeReferenceOptions.GenericTypeParameter);
        }

        private string GetInterfaceTypeName()
        {
            return new CodeClassNameFormatter(_interfaceType).GetFullClassName();
        }

        private void AddMethodInvokation()
        {
            var buildInvokeStatement = BuildInvokeStatement();
            if (_invokingMethod.ReturnType == typeof(void))
            {
                _generatedMethod.Statements.Add(buildInvokeStatement);
            }
            else
            {
                _generatedMethod.Statements.Add(
                    new CodeMethodReturnStatement(
                        buildInvokeStatement));
            }
        }

        private CodeExpression BuildInvokeStatement()
        {
            var invokeStatement = new CodeMethodInvokeExpression(
                new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(),
                    _field.Name
                    ),
                _invokingMethod.Name,
                GetParameterExpressions());
            AddInvokeStatementsGenericParameters(invokeStatement);
            return invokeStatement;
        }

        private void AddInvokeStatementsGenericParameters(CodeMethodInvokeExpression invokeStatement)
        {
            var arguments = _invokingMethod.GetGenericArguments();
            foreach (var type in arguments)
            {
                var typeReference = new CodeTypeReference(type.Name, CodeTypeReferenceOptions.GenericTypeParameter);
                invokeStatement.Method.TypeArguments.Add(typeReference);
            }
        }

        private CodeExpression[] GetParameterExpressions()
        {
            var parameters = new List<CodeExpression>();
            foreach (var parameter in _invokingMethod.GetParameters())
            {
                var parameterExpression = GetParameterExpression(parameter);
                parameters.Add(parameterExpression);
            }
            return parameters.ToArray();
        }

        private static CodeExpression GetParameterExpression(ParameterInfo parameter)
        {
            var parameterExpression = new CodeArgumentReferenceExpression(parameter.Name);
            if (parameter.IsOut)
            {
                return new CodeDirectionExpression(FieldDirection.Out, parameterExpression);
            }
            if (parameter.ParameterType.IsByRef)
            {
                return new CodeDirectionExpression(FieldDirection.Ref, parameterExpression);
            }
            return parameterExpression;
        }
    }
}