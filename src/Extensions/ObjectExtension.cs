using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSharpCore.Extensions
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class ObjectExtension
    {
        /// <summary>
        /// Gets the attribute type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this object value) where T : Attribute
        {
            var typeInfo = value.GetType().GetTypeInfo();

            var objValue = typeInfo.DeclaredMembers.FirstOrDefault(x => x.Name == value.ToString());

            return objValue?.GetCustomAttribute<T>();
        }

        /// <summary>
        /// Gets the attribute type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAttributesOfType<T>(this object value) where T : Attribute
        {
            var typeInfo = value.GetType().GetTypeInfo();

            var objValue = typeInfo.DeclaredMembers.Where(x => x.Name == value.ToString());

            return objValue?.Select(obj =>  obj?.GetCustomAttribute<T>());
        }
    }
}
