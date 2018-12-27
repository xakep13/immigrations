using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using System.Linq;
using xTab.Tools.Extensions;

namespace LemmaSharp
{
    [Serializable()]
    public class LemmaProcessor : BaseLemmaProcessor
    {
        public const string FILEMASK = "compact7z-{0}.lem";

        public LemmaProcessor(String lang)
            : base(lang)
        {
            Stream stream = GetResourceStream(GetResourceFileName(FILEMASK));
            this.Deserialize(stream);
            stream.Close();
        }

        public LemmaProcessor(LanguagePrebuilt lang)
            : base(lang)
        {
            Stream stream = GetResourceStream(GetResourceFileName(FILEMASK));
            this.Deserialize(stream);
            stream.Close();
        }

        protected override Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        public String LemmatizeWord(String word)
        {
            return Lemmatize(word);
        }

        public String PrepareText(String text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;

            text = text
                .ToLower()
                .RegexReplace(@"['""():;?!#№=+*#]", " ")
                .RegexReplace(ObjectExtensions.RegExprs.DoubleSpaces, " ")
                .Trim();

            var words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var result = String.Empty;

            foreach (var word in words)
            {
                if (!word.Any(x => char.IsDigit(x)))
                {
                    if (word.Length >= 3)
                    {
                        var cleanWord = word.RegexReplace(@"[.,/\\]", " ").RegexReplace(ObjectExtensions.RegExprs.DoubleSpaces, " ").Trim();

                        if (cleanWord.Contains(" "))
                        {
                            foreach (var newWord in cleanWord.Split(' '))
                            {
                                if (newWord.Length >= 3)
                                {
                                    result += LemmatizeWord(cleanWord) + " ";
                                }
                            }
                        }
                        else
                        {
                            result += LemmatizeWord(cleanWord) + " ";
                        }
                    }
                }
                else
                {
                    result += word + " ";
                }
            }

            if (!String.IsNullOrWhiteSpace(result))
                return result.Trim();
            else
                return null;
        }

    }
}
