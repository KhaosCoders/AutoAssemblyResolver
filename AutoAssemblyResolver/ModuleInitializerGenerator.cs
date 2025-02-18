﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace AutoAssemblyResolver;

[Generator]
public class ModuleInitializerGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "ModuleInitializer.g.cs",
            SourceText.From(ModuleInitializerCode, Encoding.UTF8)));
    }

    public const string ModuleInitializerCode = """

using System;
using System.IO;
using System.Reflection;

namespace System
{
    internal static class ModuleInitializer
    {
        private static bool isInitialized = false;

        /// <summary>
        /// Initializes the module.
        /// </summary>
    #if NET5_0_OR_GREATER
        [System.Runtime.CompilerServices.ModuleInitializer]
    #endif
        internal static void Run()
        {
            if (isInitialized)
            {
                return;
            }

            isInitialized = true;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static string workingDirectory = AppDomain.CurrentDomain.BaseDirectory;

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string filePath = Path.Combine(workingDirectory, new AssemblyName(args.Name).Name + ".dll");
            if (File.Exists(filePath))
            {
                return Assembly.LoadFile(filePath);
            }

            return null;
        }
    }
}

""";
}
