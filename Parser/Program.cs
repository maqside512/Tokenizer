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
            string testText = "Привет и мир! Как дела? Я хорошо. Это тестовый текст и это просто проверка.";
            
            Text text = TextParser.Parse(testText);
            Console.WriteLine("Исходный текст:");
            Console.WriteLine(text);
            Console.WriteLine();
            
            var byWordCount = TextProcessor.SortSentencesByWordCount(text);
            Console.WriteLine("Предложения отсортированы по количеству слов:");
            byWordCount.PrintSentences();
            Console.WriteLine();
            
            var byLength = TextProcessor.SortSentencesByLength(text);
            Console.WriteLine("Предложения отсортированы по длине:");
            byLength.PrintSentences();
            Console.WriteLine();
            
            var questionWords = TextProcessor.FindWordsInQuestions(text, 3);
            Console.WriteLine("Слова из 3 букв в вопросительных предложениях:");
            foreach (var word in questionWords)
            {
                Console.WriteLine($" - {word.Value} (длина: {word.Length})");
            }
            Console.WriteLine();
            
            var filteredText = TextProcessor.RemoveWordsStartingWithConsonant(text, 4);
            Console.WriteLine("После удаления слов из 4 букв, начинающихся с согласной:");
            Console.WriteLine(filteredText);
            Console.WriteLine();
            
            var replacedText = TextProcessor.ReplaceWordsInSentence(text, 1, 3, "***");
            Console.WriteLine("После замены слов из 3 букв во втором предложении:");
            Console.WriteLine(replacedText);
            Console.WriteLine();
            
            // ТЕСТ СТОП-СЛОВ
            string[] possiblePaths = {
                "stopwords.txt",
                "Parser/stopwords.txt",
                "../Parser/stopwords.txt",
                "../../Parser/stopwords.txt"
            };

            string foundPath = null;
            foreach (var path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    foundPath = path;
                    break;
                }
            }

            if (foundPath != null)
            {
                var stopWordsText = TextProcessor.RemoveStopWords(text, foundPath);
                Console.WriteLine("После удаления стоп-слов:");
                Console.WriteLine(stopWordsText);
            }
            else
            {
                Console.WriteLine("Файл стоп-слов не найден. Создаем тестовый...");
                File.WriteAllText("stopwords.txt", "и\nя\nэто\nкак\nна\nне\nв\nс\nу\nо\nпри\nпросто");
                var stopWordsText = TextProcessor.RemoveStopWords(text, "stopwords.txt");
                Console.WriteLine("После удаления стоп-слов:");
                Console.WriteLine(stopWordsText);
            }
            Console.WriteLine();
            
            string xmlFilePath = "text.xml";
            XmlTextSerializer.SerializeToFile(text, xmlFilePath);
            Console.WriteLine($"Текст сериализован в {xmlFilePath}");
            
            Text deserializedText = XmlTextSerializer.DeserializeFromFile(xmlFilePath);
            Console.WriteLine("Десериализованный текст:");
            Console.WriteLine(deserializedText);
            Console.WriteLine();
            
            File.WriteAllText("output.txt", text.ToString());
            Console.WriteLine("Текст сохранен в output.txt");
        }
    }
}