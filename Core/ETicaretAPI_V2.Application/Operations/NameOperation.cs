using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ETicaretAPI_V2.Application.Operations
{
    public class NameOperation
    {
        public static string CharacterRegulatory(string name)
        {
            
            name = ReduceConsecutiveSpaces(name);
            name = name.Replace(" ", "-");
            name = RemovePunctuation(name);
            name = ConvertTurkishToEnglish(name);
            return name;
        }

        private static string ConvertTurkishToEnglish(string input)
        {
            string[] turkishChars = { "ç", "ğ", "ı", "i", "ö", "ş", "ü", "Ğ", "İ", "Ö", "Ş", "Ü", "Ç" };
            string[] englishChars = { "c", "g", "i", "i", "o", "s", "u", "G", "I", "O", "S", "U", "C" };

            for (int i = 0; i < turkishChars.Length; i++)
            {
                input = input.Replace(turkishChars[i], englishChars[i]);
            }

            return input;
        }

        private static string RemovePunctuation(string input)
        {
            return Regex.Replace(input, @"[^-\w\d]", "");
        }

        private static string ReduceConsecutiveSpaces(string input)
        {
            return Regex.Replace(input, @"\s+", " ");
        }
    }
}
