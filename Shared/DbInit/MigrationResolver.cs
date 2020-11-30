using System;

namespace Shared.DbInit
{
    public static class MigrationResolver
    {
        private static Func<string> AssemblyResolver;

        public static void Setup(Func<string> assemblyResolver)
        {
            AssemblyResolver = assemblyResolver;
        }

        public static string ResolveMigrationAssembly()
        {
            return AssemblyResolver();
        }
    }
}