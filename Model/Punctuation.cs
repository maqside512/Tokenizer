namespace Tokenizer.Model
{
    public class Punctuation: Tokenizer
    {
        public char Symbol { get; }

        public Punctuation(char symbol)
        {
            Symbol = symbol;
        }

        public override string GetValue() => Symbol.ToString();
    }
}