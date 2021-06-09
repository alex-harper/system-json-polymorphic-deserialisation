using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolymorphicDeserialisationDemo
{
    /// <summary>
    /// Cloned from https://stackoverflow.com/questions/58074304/is-polymorphic-deserialization-possible-in-system-text-json/59744873#59744873
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeDiscriminatorConverter<T> : JsonConverter<T>
    {
        private readonly IEnumerable<Type> _types;
        private readonly Dictionary<string, Type> _typeMap;
        private readonly string _typeDiscriminatorName;
        private readonly string _typeJsonPropertyName;

        public TypeDiscriminatorConverter(Expression<Func<T, object>> propertySelector)
        {
            var type = typeof(T);
            _types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .ToList();

            _typeJsonPropertyName = PropertyHelper.GetMemberJsonName(propertySelector);

            _typeDiscriminatorName = PropertyHelper.GetMemberName(propertySelector);

            _typeMap = _types.ToDictionary(x => x.GetProperty(_typeDiscriminatorName)
                                                .GetValue(Activator.CreateInstance(x))
                                                .ToString(), x => x);
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                if (!jsonDocument.RootElement.TryGetProperty(_typeJsonPropertyName, out var typeProperty))
                {
                    throw new JsonException();
                }

                var rawValue = typeProperty.ToString();
                var type = _typeMap[rawValue];
                
                if (type == null)
                {
                    throw new JsonException();
                }

                var jsonObject = jsonDocument.RootElement.GetRawText();
                var result = (T)JsonSerializer.Deserialize(jsonObject, type, options);

                return result;
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (object)value, options);
        }
    }
}
