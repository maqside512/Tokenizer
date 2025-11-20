using Tokenizer.Model;

namespace Tokenizer.Model
{
    public class Sentence
    {
        public List<Token> Tokens { get; } = new List<Token>();

        public int WordCount
        {
            get
            {
                return Tokens.Count(t => t is Word);
            }
        }

        public int Length
        {
            get
            {
                return Tokens.Sum(t => t.GetValue().Length);
            }
        }

        public void AddToken(Token token)
        {
            Tokens.Add(token);
        }

        public override string ToString()
        {
            return string.Join("", Tokens.Select(t => t.GetValue()));
        }
    }
}