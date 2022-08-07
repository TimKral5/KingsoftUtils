using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Langs.Lexer
{
    public interface ILxSchema
    {
        (bool, LxToken) CheckSchema(LxSchemaArgs args);
    }
}
