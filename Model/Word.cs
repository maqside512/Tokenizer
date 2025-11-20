using System.Xml.Serialization;

namespace Tokenizer.Model
{
    public class Word : Token
    {
        [XmlAttribute("Value")]
        public string Value { get; set; }

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