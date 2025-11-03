using System.Dynamic;

namespace Tokenizer.Model
{
    public class Word: Tokenizer
    {
        public string Value { get; }

        public Word(int value)
        {
            Value = value;
        }

        public override string GetValue() => Value;
    }
}