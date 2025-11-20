using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tokenizer.Model
{
    public class TextProcessor
    {
        public static List<Sentence> SortSentencesByWordCount(Text text)
        {
            return text.Sentences.OrderBy(s => s.WordCount).ToList();
        }

        public static List<Sentence> SortSentencesByLength(Text text)
        {
            return text.Sentences.OrderBy(s => s.Length).ToList();
        }
        
        public static List<Word> FindWordsInQuestions(Text text, int wordLength)
        {
            var result = new List<Word>();
            
            foreach (var sentence in text.Sentences)
            {
                if (IsQuestionSentence(sentence))
                {
                    var wordsInSentence = sentence.Tokens
                        .OfType<Word>()
                        .Where(word => word.Length == wordLength)
                        .DistinctBy(word => word.Value.ToLower());
                    
                    result.AddRange(wordsInSentence);
                }
            }
            
            return result;
        }
        
        public static Text RemoveWordsStartingWithConsonant(Text text, int wordLength)
        {
            var newText = new Text();
            
            foreach (var sentence in text.Sentences)
            {
                var newSentence = new Sentence();
                
                foreach (var token in sentence.Tokens)
                {
                    if (token is Word word)
                    {
                        if (word.Length == wordLength && StartsWithConsonant(word.Value))
                        {
                            continue;
                        }
                        newSentence.AddToken(word);
                    }
                    else
                    {
                        newSentence.AddToken(token);
                    }
                }
                
                newText.AddSentence(newSentence);
            }
            
            return newText;
        }
        
        public static Text ReplaceWordsInSentence(Text text, int sentenceIndex, int wordLength, string replacement)
        {
            if (sentenceIndex < 0 || sentenceIndex >= text.Sentences.Count)
                throw new ArgumentOutOfRangeException(nameof(sentenceIndex));
            
            var newText = new Text();
            
            for (int i = 0; i < text.Sentences.Count; i++)
            {
                if (i == sentenceIndex)
                {
                    var newSentence = new Sentence();
                    var sentence = text.Sentences[i];
                    
                    foreach (var token in sentence.Tokens)
                    {
                        if (token is Word word && word.Length == wordLength)
                        {
                            newSentence.AddToken(new Word(replacement));
                        }
                        else
                        {
                            newSentence.AddToken(token);
                        }
                    }
                    
                    newText.AddSentence(newSentence);
                }
                else
                {
                    newText.AddSentence(text.Sentences[i]);
                }
            }
            
            return newText;
        }
        
        public static Text RemoveStopWords(Text text, string stopWordsFilePath)
        {
            if (!File.Exists(stopWordsFilePath))
                throw new FileNotFoundException($"Stop words file not found: {stopWordsFilePath}");
            
            var stopWords = File.ReadAllLines(stopWordsFilePath)
                .Select(line => line.Trim().ToLower())
                .Where(line => !string.IsNullOrEmpty(line))
                .ToHashSet();
            
            var newText = new Text();
            
            foreach (var sentence in text.Sentences)
            {
                var newSentence = new Sentence();
                
                foreach (var token in sentence.Tokens)
                {
                    if (token is Word word && stopWords.Contains(word.Value.ToLower()))
                    {
                        continue;
                    }
                    newSentence.AddToken(token);
                }
                
                newText.AddSentence(newSentence);
            }
            
            return newText;
        }
        
        private static bool IsQuestionSentence(Sentence sentence)
        {
            return sentence.Tokens.LastOrDefault() is Punctuation punctuation && 
                   punctuation.Symbol == '?';
        }
        
        private static bool StartsWithConsonant(string word)
        {
            if (string.IsNullOrEmpty(word)) return false;
            
            char firstChar = char.ToLower(word[0]);
            string consonants = "bcdfghjklmnpqrstvwxyzбвгджзйклмнпрстфхцчшщ";
            
            return consonants.Contains(firstChar);
        }
    }
}