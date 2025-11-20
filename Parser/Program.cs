using System;
using System.IO;
using System.Linq;
using Tokenizer.Model;

namespace Tokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string testText = "Hello world! How are you? I am fine. This is a test.";
            
            Text text = TextParser.Parse(testText);
            Console.WriteLine("Original text:");
            Console.WriteLine(text);
            Console.WriteLine();
            
            var byWordCount = TextProcessor.SortSentencesByWordCount(text);
            Console.WriteLine("Sentences sorted by word count:");
            byWordCount.PrintSentences();
            Console.WriteLine();
            
            var byLength = TextProcessor.SortSentencesByLength(text);
            Console.WriteLine("Sentences sorted by length:");
            byLength.PrintSentences();
            Console.WriteLine();
            
            var questionWords = TextProcessor.FindWordsInQuestions(text, 3);
            Console.WriteLine("3-letter words in questions:");
            foreach (var word in questionWords)
            {
                Console.WriteLine($" - {word.Value} (length: {word.Length})");
            }
            Console.WriteLine();
            
            var filteredText = TextProcessor.RemoveWordsStartingWithConsonant(text, 3);
            Console.WriteLine("After removing 3-letter words starting with consonant:");
            Console.WriteLine(filteredText);
            Console.WriteLine();
            
            var replacedText = TextProcessor.ReplaceWordsInSentence(text, 1, 3, "WORD");
            Console.WriteLine("After replacing 3-letter words in second sentence:");
            Console.WriteLine(replacedText);
            Console.WriteLine();
            
            try
            {
                var stopWordsText = TextProcessor.RemoveStopWords(text, "stopwords.txt");
                Console.WriteLine("After removing stop words:");
                Console.WriteLine(stopWordsText);
                Console.WriteLine();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Stop words file not found, skipping this test.");
                Console.WriteLine();
            }
            
            string xmlFilePath = "text.xml";
            XmlTextSerializer.SerializeToFile(text, xmlFilePath);
            Console.WriteLine($"Text serialized to {xmlFilePath}");
            
            Text deserializedText = XmlTextSerializer.DeserializeFromFile(xmlFilePath);
            Console.WriteLine("Deserialized text:");
            Console.WriteLine(deserializedText);
            Console.WriteLine();
            
            File.WriteAllText("output.txt", text.ToString());
            Console.WriteLine("Text saved to output.txt");
        }
    }
}