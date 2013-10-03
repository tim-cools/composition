using System;
using System.CodeDom;
using System.Reflection;
using System.Globalization;

namespace Soloco.Composition.Generation
{
    internal class EventImplementor
    {
        private readonly CodeTypeDeclaration _typeDeclaration;
        private readonly CodeMemberField _field;
        private readonly EventInfo _event;
        private readonly Type _interfaceType;
        private CodeSnippetTypeMember _generatedEvent;

        public EventImplementor(
            CodeTypeDeclaration typeDeclaration, 
            CodeMemberField field, 
            EventInfo property)
        {
            if (typeDeclaration == null) throw new ArgumentNullException("typeDeclaration");
            if (field == null) throw new ArgumentNullException("field");
            if (property == null) throw new ArgumentNullException("property");

            _typeDeclaration = typeDeclaration;
            _field = field;
            _event = property;
            _interfaceType = property.DeclaringType;
        }

        public void Build()
        {
            CreateEvent();
        }

        private void CreateEvent()
        {
            _generatedEvent = new CodeSnippetTypeMember
            {
                Name = _event.Name,
                Attributes = MemberAttributes.Private,                
                Text = BuildEventDeclaration()
            };
            _typeDeclaration.Members.Add(_generatedEvent);
        }

        private string BuildEventDeclaration()
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "event {0} {1}.{2} {{ add {{ {3}.{2} += value; }} remove {{ {3}.{2} -= value; }} }}",
                GetFullEventHandlerType(),
                GetFulleEventInterfaceType(),
                _event.Name,
                _field.Name);
        }

        private string GetFulleEventInterfaceType()
        {
            return new CodeClassNameFormatter(_interfaceType).GetFullClassName();
        }

        private string GetFullEventHandlerType()
        {
            return new CodeClassNameFormatter(_event.EventHandlerType).GetFullClassName();
        }
    }
}