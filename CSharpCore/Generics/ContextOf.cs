using System;

namespace CSharpCore.Generics
{
    /// <summary>
    /// Provides genetic context
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ContextOf<T> where T : class
    {
        /// <summary>
        /// Context object
        /// </summary>
        public static T Context { get; set; }

        /// <summary>
        /// Inilialie the context object on the constructor
        /// </summary>
        static ContextOf()
        {
            Context = (T)Activator.CreateInstance(typeof(T));
        }
    }
}
