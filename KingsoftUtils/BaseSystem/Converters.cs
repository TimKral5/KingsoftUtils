using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.BaseSystem
{
	public static class Converters
	{
		public static Dictionary<string, string> HttpQueryToDictionary(this string[] aStr)
		{
			Dictionary<string, string> res = new Dictionary<string, string>();
			if (aStr == null) return null;
			for (int i = 0; i < aStr.Length; i++)
			{
				string key = aStr[i].Split('=')[0];
				string value = aStr[i].Remove(0, key.Length + 1);
				res.Add(key, value);
			}
			return res;
		}
	}
}
