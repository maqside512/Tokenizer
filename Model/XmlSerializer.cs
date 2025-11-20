using System;
using System.IO;
using System.Xml.Serialization;

namespace Tokenizer.Model
{
    public class XmlTextSerializer
    {
        public static void SerializeToFile(Text text, string filePath)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            
            var serializer = new XmlSerializer(typeof(Text));
            
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, text);
            }
        }
        
        public static string SerializeToString(Text text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            
            var serializer = new XmlSerializer(typeof(Text));
            
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, text);
                return writer.ToString();
            }
        }
        
        public static Text DeserializeFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"XML file not found: {filePath}");
            
            var serializer = new XmlSerializer(typeof(Text));
            
            using (var reader = new StreamReader(filePath))
            {
                var result = serializer.Deserialize(reader) as Text;
                if (result == null)
                    throw new InvalidOperationException("Deserialization returned null");
                
                return result;
            }
        }
    }
}