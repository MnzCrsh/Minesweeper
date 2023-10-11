using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MineSweeperApi;

public class Char2DArrayConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var array = (char[,])value!;
        var rows = array.GetLength(0);
        var cols = array.GetLength(1);
        writer.WriteStartArray();
        for (int i = 0; i < rows; i++)
        {
            writer.WriteStartArray();
            for (int j = 0; j < cols; j++)
            {
                writer.WriteValue(array[i, j].ToString());
            }
            writer.WriteEndArray();
        }
        writer.WriteEndArray();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var charList = new List<List<char>>();
        JArray jArray = JArray.Load(reader);
        foreach (JArray subArray in jArray.Children<JArray>())
        {
            var innerList = new List<char>();
            foreach (JValue value in subArray.Children<JValue>())
            {
                innerList.Add(value.Value!.ToString()![0]);
            }
            charList.Add(innerList);
        }

        var result = new char[charList.Count, charList[0].Count];
        for (int i = 0; i < charList.Count; i++)
        {
            for (int j = 0; j < charList[0].Count; j++)
            {
                result[i, j] = charList[i][j];
            }
        }

        return result;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(char[,]);
    }
}