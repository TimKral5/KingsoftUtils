using Kingsoft.Utils.TypeExtensions.Arrays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.TypeExtensions.String
{
	public static class StringExtension
	{
		public static int[] KsGetPosOAIs(this string str, char arg)
		{
			string[] aStr = str.Split(arg);
			int j = 0;
			int[] res = new int[aStr.Length - 1];
			for (int i = 0; i < (aStr.Length - 1); i++)
			{
				string current = aStr[i];
				j += current.Length;
				res[i] = j;
				j += 1;
			}

			return res;
		}

		public static string ReplaceI (this string str, string arg, string arg2)
        {
			string[] aStr = str.Split(new string[] { arg }, StringSplitOptions.None);
			string res = aStr[0];
			aStr.Each((x, y) => {  res += x == 0 ? "" : $"{arg2}{y}"; });
			return res;
        }
	}
}
