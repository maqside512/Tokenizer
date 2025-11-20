using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Tokenizer.Model
{
    public class Sentence
    {
        [XmlElement("Word", Type = typeof(Word))]
        [XmlElement("Punctuation", Type = typeof(Punctuation))]
        public List<Token> Tokens { get; } = new List<Token>();

        public int WordCount => Tokens.Count(t => t is Word);
        public int Length => Tokens.Sum(t => t.GetValue().Length);

        public void AddToken(Token token) => Tokens.Add(token);

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int i = 0; i < Tokens.Count; i++)
            {
                result.Append(Tokens[i].GetValue());
                if (i < Tokens.Count - 1 && Tokens[i] is Word && Tokens[i + 1] is Word)
                {
                    result.Append(" ");
                }
            }
            return result.ToString();
        }
    }
}