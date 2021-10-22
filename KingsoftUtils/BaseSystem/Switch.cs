using System;
using System.Collections.Generic;

namespace Kingsoft.Utils.BaseSystem
{
	public class Switch
	{
		private List<(object, Func<object, object>)> Cases = new List<(object, Func<object, object>)>();
		private Func<object> DefaultFunk { get; set; }

		public Switch()
		{
			DefaultFunk = () => { return new object(); };
		}

		public Switch Case(object targObj, Func<object, object> func)
		{
			Cases.Add((targObj, func));
			return this;
		}

		public Switch Default(Func<object> func)
		{
			DefaultFunk = func;
			return this;
		}

		public T Run<T>(object caseInp, object funcInp)
		{
			foreach ((object, Func<object, object>) item in Cases)
			{
				if (caseInp == item.Item1)
				{
					return (T)item.Item2(funcInp);
				}
			}

			if (DefaultFunk != null) return (T)DefaultFunk();
			throw new ArgumentException();
		}

		public object Run(object caseInp, object funcInp) => Run<object>(caseInp, funcInp);
	}
}
