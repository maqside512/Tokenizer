using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tokenizer.Model
{
    public class TextParser
{
    public static Text Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new Text();

        var text = new Text();
        var currentSentence = new Sentence();
        var currentWord = new StringBuilder();
        
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            
            if (Punctuation.IsPunctuation(c))
            {
                if (currentWord.Length > 0)
                {
                    currentSentence.AddToken(new Word(currentWord.ToString()));
                    currentWord.Clear();
                }
                
                currentSentence.AddToken(new Punctuation(c));
                
                if (c == '.' || c == '!' || c == '?' || c == '…')
                {
                    if (currentSentence.Tokens.Count > 0)
                    {
                        text.AddSentence(currentSentence);
                    }
                    currentSentence = new Sentence();
                }
            }
            else if (char.IsWhiteSpace(c))
            {
                if (currentWord.Length > 0)
                {
                    currentSentence.AddToken(new Word(currentWord.ToString()));
                    currentWord.Clear();
                }
                
                if (currentSentence.Tokens.Count == 0) 
                    continue;
            }
            else
            {
                currentWord.Append(c);
            }
        }
        
        if (currentWord.Length > 0)
        {
            currentSentence.AddToken(new Word(currentWord.ToString()));
        }
        
        if (currentSentence.Tokens.Count > 0)
        {
            text.AddSentence(currentSentence);
        }
        
        return text;
    }
    
    public static Text ParseFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");
            
        string content = File.ReadAllText(filePath, Encoding.UTF8);
        return Parse(content);
    }
}
}