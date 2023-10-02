using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLab1.MyCollections.CustomEventArgs
{
	public class ArrayEventArgs : EventArgs
	{
		public ArrayAction Action { get; private set; }

		public ArrayEventArgs(ArrayAction action)
		{
			Action = action;
		}
	}
}