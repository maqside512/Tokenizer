using System.Xml.Serialization;

namespace Tokenizer.Model
{
    public class Punctuation : Token
    {
        [XmlText]
        public char Symbol { get; set; }

        public Punctuation(char symbol)
        {
            Symbol = symbol;
        }

        public Punctuation()
        {
        }

        public override string GetValue()
        {
            return Symbol.ToString();
        }

        public static bool IsPunctuation(char c)
        {
            return char.IsPunctuation(c) || c == '!' || c == '?' || c == '.' || c == ',' || c == ';' || c == ':';
        }
    }
}