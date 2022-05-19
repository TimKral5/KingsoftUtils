using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingsoft.Utils.Delegates.Templates;

namespace Kingsoft.Utils.TypeExtensions.Arrays
{
	public static class ArrayExtension
	{
		public static outT[] Each<inT, outT>(this inT[] array, 
			DelegateTemplates._1_O1<inT, outT> func, bool rev = false)
		{
			outT[] res = new outT[array.Length];
			if (!rev) for (int i = 0; i < array.Length; i++)
			{
				inT item = array[i];
				res[i] = func(item);
			}
			else for (int i = array.Length - 1; i >= 0; i--)
			{
				inT item = array[i];
				res[i] = func(item);
			}
			return res;
		}

		public static void Each<inT>(this inT[] array, 
			DelegateTemplates._1_Void<inT> func, bool rev = false)
		{
			array.Each((inT arg) => { func(arg); return true; }, rev);
			return;
		}

		public static outT[] Each<inT, outT>(this inT[] array, 
			DelegateTemplates._2_O1<int, inT, outT> func, bool rev = false) 
		{
			int i = 0;
			outT[] res = array.Each((inT arg) 
				=> { outT _res = func(i, arg); i++; return _res; }, rev);

			return res;
		}

		public static void Each<inT>(this inT[] array, 
			DelegateTemplates._2_Void<int, inT> func, bool rev = false) 
			=> array.Each((int i, inT x) => { func(i, x); return true; }, rev);

		public static string ToStr<inT>(this inT[] array)
		{
			string res = "[";
			array.Each((inT item) => { res += $"{item},"; });
			res = res.Length > 1 ? res.Remove(res.Length - 1) + "]" : res + "]";
			return res;
		}

		public static inT[] Append<inT>(this inT[] array, inT obj)
		{
			inT[] newArray = new inT[array.Length + 1];
			array.Each((int i, inT o) => { newArray[i] = o; });
			newArray[newArray.Length - 1] = obj;
			array = newArray;
			return array;
		}

		public static inT[] Remove<inT>(this inT[] array, int index)
		{
			inT[] res = new inT[array.Length - 1];
			int off = 0;
			for (int i = 0; i < res.Length; i++)
			{
				if (i == index) off++;
				res[i] = array[i + off];
			}
			return res;
		}

		public static inT[] Remove<inT>(this inT[] array, int[] indeces)
		{
			indeces.Each((int x) => { array = array.Remove(x); });
			return array;
		}

		public static inT[] Replace<inT>(this inT[] array, int index, inT val)
		{
			array[index] = val;
			return array;
		}

		public static inT[] Replace<inT>(this inT[] array, int[] indeces, inT[] values)
		{
			indeces.Each((int i) => { array = array.Replace(i, values[i]); });
			return array;
		}

		public static inT[] Inject<inT>(this inT[] array, int startIndex, inT[] values, bool injectBeforeIndex = false)
		{
			inT[] res = new inT[array.Length + values.Length];
			int x = 0;
			res.Each((inT i) => 
			{
				if (injectBeforeIndex && x != res.Length - 1) res[x] = i;
				if (x == startIndex + 1) values.Each((inT y) => { res[x] = y; x++; });
				if ((!injectBeforeIndex && x < res.Length - 1) || x == res.Length - 1) 
					res[x] = i;
				x++;
			} );

			return res;
		}
	}
}
