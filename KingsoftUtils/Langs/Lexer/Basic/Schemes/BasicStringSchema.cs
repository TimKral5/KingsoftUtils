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

            if ((char)args["lexer:vars:char"] == '"' || (char)args["lexer:vars:char"] == '\'') openString = !openString;
            else if (openString)
            {
                currentString += (char)args["lexer:vars:char"];
            }
            else
            {
                if (currentString != "")
                {
                    result = (true, new LxToken("string", currentString));
                    currentString = "";
                }
            }

            args["lexer:vars:open_string"] = openString;

            return result;
        }
    }
}
