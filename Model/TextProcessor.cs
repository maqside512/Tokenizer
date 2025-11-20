using System;
using System.Collections.Generic;
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