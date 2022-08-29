using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Langs.Lexer.Basic.Schemes
{
    public class BasicSemicolonSchema : ILxSchema
    {
        private bool openNumber = false;
        private string currentNumber = "";

        (bool, LxToken) ILxSchema.CheckSchema(LxSchemaArgs args)
        {
            (bool, LxToken) result = (false, new LxToken());

            

            result = (true, new LxToken("number", currentNumber));

            return result;
        }
    }
}
