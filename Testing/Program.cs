﻿using System;

using Seven;
using Seven.Structures;

namespace Testing
{
  class Program
  {
    public static Comparison Compare(int left, int right)
    {
      int comparison = left.CompareTo(right);
      if (comparison < 0)
        return Comparison.Less;
      else if (comparison > 0)
        return Comparison.Greater;
      else
        return Comparison.Equal;
    }

    static void Main(string[] args)
    {
      int test = 10;

      Console.WriteLine("Testing Link");
      Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
      Console.Write("Delegate: ");
      link.Foreach((dynamic current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (dynamic value in link)
        Console.Write(value);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing Array_Array<Type>");
      Array<int> array = new Array_Array<int>(10);
      for (int i = 0; i < test; i++)
        array[i] = i;
      Console.Write("Delegate: ");
      array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing List_Array<Type>");
      List<int> list_array = new List_Array<int>(10);
      for (int i = 0; i < test; i++)
        list_array.Add(i);
      Console.Write("Delegate: ");
      list_array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in list_array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing List_Linked<Type>");
      List<int> list_linked = new List_Linked<int>();
      for (int i = 0; i < test; i++)
        list_linked.Add(i);
      Console.Write("Delegate: ");
      list_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in list_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing Stack_Linked<Type>");
      Stack<int> stack_linked = new Stack_Linked<int>();
      for (int i = 0; i < test; i++)
        stack_linked.Push(i);
      Console.Write("Delegate: ");
      stack_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in stack_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing Stack_Array<Type>");
      Stack<int> stack_array = new Stack_Array<int>(10);
      for (int i = 0; i < test; i++)
        stack_array.Push(i);
      Console.Write("Delegate: ");
      stack_array.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in stack_array)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing Queue_Linked<Type>");
      Queue<int> queue_linked = new Queue_Linked<int>();
      for (int i = 0; i < test; i++)
        queue_linked.Enqueue(i);
      Console.Write("Delegate: ");
      queue_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in queue_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing AvlTree_Linked<Type>");
      AvlTree<int> avlTree_linked = new AvlTree_Linked<int>(Compare);
      for (int i = 0; i < test; i++)
        avlTree_linked.Add(i);
      Console.Write("Delegate: ");
      avlTree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in avlTree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing RedBlack_Linked<Type>");
      RedBlackTree<int> redBlackTree_linked = new RedBlackTree_Linked<int>(Compare);
      for (int i = 0; i < test; i++)
        redBlackTree_linked.Add(i);
      Console.Write("Delegate: ");
      redBlackTree_linked.Foreach((int current) => { Console.Write(current); });
      Console.WriteLine();
      Console.Write("IEnumerator: ");
      foreach (int current in redBlackTree_linked)
        Console.Write(current);
      Console.WriteLine();
      Console.WriteLine();

      Console.WriteLine("Testing Complete...");
      Console.ReadLine();
    }
  }
}
