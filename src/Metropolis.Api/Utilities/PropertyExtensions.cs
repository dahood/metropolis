using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Metropolis.Api.Utilities
{
    public static class PropertyExtensions
    {
        public static TReturnValue GetValue<T, TReturnValue>(this Expression<Func<T, TReturnValue>> property, T item)
        {
            var info = GetPropertyInfo(item, property);
            return (TReturnValue)info.GetValue(item, new object[0]);
        }

        public static void SetValue<T, TReturnValue>(this Expression<Func<T, TReturnValue>> property, T item, TReturnValue value)
        {
            var info = GetPropertyInfo(item, property);
            info.SetValue(item, value, new object[0]);
        }

        public static TReturnValue GetValue<TSource, TReturnValue>(this PropertyInfo info, TSource source)
        {
            return (TReturnValue)info.GetValue(source, new object[0]);
        }

        public static PropertyInfo GetPropertyInfo<TSource>(this TSource source, string propertyName)
        {
            return typeof(TSource).GetProperties().Single(x => x.Name == propertyName);
        }

        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            var type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException($"Expresion '{propertyLambda}' refers to a property that is not from type {type}.");

            return propInfo;
        }
    }
}
