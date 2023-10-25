using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetLab1.MyCollections
{
	internal class MyEnumerator<T> : IEnumerator<T>
	{
		private readonly IList<T> _list;
		private T _current;
		private int _index;

		public T Current => _current;

		object IEnumerator.Current => _current!;

		public MyEnumerator(IList<T> collection)
		{

			_list = collection;
			_index = -1;

			_current = _list.Any() ? _list[0] : default!;
		}

		public bool MoveNext()
		{
			_index++;

			if (_index < _list.Count)
			{
				_current = _list[_index];

				return true;
			}

			return false;
		}

		public void Reset()
		{
			_index = -1;

			if (_list.Any())
			{
				_current = _list[0];
			}
		}

		public void Dispose()
		{
			return;
		}
	}
}
