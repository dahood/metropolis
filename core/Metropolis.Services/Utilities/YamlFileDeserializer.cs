using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Metropolis.Common.Utilities
{
    public interface IYamlFileDeserializer<T>
    {
        T Deserialize(string configFilePath);
    }

    public class YamlFileDeserializer<T> : IYamlFileDeserializer<T>
    {
        public T Deserialize(string configFilePath)
        {
            using (var reader = File.OpenText(configFilePath))
            {
                var deserializer = new DeserializerBuilder().WithNamingConvention(new CamelCaseNamingConvention()).Build();
                return deserializer.Deserialize<T>(reader);
            }
        }
    }
}