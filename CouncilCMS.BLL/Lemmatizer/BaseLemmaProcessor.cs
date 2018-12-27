using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Reflection;
using LemmaSharp.Classes;
using System.Linq;

namespace LemmaSharp
{
    [Serializable()]
    public abstract class BaseLemmaProcessor : Lemmatizer
    {

        #region Private Variables

        private static string[] asLangMapping = new string[] {
            "bg", "mlteast",
            "cs", "mlteast",
            "en", "mlteast",
            "et", "mlteast",
            "fa", "mlteast",
            "fr", "mlteast",
            "hu", "mlteast",
            "mk", "mlteast",
            "pl", "mlteast",
            "ro", "mlteast",
            "ru", "mlteast",
            "sk", "mlteast",
            "sl", "mlteast",
            "sr", "mlteast",
            "uk", "mlteast",
            "en", "multext",
            "fr", "multext",
            "ge", "multext",
            "it", "multext",
            "sp", "multext",
        };

        private LanguagePrebuilt lang;

        #endregion

        #region Constructor(s) & Destructor(s)

        public BaseLemmaProcessor(String lang)
           : base()
        {
            switch (lang)
            {
                case "ru":
                    this.lang = LanguagePrebuilt.Russian;
                    break;
                case "uk":
                    this.lang = LanguagePrebuilt.Ukrainian;
                    break;
                case "en":
                    this.lang = LanguagePrebuilt.English;
                    break;
                default:
                    goto case "en";
            }
        }

        public BaseLemmaProcessor(LanguagePrebuilt lang)
            : base()
        {
            this.lang = lang;
        }

        public BaseLemmaProcessor(LanguagePrebuilt lang, LemmatizerSettings lsett)
            : base(lsett)
        {
            this.lang = lang;
        }

        #endregion

        #region Private Properties Helping Functions

        protected string GetResourceFileName(string sFileMask)
        {
            return GetResourceFileName(sFileMask, lang);
        }

        public static string GetResourceFileName(string sFileMask, LanguagePrebuilt lang)
        {
            string langFileName = asLangMapping[(int)lang * 2 + 1] + '-' + asLangMapping[(int)lang * 2];
            return String.Format(sFileMask, langFileName);
        }

        #endregion

        #region Public Properties

        public LanguagePrebuilt Language
        {
            get
            {
                return lang;
            }
        }
        public LexiconPrebuilt Lexicon
        {
            get
            {
                return GetLexicon(lang);
            }
        }

        #endregion

        #region Public Properties

        public static LexiconPrebuilt GetLexicon(LanguagePrebuilt lang)
        {
            return (LexiconPrebuilt)Enum.Parse(typeof(LexiconPrebuilt), asLangMapping[((int)lang) * 2 + 1], true);
        }

        #endregion

        #region Resource Management Functions

        protected abstract Assembly GetExecutingAssembly();

        protected Stream GetResourceStream(string sResourceShortName)
        {
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var assembly = assemblys.First(x => x.FullName.StartsWith("CouncilCMS.BLL"));

            string sResourceName = null;
            foreach (string sResource in assembly.GetManifestResourceNames())
                if (sResource.EndsWith(sResourceShortName))
                {
                    sResourceName = sResource;
                    break;
                }

            if (String.IsNullOrEmpty(sResourceName)) return null;

            return assembly.GetManifestResourceStream(sResourceName);
        }

        #endregion

        #region Serialization Functions

        public BaseLemmaProcessor(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

    }

    public enum LanguagePrebuilt
    {
        Bulgarian,
        Czech,
        English,
        Estonian,
        Persian,
        French,
        Hungarian,
        Macedonian,
        Polish,
        Romanian,
        Russian,
        Slovak,
        Slovene,
        Serbian,
        Ukrainian,
        EnglishMT,
        FrenchMT,
        German,
        Italian,
        Spanish,
    }

    public enum LexiconPrebuilt
    {
        MltEast,
        Multext
    }

}
