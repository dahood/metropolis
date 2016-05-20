using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Metropolis.Api.Extensions;
using Metropolis.Common.Extensions;

namespace Metropolis.Test.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Creates a deep copy of object by serializing to memory stream.
        /// </summary>
        /// <param name="obj" />
        public static T DeepClone<T>(this T obj) where T : class
        {
            if (obj == null)
                return default(T);
            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                memoryStream.Seek(0L, SeekOrigin.Begin);
                return (T) binaryFormatter.Deserialize(memoryStream);
            }
        }

        public static bool ReflectionEquals(this object o1, object o2, bool throwException = false)
        {
            var result = ReflectionEquals(o1, o2, new List<object>(), throwException);
            if (!result && throwException)
            {
                throw new ArgumentException($"Objects not equal {o1} and {o2}");
            }
            return result;
        }

        public static bool ReflectionEquals(object o1, object o2, List<object> objectsAlreadyCompared,
            bool throwException,
            params Type[] attributeTypesOnPropertiesToIgnore)
        {
            if (ListContainsReference(objectsAlreadyCompared, o1) || ListContainsReference(objectsAlreadyCompared, o2))
                return true;
            objectsAlreadyCompared.AddRange(new[] {o1, o2});

            if (ReferenceEquals(o1, o2)) return true;
            if (IsSystemType(o1)) return o1.Equals(o2);
            if (o1 is IEnumerable)
                return CollectionEquals((IEnumerable) o1, o2 as IEnumerable, objectsAlreadyCompared, throwException,
                    attributeTypesOnPropertiesToIgnore);

            if (o1 == null && o2 == null)
            {
                return true;
            }
            if (o1 == null || o2 == null) return false;

            if (o1.GetType() != o2.GetType()) return false;

            if (o1.GetType().GetProperties().FirstOrDefault(x => x.Name == "HibernateLazyInitializer") != null)
            {
                //swap the two objects so that nhibernate one is last since it contains more properties that we shouldn't be checking
                var o3 = o1;
                o1 = o2;
                o2 = o3;
            }
            var propertyInfos = o1.GetType().GetProperties();
            var propertiesToCompare =
                propertyInfos.Where(
                    prop =>
                        !prop.CustomAttributes.Any(
                            attrib =>
                                attributeTypesOnPropertiesToIgnore.Any(
                                    attribToIgnore => attribToIgnore.Name == attrib.AttributeType.Name)));

            return propertiesToCompare.All(x =>
            {
                var o1Value = x.GetValue(o1, null);
                var x2 = o2.GetType().GetProperties().First(y => y.Name == x.Name);

                var o2Value = x2.GetValue(o2, null);
                if (o1Value == null && o2Value == null) return true;
                if (o1Value == null || o2Value == null)
                {
                    if (throwException)
                        throw new ArgumentException(
                            string.Format(
                                "Property {0} - Objects:\n{1}\nvs\n{2}:\nOne is null while the other isn't:\n{3}\n vs \n{4}"
                                    .FormatWith(x.Name, o1, o2,
                                        o1Value.Stringify(), o2Value.Stringify())));
                    return o2Value == null;
                }
                if (o1Value is DateTime)
                {
                    o1Value = ((DateTime) o1Value).TruncateMilliseconds();
                    o2Value = ((DateTime) o2Value).TruncateMilliseconds();
                }

                var result = o1Value is ValueType
                    ? o1Value.Equals(o2Value)
                    : ReferenceEquals(o1Value, o2Value) || o1Value.Equals(o2Value) ||
                      ReflectionEquals(o1Value, o2Value, objectsAlreadyCompared, throwException,
                          attributeTypesOnPropertiesToIgnore);

                if (!result && throwException)
                    throw new ArgumentException(
                        string.Format(
                                x.Name, o1, o2,
                                o1Value.Stringify(), o2Value.Stringify()));
                return result;
            });
        }

        private static string Stringify(this object obj)
        {
            if (obj == null) return "null";
            var os = obj as IEnumerable;
            if (os == null) return obj.ToString();
            var sb = new StringBuilder("IEnumerable Count:").Append(os.CountEnum());
            foreach (var o in os)
            {
                sb.Append("\n").Append(o.Stringify());
            }
            return sb.ToString();
        }

        private static int CountEnum(this IEnumerable enumerable)
        {
            return enumerable.Cast<object>().Count();
        }

        private static bool IsSystemType(object o1)
        {
            return o1 != null && "System".Equals(o1.GetType().Namespace) && !(o1 is IEnumerable);
        }

        private static bool ListContainsReference(IEnumerable<object> objects, object o1)
        {
            return objects.Any(o => ReferenceEquals(o, o1));
        }

        private static bool CollectionEquals(IEnumerable o1Value, IEnumerable o2Value,
            List<object> objectsAlreadyCompared, bool throwException,
            Type[] attributeTypesOnPropertiesToIgnore)
        {
            if (o1Value == null) return o2Value == null;

            if (o1Value.CountEnum() != o2Value.CountEnum())
            {
                return false;
            }

            var enumerator1 = o1Value.GetEnumerator();
            var enumerator2 = o2Value.GetEnumerator();
            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                if (ReflectionEquals(enumerator1.Current, enumerator2.Current, objectsAlreadyCompared, throwException,
                    attributeTypesOnPropertiesToIgnore))
                    continue;
                if (throwException)
                    throw new ArgumentException(string.Format("Objects inside IEnumerable not equal:\n{0}\nvs\n{1}",
                        o1Value, o2Value));
                return false;
            }
            return true;
        }

        // Currently only supporting struct, but could extend to any type
        public static bool IsAny<T>(this T target, params T[] possibleValues) where T : struct
        {
            return possibleValues.Contains(target);
        }

        public static bool IsNotAny<T>(this T target, params T[] possibleValues) where T : struct
        {
            return !target.IsAny(possibleValues);
        }

        public static TResult SafeGet<T, TResult>(this T item, Func<T, TResult> getter)
        {
            return ReferenceEquals(default(T), item) ? default(TResult) : getter(item);
        }
    }
}