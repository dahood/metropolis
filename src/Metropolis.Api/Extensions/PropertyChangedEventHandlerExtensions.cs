using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Metropolis.Api.Extensions
{
    public static class PropertyChangedEventHandlerExtensions
    {
        public static void Notify<MODEL, T>(this PropertyChangedEventHandler target, MODEL sender, Expression<Func<MODEL, T>> expression)
        {
            Notify(target, sender, expression.GetProperty().Name);
        }

        public static void Notify(this PropertyChangedEventHandler target, object sender, string property)
        {
            target?.Invoke(sender, new PropertyChangedEventArgs(property));
        }
    }
}