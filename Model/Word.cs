using System.Xml.Serialization;

namespace Tokenizer.Model
{
    public class Word : Token
    {
        [XmlText]
        public string Value { get; }

        public Word(string value)
        {
            Value = value;
        }

        public Word()
        {
            Value = string.Empty;
        }

        public override string GetValue() => Value;

        public int Length => Value.Length;
    }
}