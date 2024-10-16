using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
	/// <summary>
	/// class to generate random strings
	/// </summary>
	public class CodeGenerationHelper
	{
        public string GenerateRandomCode(int codeLength)
        {
            List<char> chars = new List<char>();

            int oddtNumeric = 0;
            int oddLowerCaseChars = 0;

            if(codeLength % 2 == 1)
			{
                Random random = new Random();
                bool isNumeric = random.Next(2) == 0;
                if (isNumeric)
                    oddtNumeric = 1;
                else
                    oddLowerCaseChars = 1;
			}

            chars.AddRange(GetLowerCaseChars(codeLength/2 + oddLowerCaseChars));
            chars.AddRange(GetNumericChars(codeLength/2 + oddtNumeric));

            return GenerateCodeFromList(chars);
        }

        private List<char> GetLowerCaseChars(int count)
        {
            List<char> result = new List<char>();

            Random random = new Random();

            for (int index = 0; index < count; index++)
            {
                result.Add(Char.ToLower(Convert.ToChar(random.Next(97, 122))));
            }

            return result;
        }

        private List<char> GetNumericChars(int count)
        {
            List<char> result = new List<char>();

            Random random = new Random();

            for (int index = 0; index < count; index++)
            {
                result.Add(Convert.ToChar(random.Next(0, 9).ToString()));
            }

            return result;
        }

        private string GenerateCodeFromList(List<char> chars)
        {
            string result = string.Empty;

            Random random = new Random();

            while (chars.Count > 0)
            {
                int randomIndex = random.Next(0, chars.Count);
                result += chars[randomIndex];
                chars.RemoveAt(randomIndex);
            }

            return result;
        }
    }
}