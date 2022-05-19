using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kingsoft.Utils.Delegates.Templates
{
	public static class DelegateTemplates
	{
		#region In 1 - 4 (+ Array), Out 0
		public delegate void _1_Void<inT>
			(inT arg1);

		public delegate void _2_Void<inT, inT2>
			(inT arg1, inT2 arg2);

		public delegate void _3_Void<inT, inT2, inT3>
			(inT arg1, inT2 arg2, inT3 arg3);

		public delegate void _4_Void<inT, inT2, inT3, inT4>
			(inT arg1, inT2 arg2, inT3 arg3, inT4 arg4);

		public delegate void _A_Void<inT>
			(inT[] arg1);
		#endregion

		#region In 1 - 4 (+ Array), Out 1 (+ Array)
		public delegate outT _1_O1<inT, outT>
			(inT arg1);

		public delegate outT _2_O1<inT, inT2, outT>
			(inT arg1, inT2 arg2);

		public delegate outT _3_O1<inT, inT2, inT3, outT>
			(inT arg1, inT2 arg2, inT3 arg3);

		public delegate outT _4_O1<inT, inT2, inT3, inT4, outT>
			(inT arg1, inT2 arg2, inT3 arg3, inT4 arg4);

		public delegate outT[] _A_OA<inT, outT>
			(inT[] arg1);
		#endregion

		#region In 1 - 4 (+ Array), Out 2 (Tuple) (+ Array)
		public delegate (outT, outT2) _1_OT2<inT, outT, outT2>
			(inT arg1);

		public delegate (outT, outT2) _2_OT2<inT, inT2, outT, outT2>
			(inT arg1, inT2 arg2);

		public delegate (outT, outT2) _3_OT2<inT, inT2, inT3, outT, outT2>
			(inT arg1, inT2 arg2, inT3 arg3);

		public delegate (outT, outT2) _4_OT2<inT, inT2, inT3, inT4, outT, outT2>
			(inT arg1, inT2 arg2, inT3 arg3, inT4 arg4);

		public delegate (outT[], outT2[]) _A_OT2A<inT, outT, outT2>
			(inT[] arg1);
		#endregion

		#region In 1 - 4 (+ Array), Out 3 (Tuple) (+ Array)
		public delegate (outT, outT2, outT3) _1_OT3<inT, outT, outT2, outT3>
			(inT arg1);

		public delegate (outT, outT2, outT3) _2_OT3<inT, inT2, outT, outT2, outT3>
			(inT arg1, inT2 arg2);

		public delegate (outT, outT2, outT3) _3_OT3<inT, inT2, inT3, outT, outT2, outT3>
			(inT arg1, inT2 arg2, inT3 arg3);

		public delegate (outT, outT2, outT3) _4_OT3<inT, inT2, inT3, inT4, outT, outT2, outT3>
			(inT arg1, inT2 arg2, inT3 arg3, inT4 arg4);

		public delegate (outT[], outT2[], outT3[]) _A_OT3A<inT, outT, outT2, outT3>
			(inT[] arg1);
		#endregion

		#region In 1 - 4 (+ Array), Out 3 (Tuple) (+ Array)
		public delegate (outT, outT2, outT3, outT4) _1_OT4<inT, outT, outT2, outT3, outT4>
			(inT arg1);

		public delegate (outT, outT2, outT3, outT4) _2_OT4<inT, inT2, outT, outT2, outT3, outT4>
			(inT arg1, inT2 arg2);

		public delegate (outT, outT2, outT3, outT4) _3_OT4<inT, inT2, inT3, outT, outT2, outT3, outT4>
			(inT arg1, inT2 arg2, inT3 arg3);

		public delegate (outT, outT2, outT3, outT4) _4_OT4<inT, inT2, inT3, inT4, outT, outT2, outT3, outT4>
			(inT arg1, inT2 arg2, inT3 arg3, inT4 arg4);

		public delegate (outT[], outT2[], outT3[], outT4[]) _A_OT4A<inT, outT, outT2, outT3, outT4>
			(inT[] arg1);
		#endregion
	}
}
