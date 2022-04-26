using UnityEngine;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cr7Sund.EditorEnhanceTools
{
    public static class TypeExtension
    {
        public static bool IsReallyAssignableFrom(this Type type, Type otherType)
        {
            if (type.IsAssignableFrom(otherType))
                return true;
            if (otherType.IsAssignableFrom(type))
                return true;

            try
            {
                var v = Expression.Variable(otherType);
                var expr = Expression.Convert(v, type);
                return expr.Method != null && expr.Method.Name != "op_Implicit";
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get Default via type
        /// https://stackoverflow.com/a/54125660/7360004
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object GetDefault(this Type t){
            var defaultValue = typeof(TypeExtension).GetRuntimeMethod(nameof(GetDefaultGeneric), 
            new Type[]{}).MakeGenericMethod(t).Invoke(null,null);
            return defaultValue;
        }

        public static T GetDefaultGeneric<T>(){
            return default(T);
        }

    }
}