using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLab1.MyCollections.CustomEventArgs
{
	public class ArrayItemEventArgs<T> : ArrayEventArgs
	{
		public T Item { get; private set; }
		public int Index { get; private set; }

		public ArrayItemEventArgs(T item, int index, ArrayAction action) : base(action)
		{
			Item = item;
			Index = index;
		}
	}
}
