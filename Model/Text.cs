using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tokenizer.Model
{
    [XmlRoot("Text")]
    public class Text
    {
        [XmlElement("Sentence")]
        public List<Sentence> Sentences { get; } = new List<Sentence>();

        public void AddSentence(Sentence sentence)
        {
            Sentences.Add(sentence);
        }

        public override string ToString()
        {
            return string.Join(" ", Sentences.Select(s => s.ToString()));
        }
    }
}