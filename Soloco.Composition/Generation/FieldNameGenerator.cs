using System;
using System.Text;

namespace Soloco.Composition.Generation
{
    internal class FieldNameGenerator
    {
        private StringBuilder _fieldBuilder;

        public string Get(Type @interface)
        {
            if (@interface == null) throw new ArgumentNullException("interface");

            _fieldBuilder= new StringBuilder();
            var name = @interface.FullName ?? @interface.Namespace + "_" + @interface.Name;
            foreach (var charValue in name.ToCharArray())
            {
                AppendChracter(charValue);
            }
            return _fieldBuilder.ToString();
        }

        private void AppendChracter(char charValue)
        {
            if (IsValidFieldNameCharacter(charValue, _fieldBuilder.Length == 0))
            {
                _fieldBuilder.Append(charValue);
            }
            else
            {
                _fieldBuilder.Append('_');
            }
        }

        private static bool IsValidFieldNameCharacter(char value, bool isFirstCharacter)
        {
            return IsLowerCase(value)
                || IsUpperCase(value)
                || !isFirstCharacter && IsDigit(value);
        }

        private static bool IsDigit(char value)
        {
            return value >= '0' && value <= '9';
        }

        private static bool IsUpperCase(char value)
        {
            return value >= 'A' && value <= 'Z';
        }

        private static bool IsLowerCase(char value)
        {
            return value >= 'a' && value <= 'z';
        }
    }
}