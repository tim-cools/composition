using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using Microsoft.CSharp;

namespace Soloco.Composition.Generation
{
    internal class CompositionTypeGenerator
    {
#if DEBUG
        private static double _totalDuration;
        private static int _totalCompilations;
#endif

        private readonly GenerationContext _context;

        public CompositionTypeGenerator(GenerationContext context)
        {
            if (context == null) throw new ArgumentNullException("context");

            _context = context;
        }

        internal void Generate()
        {
            var codeUnit = new CodeCompileUnit();

            new NamespaceGenerator(codeUnit, _context).Build();

            Compile(codeUnit);
        }

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "source")]
        private void Compile(CodeCompileUnit codeUnit)
        {
#if DEBUG
            var start = DateTime.Now;
#endif
            var compiler = GetCompiler();
            var options = GetCompilationOptions();
#if DEBUG
            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                compiler.GenerateCodeFromCompileUnit(codeUnit, writer, new CodeGeneratorOptions());
                string source = writer.GetStringBuilder().ToString();
            }
#endif       

            var results = compiler.CompileAssemblyFromDom(options, codeUnit);
            ThrowExceptionOnCompilationErrors(results.Errors);
            _context.ComposedType = results.CompiledAssembly.GetType(_context.ClassFullName);

#if DEBUG
            var durationMilliseconds = DateTime.Now.Subtract(start).TotalMilliseconds;

            _totalCompilations++;
            _totalDuration += durationMilliseconds;

            Debug.WriteLine("Duration: " + durationMilliseconds + "     Avg: " + (_totalDuration / _totalCompilations));
#endif
        }

        private void ThrowExceptionOnCompilationErrors(CompilerErrorCollection errors)
        {
            if (errors.Count == 0)
            {
                return;
            }

            var message = GetCompilationErrorMessages(errors);
            throw new InvalidOperationException(message);
        }

        private string GetCompilationErrorMessages(CompilerErrorCollection errors)
        {
            var messageBuilder = new StringBuilder();
            messageBuilder.AppendFormat(CultureInfo.InvariantCulture, "Error occured while compiling composed type '{0}'", _context.CompositionType);
            messageBuilder.AppendLine();
            foreach (CompilerError error in errors)
            {
                messageBuilder.AppendFormat(CultureInfo.InvariantCulture, "({0}, {1}) [{2}] {3}", error.Line, error.Column, error.ErrorNumber, error.ErrorText);
                messageBuilder.AppendLine();
            }
            return messageBuilder.ToString();
        }

        private CompilerParameters GetCompilationOptions()
        {
            var parameters = new CompilerParameters
                                 {
                                     WarningLevel = 4,
                                     GenerateInMemory = true,
                                     IncludeDebugInformation = false,
                                     TempFiles = { KeepFiles = false },
                                 };
            
            AddReferences(parameters.ReferencedAssemblies);
            return parameters;
        }

        private void AddReferences(StringCollection referencedAssemblies)
        {
            var resolver = new ReferencedAssembliesResolver(_context);
            IEnumerable<string> assemblies = resolver.Resolve();

            foreach (var assembly in assemblies)
            {
                referencedAssemblies.Add(assembly);
            }
        }     
        private static CodeDomProvider GetCompiler()
        {
            return new CSharpCodeProvider();
        }
    }
}