using System;
using System.Reflection;

namespace Cr7Sund.EditorEnhanceTools
{
    #if CSHARP_7_3_OR_NEWER
    public class InternalGetter<T> where T : Delegate
    #else
    public class InternalGetter<T> where T : class
    #endif
    {
        private readonly Type type;
        private readonly string funcName;

        public InternalGetter(Type type, string funcName)
        {
            this.type = type;
            this.funcName = funcName;
        }
        
        public T Func =>
            type
                .GetMethod(funcName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .CreateDelegate(typeof(T), null) as T;
    }
}