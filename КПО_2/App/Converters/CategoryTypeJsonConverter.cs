using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bank.App.Converters
{
    public class CategoryTypeJsonConverter : JsonConverter<CategoryType>
    {
        public override CategoryType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return value?.ToLower() switch
            {
                "income" => CategoryType.Income,
                "expense" => CategoryType.Expense,
                "0" => CategoryType.Income,
                "1" => CategoryType.Expense,
                _ => throw new JsonException($"Value '{value}' is not a valid CategoryType.")
            };
        }

        public override void Write(Utf8JsonWriter writer, CategoryType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToLower());
        }
    }
}