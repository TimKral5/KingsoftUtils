﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Langs.Lexer.Basic.Schemes
{
    public class BasicKeywordSchema : ILxSchema
    {
        private int counter = 0;
        private string keyword;
        private string value;

        public BasicKeywordSchema(string _keyword, string _value)
        {
            keyword = _keyword;
            value = _value;
        }

        (bool, LxToken) ILxSchema.CheckSchema(LxSchemaArgs args)
        {

            (bool, LxToken) result = (false, new LxToken());

            char[] word = keyword.ToCharArray();

            Console.WriteLine(args["lexer:vars:args"]);

            if ((counter < word.Length) && ((char)args["lexer:vars:char"] == word[counter]))
            {
                counter++;
            }

            else if (args.CheckForEOT((char)args["lexer:vars:char"]))
            {
                if (counter == word.Length)
                {
                    result = (true, new LxToken(value, ""));
                }
                counter = 0;
            }

            return result;
        }
    }
}
