using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Langs.Lexer.Basic.Schemes
{
    public class BasicNumberSchema : ILxSchema
    {
        private bool openNumber = false;
        private string currentNumber = "";

        (bool, LxToken) ILxSchema.CheckSchema(LxSchemaArgs args)
        {
            (bool, LxToken) result = (false, new LxToken());

            char[] chars = new char[]
            {
                '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
            };

            bool match = false;

            for (int i = 0; i < chars.Length; i++)
            {
                if (args.Char == chars[i]) match = true;
            }

            if (match) openNumber = true;

            if (openNumber)
            {
                if (match || args.Char == '.') currentNumber += args.Char;
                else openNumber = false;
            }

            if (!openNumber && currentNumber != "")
            {
                result = (true, new LxToken("number", currentNumber));
                currentNumber = "";
            }

            return result;
        }
    }
}
