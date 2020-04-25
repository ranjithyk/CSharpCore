using System;

namespace CSharpCore.Utils
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class AssemblyUtility
    {
        /// <summary>
        /// Get the Assembly Name from the given type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetAssemblyName(Type type)
        {
            try
            {
                if (type == null)
                    return string.Empty;

                var assembly = System.Reflection.Assembly.GetAssembly(type);
                var assemblyName = assembly?.FullName.Split(',')[0];
                return assemblyName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

    }
}
