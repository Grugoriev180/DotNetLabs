using DotNetLab1.MyCollections.CustomEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetLab1.MyCollections;

namespace DotNetLab1.Lab1
{
	public static class Program
	{

		public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}
		static void Main(string[] args)
		{



			DynamicArray<int> myArray = new DynamicArray<int>();

			myArray.ItemAdded += PrintEventInvoke!;
			myArray.ItemRemoved += PrintEventInvoke!;
			myArray.ArrayCleared += (sender, e) => Console.WriteLine($"_____Event invoked: \"{e.Action}\"_____");

			myArray.Add(10);
			myArray.Add(2);
			myArray.Add(4);
			myArray.Add(6);
			myArray.Add(7);
			Console.WriteLine("myArray:");
			myArray.Foreach(item => Console.Write(item + " "));

			
			int[] targetArray = new int[9];

			Console.WriteLine("\n");
			myArray.CopyTo(targetArray, 2);
			Console.WriteLine("targetArray after CopyTo:");
			targetArray.Foreach(item => Console.Write(item + " "));

			Console.WriteLine("\n");
			myArray.Remove(3);
			myArray.RemoveAt(1);
			Console.WriteLine("myArray after removing some elements:");
			myArray.Foreach(item => Console.Write(item + " "));

			Console.WriteLine("\n");
			myArray.Insert(0, 8);
			Console.WriteLine("myArray after inserting element:");
			myArray.Foreach(item => Console.Write(item + " "));

			if(myArray.IndexOf(4) == -1)
				Console.WriteLine("\n\nIndexOf 4: No such element in array");
			else
				Console.WriteLine($"\n\nIndexOf 4: {myArray.IndexOf(4)}");

			Console.WriteLine($"\nContains 5: {myArray.Contains(5)}");

			Console.Write("\n");
			myArray.Clear();
			Console.WriteLine("Clear myArray:");
			myArray.Foreach(item => Console.Write(item + " "));

			

			Console.ReadLine();
		}
		public static void PrintEventInvoke(object sender, ArrayItemEventArgs<int> e)
		{
			Console.WriteLine($"_____Event invoked: \"{e.Action}\" Item: {e.Item} Index: {e.Index}_____");
		}
	}
}
