using Tokenizer.Model;

namespace Tokenizer.Model
{
    public class Word : Token
    {
        public string Value { get; }

        public Word(string Value)
        {
            this.Value = Value;
        }

        public override string GetValue() => Value;

        public int Length => Value.Length;
    }
}