using System;
using System.Collections.Generic;

namespace Tokenizer.Model
{
    public static class TextExtensions
    {
        public static void PrintSentences(this List<Sentence> sentences)
        {
            foreach (var sentence in sentences)
            {
                Console.WriteLine($"({sentence.WordCount} words, {sentence.Length} chars): {sentence}");
            }
        }
    }
}