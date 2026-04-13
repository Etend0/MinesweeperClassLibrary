using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MinesweeperClassLibrary.Models;

namespace MinesweeperClassLibrary.Services.MinesweeperDAO
{
    public class CellModelJsonConverter : JsonConverter<CellModel>
    {
        /// <summary>
        /// An override of the Read method to handle deserialization of CellModel and its derived types (BombCellModel and RewardCellModel).
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override CellModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Parse the JSON object
            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                // Get the root element and determine the type based on the $type property
                var root = doc.RootElement;
                // Read the $type property to determine which class to instantiate
                string typeName = root.GetProperty("$type").GetString();
                // Use the type name to determine which class to instantiate
                Type type = typeName switch
                {
                    // Map the type name to the corresponding class type
                    "BombCellModel" => typeof(BombCellModel),
                    // Map the type name to the corresponding class type
                    "RewardCellModel" => typeof(RewardCellModel),
                    // Default to CellModel if the type is not recognized
                    _ => typeof(CellModel)
                };
                // Create an instance using the default constructor
                var cell = (CellModel)Activator.CreateInstance(type);
                // Set the properties of the instance using reflection
                foreach (var prop in type.GetProperties())
                {
                    // Check if the JSON contains the property and if it can be set
                    if (root.TryGetProperty(prop.Name, out var jsonProp) && prop.CanWrite)
                    {
                        // Deserialize the property value from JSON and set it on the instance
                        object value = JsonSerializer.Deserialize(jsonProp.GetRawText(), prop.PropertyType, options);
                        // Set the property value on the instance
                        prop.SetValue(cell, value);
                    }
                }
                // Return the deserialized instance
                return cell;
            }
        }

        /// <summary>
        /// An override to the Write method to handle serialization of CellModel and its derived types
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, CellModel value, JsonSerializerOptions options)
        {
            // Get the type of the value to determine which properties to serialize
            var type = value.GetType();
            // Start writing the JSON object and include the $type property to indicate the type of the object
            writer.WriteStartObject();
            // Write the $type property to indicate the type of the object being serialized
            writer.WriteString("$type", type.Name);
            // Write the properties of the object using reflection
            foreach (var prop in type.GetProperties())
            {
                // Check if the property can be read before attempting to get its value
                if (prop.CanRead)
                {
                    // Get the value of the property and write it to the JSON output
                    var propValue = prop.GetValue(value);
                    // Write the property name and value to the JSON output using the JsonSerializer to handle complex types
                    writer.WritePropertyName(prop.Name);
                    // Serialize the property value to JSON and write it to the output
                    JsonSerializer.Serialize(writer, propValue, prop.PropertyType, options);
                }
            }
            // End the JSON object
            writer.WriteEndObject();
        }
    }
}
