using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Soloco.Composition.Construction
{
    internal static class ThisFieldSetterGenerator
    {
        public static ThisFieldSetter Generate(FieldInfo field)
        {
            CheckField(field);

            var dynamicMethod = CreateMethod(field);
            EmitSetFieldCode(field, dynamicMethod);

            return (ThisFieldSetter) dynamicMethod.CreateDelegate(typeof (ThisFieldSetter));
        }

        private static void CheckField(FieldInfo field)
        {
            if (field == null) throw new ArgumentNullException("field");

            if (field.FieldType.IsValueType) throw new NotSupportedException("Value type fields are not supported!");
            if (field.DeclaringType.IsValueType) throw new NotSupportedException("Fields of value types are not supported!");
        }

        private static DynamicMethod CreateMethod(FieldInfo field)
        {
            var methodName = "$SetField" + field.Name;
            return new DynamicMethod(
                methodName,
                typeof(void),
                new[] { typeof(object), typeof(object) },
                field.DeclaringType);
        }

        private static void EmitSetFieldCode(FieldInfo field, DynamicMethod dynamicMethod)
        {
            var generator = dynamicMethod.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, field.DeclaringType);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Castclass, field.FieldType);
            generator.Emit(OpCodes.Stfld, field);
            generator.Emit(OpCodes.Ret);
        }
    }
}