using System;
using System.Collections.Generic;

namespace Metropolis.Api.Extensions
{
    public static class DictionaryExtensions
    {
        public static Target FindOrCreate<TKey, Target>(this IDictionary<TKey, Target> dictionary, TKey key, Func<Target> factory ) where Target : class
        {
            if (!dictionary.ContainsKey(key)) 
                dictionary[key] = factory();
            return dictionary[key];
        }

        public static void DoWhenItemFound<TKey, Target>(this IDictionary<TKey, Target> dictionary, TKey key, Action<Target> action)
        {
            if (!dictionary.ContainsKey(key)) return;
            action(dictionary[key]);
        }
    }
}
