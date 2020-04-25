using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CSharpCore.Extensions
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class EnumExtension
    {
        /// <summary>
        /// Gets the attribute type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(this Enum value) where T : Attribute
        {
            var typeInfo = value.GetType().GetTypeInfo();

            var enumValue = typeInfo.DeclaredMembers.FirstOrDefault(x => x.Name == value.ToString());

            return enumValue?.GetCustomAttribute<T>();
        }

        /// <summary>
        /// Gets the Description the enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var attribute = GetAttributeOfType<DescriptionAttribute>(value);

            return attribute != null
                ? attribute.Description
                : string.Empty;
        }
    }
}
