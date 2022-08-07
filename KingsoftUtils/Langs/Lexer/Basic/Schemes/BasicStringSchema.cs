using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Langs.Lexer.Basic.Schemes
{
    public class BasicStringSchema : ILxSchema
    {
        private bool openString = false;
        private string currentString = "";

        (bool, LxToken) ILxSchema.CheckSchema(LxSchemaArgs args)
        {
            (bool, LxToken) result = (false, new LxToken());

            if (args.Char == '"' || args.Char == '\'') openString = !openString;
            else if (openString)
            {
                currentString += args.Char;
            }
            else
            {
                if (currentString != "")
                {
                    result = (true, new LxToken("string", currentString));
                    currentString = "";
                }
            }

            return result;
        }
    }
}
