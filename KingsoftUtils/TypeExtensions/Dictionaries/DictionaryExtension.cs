using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingsoft.Utils.Delegates.Templates;
using Kingsoft.Utils.TypeExtensions.Arrays;
using DT = Kingsoft.Utils.Delegates.Templates.DelegateTemplates;

namespace Kingsoft.Utils.TypeExtensions.Dictionaries
{
	public static class DictionaryExtension
	{
		public static Dictionary<T2, Dictionary<T1, T2>> ConvertToDict<T1,T2>(this Dictionary<T1, T2>[] self, T1 key)
        {
			Dictionary<T2,Dictionary<T1,T2>> res = new Dictionary<T2,Dictionary<T1, T2>>();

			foreach (Dictionary<T1, T2> item in self)
            {
				res.Add(item[key], item);
            }
			return res;
        }

		public static Dictionary<T1, T2> ConvertToDict<T1, T2>(this Dictionary<T1, T2>[] self, DT._1_O1<Dictionary<T1, T2>, (bool, T1, T2)> condition)
		{
			Dictionary<T1, T2> res = new Dictionary<T1, T2>();

			foreach (Dictionary<T1, T2> item in self)
			{
				(bool res1, T1 res2, T2 res3) = condition(item);
				if (res1) res.Add(res2, res3);
			}
			return res;
		}

		public static bool ContainsKeys<key, val>(this Dictionary<key, val> self, params key[] keys)
        {
			key[] _keys = self.Keys.ToArray();
			bool res = true;
			keys.Each((x) => { if (!res) return; if (!_keys.Contains(x)) res = false; });
			return res;
        }

		public static Dictionary<ouT, ouT> Add<ouT>(this Dictionary<ouT, ouT> self, ouT[] pair)
        {
			self.Add(pair[0], pair[1]);
			return self;
		}
		public static outT[] Each<inT, inT2, outT>(
			this Dictionary<inT, inT2> dict, 
			DelegateTemplates._2_O1<inT, inT2, outT> func)
		{
			int i = -1;
			outT[] res = new outT[dict.Count];

			foreach (KeyValuePair<inT, inT2> item in dict)
			{
				i++;
				inT key = item.Key;
				inT2 val = item.Value;
				res[i] = func(key, val);
			}

			return res;
		}

		public static void Each<inT, inT2>(
			this Dictionary<inT, inT2> dict, 
			DelegateTemplates._2_Void<inT, inT2> func)
		{ dict.Each((inT key, inT2 val ) => { func(key, val); return true; }); }
	}
}
