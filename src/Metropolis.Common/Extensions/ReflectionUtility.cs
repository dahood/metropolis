using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Metropolis.Common.Extensions
{
    public static class ReflectionUtility
    {
        public static string GetPropertyName<MODEL, T>(this Expression<Func<MODEL, T>> expression)
        {
            return expression.Body.ToString().TrimTo('.').TrimEnd(')');
        }

        public static PropertyInfo GetProperty<MODEL, T>(this Expression<Func<MODEL, T>> expression)
        {
            MemberExpression memberExpression = getMemberExpression(expression);
            return (PropertyInfo)memberExpression.Member;
        }

        private static MemberExpression getMemberExpression<MODEL, T>(Expression<Func<MODEL, T>> expression, bool enforceCheck = true)
        {
            MemberExpression memberExpression = null;
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var body = (UnaryExpression)expression.Body;
                memberExpression = body.Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpression = expression.Body as MemberExpression;
            }
            if (enforceCheck && memberExpression == null)
            {
                throw new ArgumentException("Not a member access", nameof(expression));
            }
            return memberExpression;
        }
    }
}