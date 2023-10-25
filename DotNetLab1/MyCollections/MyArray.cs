using DotNetLab1.MyCollections;
using DotNetLab1.MyCollections.CustomEventArgs;
using System.Collections;

namespace DotNetLab1;

public class DynamicArray<T> : IList<T>
{

	private LinkedListNode head;
	private int count;

	public int Count => count;
	public bool IsReadOnly => false;

	private class LinkedListNode
	{
		public T Data { get; set; }
		public LinkedListNode Next { get; set; }

		public LinkedListNode(T data)
		{
			Data = data;
			Next = null;
		}
	}
	public DynamicArray()
	{
		head = null;
		count = 0;
	}
	
	public DynamicArray(IEnumerable<T> collection)
	{
		if (collection == null)
		{
			throw new ArgumentNullException("collection");
		}

		head = null;
		count = 0;

		foreach (T item in collection)
		{
			Add(item);
		}
	}
	
	public DynamicArray(int size)
	{
		if (size < 0)
		{
			throw new ArgumentOutOfRangeException("size");
		}

		head = null;
		count = 0;

		for (int i = 0; i < size; i++)
		{
			Add(default(T)); 
		}
	}

	public T this[int index]
	{
		get
		{
			if (index < 0 || index >= count)
			{
				throw new IndexOutOfRangeException("Index is out of range.");
			}
			LinkedListNode current = head;
			for (int i = 0; i < index; i++)
			{
				current = current.Next;
			}
			return current.Data;
		}
		set
		{
			if (index < 0 || index >= count)
			{
				throw new IndexOutOfRangeException("Index is out of range.");
			}
			LinkedListNode current = head;
			for (int i = 0; i < index; i++)
			{
				current = current.Next;
			}
			current.Data = value;
		}
	}
	#region Events
	public EventHandler<ArrayItemEventArgs<T>> ItemAdded;

	public EventHandler<ArrayItemEventArgs<T>> ItemRemoved;

	public EventHandler<ArrayEventArgs> ArrayCleared;


	protected virtual void OnItemAdded(T item, int index)
	{
		if (ItemAdded != null)
		{
			ItemAdded(this, new ArrayItemEventArgs<T>(item, index, ArrayAction.Add));
		}
	}

	protected virtual void OnItemRemoved(T item, int index)
	{
		if (ItemRemoved != null)
		{
			ItemRemoved(this, new ArrayItemEventArgs<T>(item, index, ArrayAction.Remove));
		}
	}

	protected virtual void OnArrayCleared()
	{
		if (ArrayCleared != null)
		{
			ArrayCleared(this, new ArrayEventArgs(ArrayAction.Clear));
		}
	}

	#endregion 
	public void Add(T item)
	{
		if (item == null)
		{
			throw new ArgumentNullException(nameof(item), "Item cannot be null");
		}

		LinkedListNode newNode = new LinkedListNode(item);
		if (head == null)
		{
			head = newNode;
		}
		else
		{
			LinkedListNode current = head;
			while (current.Next != null)
			{
				current = current.Next;
			}
			current.Next = newNode;
		}
		count++;
		OnItemAdded(item, Count - 1);
	}

	public bool Contains(T item)
	{
		if (item is null)
			throw new ArgumentNullException(nameof(item), "Item to check for existance cannot be null.");
		
		return IndexOf(item) != -1;
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		if (array == null)
			throw new ArgumentNullException(nameof(array), "Array cannot be null.");

		if (arrayIndex < 0 || arrayIndex > array.Length)
			throw new ArgumentOutOfRangeException(nameof(arrayIndex), "Invalid array index.");

		if (count > array.Length - arrayIndex)
			throw new ArgumentException("The destination array does not have enough space.");

		LinkedListNode current = head;
		while (current != null)
		{
			array[arrayIndex++] = current.Data;
			current = current.Next;
		}
	}

	public int IndexOf(T item)
	{
		if (item == null)
			throw new ArgumentNullException(nameof(item), "Item to search cannot be null.");

		int index = 0;
		LinkedListNode current = head;

		while (current != null)
		{
			if (EqualityComparer<T>.Default.Equals(current.Data, item))
			{
				return index;
			}

			current = current.Next;
			index++;
		}

		return -1;
	}

	public void Insert(int index, T item)
	{
		
		if (item == null)
		{
			throw new ArgumentNullException(nameof(item), "Item cannot be null");
		}
		
		if (index < 0 || index > count)
			throw new ArgumentOutOfRangeException(nameof(index));

		LinkedListNode newNode = new LinkedListNode(item);

		if (index == 0)
		{
			newNode.Next = head;
			head = newNode;
		}
		else
		{
			LinkedListNode current = head;
			for (int i = 0; i < index - 1; i++)
			{
				current = current.Next;
			}

			newNode.Next = current.Next;
			current.Next = newNode;
		}

		count++;
		OnItemAdded(item, index);
	}

	public bool Remove(T item)
	{
		if (item == null)
			throw new ArgumentNullException(nameof(item), "Item to search cannot be null.");


		LinkedListNode current = head;
		LinkedListNode previous = null;

		while (current != null)
		{
			if (EqualityComparer<T>.Default.Equals(current.Data, item))
			{
				if (previous == null)
				{
					head = current.Next;
				}
				else
				{
					previous.Next = current.Next;
				}
				count--;
				OnItemRemoved(item, Count);
				return true;
			}

			previous = current;
			current = current.Next;
		}

		return false;
	}

	public void RemoveAt(int index)
	{
		if (index < 0 || index >= count)
			throw new ArgumentOutOfRangeException(nameof(index));

		if (index == 0)
			head = head.Next;
		else
		{
			LinkedListNode current = head;
			LinkedListNode previous = null;

			for (int i = 0; i < index; i++)
			{
				previous = current;
				current = current.Next;
			}

			previous.Next = current.Next;
			OnItemRemoved(current.Data, Count);
		}

		count--;
	}

	public void Clear()
	{
		head = null;
		count = 0;
		OnArrayCleared();
	}

	public IEnumerator<T> GetEnumerator()
	{
		return new MyEnumerator<T>(this);
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}