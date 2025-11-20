namespace Tokenizer.Model
{
    public class Text
    {
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