using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Metropolis.Common.Extensions;

namespace Metropolis.Api.Extensions
{
    public static class PropertyChangedEventHandlerExtensions
    {
        public static void Notify<TModel, T>(this PropertyChangedEventHandler target, TModel sender, Expression<Func<TModel, T>> expression)
        {
            Notify(target, sender, expression.GetProperty().Name);
        }

        public static void Notify(this PropertyChangedEventHandler target, object sender, string property)
        {
            target?.Invoke(sender, new PropertyChangedEventArgs(property));
        }
    }
}