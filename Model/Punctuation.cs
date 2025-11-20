using Tokenizer.Model;

namespace Tokenizer.Model
{
    public class Punctuation : Token
    {
        public char Symbol { get; }

        public Punctuation(char symbol)
        {
            Symbol = symbol;
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