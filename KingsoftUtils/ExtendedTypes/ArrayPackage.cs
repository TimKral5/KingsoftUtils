using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.ExtendedTypes
{
	public class ArrayPackage<ArrayType>
	{
		public ArrayType[] array { get; set; }
		public ArrayPackage() { }
		public ArrayPackage(ArrayType[] _array)
		{
			array = _array;
		}
	}

	public static class ExtArrayArrayExtension
	{
		public static ArrayPackage<inT> ToExtArray<inT>(this inT[] array)
		{
			return new ArrayPackage<inT> { array = array };
		}
	}
}
