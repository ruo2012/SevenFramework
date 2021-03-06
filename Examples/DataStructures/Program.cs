﻿using System;

using Seven;
using Seven.Mathematics;
using Seven.Structures;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Testing
{
	class Program
	{
		static void Main(string[] args)
		{
			Random random = new Random();
			int test = 10;

			Console.WriteLine("Welcome To SevenFramework! :)");
			Console.WriteLine();
			Console.WriteLine("You are runnning the Data Structures example.");
			Console.WriteLine("======================================================");
			Console.WriteLine();

			#region Link

			Console.WriteLine("  Testing Link-------------------------------");
			Console.WriteLine("   Size: 6");
			Link link = new Link<int, int, int, int, int, int>(0, 1, 2, 3, 4, 5);
			Console.Write("    Traversal: ");
			link.Stepper((dynamic current) => { Console.Write(current); });
			Console.WriteLine();
			// Saving to a file
			//string linklink_file = "link." + ToExtension(link.GetType());
			//Console.WriteLine("    File: \"" + linklink_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(linklink_file, link));
			//Link<int, int, int, int, int, int> deserialized_linklink;
			//Console.WriteLine("    Deserialized: " + Deserialize(linklink_file, out deserialized_linklink));
			Console.WriteLine();

			#endregion
			
			#region Array

			Console.WriteLine("  Testing Array_Array<int>-------------------");
			Array<int> array = new ArrayArray<int>(test);
			for (int i = 0; i < test; i++)
				array[i] = i;
			Console.Write("    Traversal: ");
			array.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Saving to a file
			//string arrayarray_file = "array." + ToExtension(array.GetType());
			//Console.WriteLine("    File: \"" + arrayarray_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(arrayarray_file, array));
			//ArrayArray<int> deserialized_arrayarray;
			//Console.WriteLine("    Deserialized: " + Deserialize(arrayarray_file, out deserialized_arrayarray));
			Console.WriteLine();

			#endregion
			
			#region List

			Console.WriteLine("  Testing List_Array<int>--------------------");
			List<int> list_array = new ListArray<int>(test);
			for (int i = 0; i < test; i++)
				list_array.Add(i);
			Console.Write("    Traversal: ");
			list_array.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Saving to a file
			//string listarray_file = "list_array." + ToExtension(list_array.GetType());
			//Console.WriteLine("    File: \"" + listarray_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(listarray_file, list_array));
			//ListArray<int> deserialized_listarray;
			//Console.WriteLine("    Deserialized: " + Deserialize(listarray_file, out deserialized_listarray));
			Console.WriteLine();

			Console.WriteLine("  Testing List_Linked<int>-------------------");
			List<int> list_linked = new ListLinked<int>();
			for (int i = 0; i < test; i++)
				list_linked.Add(i);
			Console.Write("    Traversal: ");
			list_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Saving to a file
			//string listlinked_file = "list_linked." + ToExtension(list_linked.GetType());
			//Console.WriteLine("    File: \"" + listlinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(listlinked_file, list_linked));
			//ListLinked<int> deserialized_listlinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(listlinked_file, out deserialized_listlinked));
			Console.WriteLine();

			#endregion

			#region Stack

			Console.WriteLine("  Testing Stack_Linked<int>------------------");
			Stack<int> stack_linked = new StackLinked<int>();
			for (int i = 0; i < test; i++)
				stack_linked.Push(i);
			Console.Write("    Traversal: ");
			stack_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Saving to a file
			//string stacklinked_file = "stack_linked." + ToExtension(stack_linked.GetType());
			//Console.WriteLine("    File: \"" + stacklinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(stacklinked_file, stack_linked));
			//StackLinked<int> deserialized_stacklinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(stacklinked_file, out deserialized_stacklinked));
			Console.WriteLine();

			#endregion

			#region Queue

			Console.WriteLine("  Testing Queue_Linked<int>------------------");
			Queue<int> queue_linked = new QueueLinked<int>();
			for (int i = 0; i < test; i++)
				queue_linked.Enqueue(i);
			Console.Write("    Traversal: ");
			queue_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Saving to a file
			//string queuelinked_file = "queue_linked." + ToExtension(queue_linked.GetType());
			//Console.WriteLine("    File: \"" + queuelinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(queuelinked_file, queue_linked));
			//QueueLinked<int> deserialized_queuelinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(queuelinked_file, out deserialized_queuelinked));
			Console.WriteLine();

			#endregion

			#region Heap

			Console.WriteLine("  Testing Heap_Array<int>--------------------");
			Heap<int> heap_array = new HeapArray<int>(Compute<int>.Compare);
			for (int i = 0; i < test; i++)
				heap_array.Enqueue(i);
			Console.Write("    Delegate: ");
			heap_array.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Saving to a file
			//string heaplinked_file = "heap_array." + ToExtension(heap_array.GetType());
			//Console.WriteLine("    File: \"" + heaplinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(heaplinked_file, heap_array));
			//HeapArray<int> deserialized_heaplinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(heaplinked_file, out deserialized_heaplinked));
			Console.WriteLine();

			#endregion

			#region Tree

			Console.WriteLine("  Testing Tree_Map<int>----------------------");
			Tree<int> tree_Map = new TreeMap<int>(0, Compute<int>.Equate, Hash.Default);
			for (int i = 1; i < test; i++)
				tree_Map.Add(i, i / (int)System.Math.Sqrt(test));
			Console.Write("    Children of 0 (root): ");
			tree_Map.Children(0, (int i) => { Console.Write(i + " "); });
			Console.WriteLine();
			Console.Write("    Children of " + ((int)System.Math.Sqrt(test) - 1) + " (root): ");
			tree_Map.Children(((int)System.Math.Sqrt(test) - 1), (int i) => { Console.Write(i + " "); });
			Console.WriteLine();
			Console.Write("    Traversal: ");
			tree_Map.Stepper((int i) => { Console.Write(i + " "); });
			Console.WriteLine();
			// Saving to a file
			//string treelinked_file = "tree_Map." + ToExtension(tree_Map.GetType());
			//Console.WriteLine("    File: \"" + treelinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(treelinked_file, tree_Map));
			//TreeMap<int> deserialized_treelinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(treelinked_file, out deserialized_treelinked));
			Console.WriteLine();

			#endregion

			#region AVL Tree

			//Console.WriteLine("  Testing AvlTree_Linked<int>----------------");
			//// Construction
			//AvlTree<int> avlTree_linked = new AvlTree_Linked<int>(Logic.compare);
			//// Adding Items
			//Console.Write("    Adding (0-" + test + ")...");
			//for (int i = 0; i < test; i++)
			//	avlTree_linked.Add(i);
			//Console.WriteLine();
			//// Iteration
			//Console.Write("    Traversal: ");
			//avlTree_linked.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//// Removal
			//int avl_tree_linked_removal = random.Next(0, test);
			//avlTree_linked.Remove(avl_tree_linked_removal);
			//Console.Write("    Remove(" + avl_tree_linked_removal + "): ");
			//avlTree_linked.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//// Look Up Items
			//int avl_tree_linked_lookup = random.Next(0, test);
			//while (avl_tree_linked_lookup == avl_tree_linked_removal)
			//	avl_tree_linked_lookup = random.Next(0, test);
			//Console.WriteLine("    Look Up (" + avl_tree_linked_lookup + "): " + avlTree_linked.TryGet(avl_tree_linked_lookup, Logic.compare, out temp));
			//Console.WriteLine("    Look Up (" + avl_tree_linked_removal + "): " + avlTree_linked.TryGet(avl_tree_linked_removal, Logic.compare, out temp));
			//avlTree_linked.Get(avl_tree_linked_lookup, Logic.compare);
			//// Current Min-Max Values
			//Console.WriteLine("    Least: " + avlTree_linked.CurrentLeast + " Greatest: " + avlTree_linked.CurrentGreatest);
			//// Saving to a file
			//string avltreelinked_file = "avlTree_linked." + ToExtension(avlTree_linked.GetType());
			//Console.WriteLine("    File: \"" + avltreelinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(avltreelinked_file, avlTree_linked));
			//AvlTree_Linked<int> deserialized_avltreelinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(avltreelinked_file, out deserialized_avltreelinked));
			//Console.WriteLine();

			#endregion

			#region Red-Black Tree

			Console.WriteLine("  Testing RedBlack_Linked<int>---------------");
			RedBlackTree<int> redBlackTree_linked = new RedBlackTreeLinked<int>(Compute<int>.Compare);
			for (int i = 0; i < test; i++)
				redBlackTree_linked.Add(i);
			Console.Write("    Traversal: ");
			redBlackTree_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Saving to a file
			//string redblacktreelinked_file = "redBlackTree_linked." + ToExtension(redBlackTree_linked.GetType());
			//Console.WriteLine("    File: \"" + redblacktreelinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(redblacktreelinked_file, redBlackTree_linked));
			//RedBlackTreeLinked<int> deserialized_redblacktreelinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(redblacktreelinked_file, out deserialized_redblacktreelinked));
			Console.WriteLine();

			#endregion

			#region BTree

			//Console.WriteLine("  Testing BTree_LinkedArray<int>-------------");
			//BTree<int> btree_linked = new BTree_LinkedArray<int>(Logic.compare, 3);
			//for (int i = 0; i < test; i++)
			//	btree_linked.Add(i);
			//Console.Write("    Delegate: ");
			//btree_linked.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//Console.Write("    IEnumerator: ");
			//foreach (int current in btree_linked)
			//	Console.Write(current);
			//Console.WriteLine();
			//Console.WriteLine("  Press Enter to continue...");
			//string maplinked_file = "maplinked.quad";
			//Console.WriteLine("    File: \"" + maplinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(maplinked_file, hashTable_linked));
			//Omnitree_LinkedLinkedLists<int, double> deserialized_maplinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(maplinked_file, out deserialized_maplinked));
			//Console.ReadLine();
			//Console.WriteLine();

			#endregion

			#region Set

			Console.WriteLine("  Testing Set_Hash<int>----------------------");
			Set<int> set_linked = new SetHashList<int>(Compute<int>.Equate, Hash.Default);
			for (int i = 0; i < test; i++)
				set_linked.Add(i);
			// Traversal
			Console.Write("    Traversal: ");
			set_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.Write("    Table Size: " + (set_linked as SetHashList<int>).TableSize);
			Console.WriteLine();
			Console.WriteLine();

			#endregion

			#region Map

			Console.WriteLine("  Testing MapHashList<int, int>--------------");
			Map<int, int> map_sethash = new MapHashLinked<int, int>(Compute<int>.Equate, Hash.Default);
			for (int i = 0; i < test; i++)
				map_sethash.Add(i, i);
			Console.Write("    Look Ups: ");
			for (int i = 0; i < test; i++)
				Console.Write(map_sethash[i]);
			Console.WriteLine();
			// Traversal
			Console.Write("    Traversal: ");
			map_sethash.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.Write("    Table Size: " + (map_sethash as MapHashLinked<int, int>).TableSize);
			Console.WriteLine();
			Console.WriteLine();

			#endregion

			#region Quad-Tree
			
			//Console.WriteLine("  Testing Quadtree_Array<int, double>--------");

			//// Construction
			//Quadtree<int, double> quadtree_array = new Quadtree_Array<int, double>(
			//	-test - 1, -test - 1, // minimum dimensions of the quadtree
			//	test + 1, test + 1, // maximum dimensions of the quadtree
			//	(int i, out double x, out double y) => { x = i; y = i; }, // 2D location function
			//	Logic.compare, // axis comparison function
			//	Statistics.Mean); // axis average function
			//// Adding
			//for (int i = 0; i < test; i++)
			//	quadtree_array.Add(i);
			//// Proper Traversal
			//Console.Write("    Traversal: ");
			//quadtree_array.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//// Saving to a file
			//string quadtreearray_file = "quadtree_array." + ToExtension(quadtree_array.GetType());
			//Console.WriteLine("    File: \"" + quadtreearray_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(quadtreearray_file, quadtree_array));
			//Quadtree_Array<int, double> deserialized_quadtreearray;
			//Console.WriteLine("    Deserialized: " + Deserialize(quadtreearray_file, out deserialized_quadtreearray));
			//Console.WriteLine();

			//Console.WriteLine("  Testing Quadtree_Linked<int, double>-------");

			//// Construction
			//Quadtree<int, double> quadtree_linked = new Quadtree_Linked<int, double>(
			//	-test - 1, -test - 1, // minimum dimensions of the quadtree
			//	test + 1, test + 1, // maximum dimensions of the quadtree
			//	(int i, out double x, out double y) => { x = i; y = i; }, // 2D location function
			//	Logic.compare, // axis comparison function
			//	(Quadtree.Average<double>)Statistics.Mean<double>); // axis average function
			//// Adding
			//for (int i = 0; i < test; i++)
			//	quadtree_linked.Add(i);
			//// Proper Traversal
			//Console.Write("    Traversal: ");
			//quadtree_linked.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//// Saving to a file
			//string quadtreelinked_file = "quadtree_linked." + ToExtension(quadtree_linked.GetType());
			//Console.WriteLine("    File: \"" + quadtreelinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(quadtreelinked_file, quadtree_linked));
			//Quadtree_Linked<int, double> deserialized_quadtreelinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(quadtreelinked_file, out deserialized_quadtreelinked));
			//Console.WriteLine();

			#endregion

			#region Oct-Tree
			
			//Console.WriteLine("  Testing Octree_Linked<int, double>---------");

			//// Construction
			//Octree<int, double> octree_linked = new Octree_Linked<int, double>(
			//	-test - 1, -test - 1, -test - 1, // minimum dimensions of the octree
			//	test + 1, test + 1, test + 1, // maximum dimensions of the octree
			//	(int i, out double x, out double y, out double z) => { x = i; y = i; z = i; }, // 3D location function
			//	Logic.compare, // axis comparison function
			//	Statistics.Mean); // axis average function
			//// Addition
			//for (int i = 0; i < test; i++)
			//	octree_linked.Add(i);
			//// Proper Traversal
			//Console.Write("    Traversal: ");
			//octree_linked.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//// Saving to a file
			//string octree_file = "octree_linked." + ToExtension(octree_linked.GetType());
			//Console.WriteLine("    File: \"" + octree_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(octree_file, octree_linked));
			//Octree_Linked<int, double> deserialized_octree;
			//Console.WriteLine("    Deserialized: " + Deserialize(octree_file, out deserialized_octree));
			//Console.WriteLine();

			#endregion

			#region Omnitree

			Console.WriteLine("  Testing OmnitreeLinkedLinked<int, double>-------");
			// Construction
			Omnitree<int, double> omnitree_linked = new OmnitreeLinked<int, double>(
				new double[] { -test - 1, -test - 1, -test - 1 }, // minimum dimensions of the omnitree
				new double[] { test + 1, test + 1, test + 1 }, // maximum dimensions of the omnitree
				(int index) => { return Accessor.Get(new double[] { index, index, index }); }, // "N-D" location function
				Compute<double>.Compare, // axis comparison function
				(double a, double b) => { return (a + b) / 2; }); // axis average function
			// Properties
			Console.WriteLine("      Origin: [" + omnitree_linked.Origin(0) + ", " + omnitree_linked.Origin(1) + ", " + omnitree_linked.Origin(2) + "]");
			Console.WriteLine("      Minimum: [" + omnitree_linked.Min(0) + ", " + omnitree_linked.Min(1) + ", " + omnitree_linked.Min(2) + "]");
			Console.WriteLine("      Maximum: [" + omnitree_linked.Max(0) + ", " + omnitree_linked.Max(1) + ", " + omnitree_linked.Max(2) + "]");
			Console.WriteLine("      Dimensions: " + omnitree_linked.Dimensions);
			Console.WriteLine("      Count: " + omnitree_linked.Count);
			// Addition
			Console.Write("    Adding 0-" + test + ": ");
			for (int i = 0; i < test; i++)
				omnitree_linked.Add(i);
			omnitree_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.WriteLine("      Count: " + omnitree_linked.Count);
			// Traversal
			Console.Write("    Traversal [ALL]: ");
			omnitree_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Look Up 1
			Console.Write("    Traversal [(" + (test / 2) + ", " + (test / 2) + ", " + (test / 2) + ")->(" + test + ", " + test + ", " + test + ")]: ");
			omnitree_linked.Stepper((int current) => { Console.Write(current); },
				new double[] { test / 2, test / 2, test / 2 },
				new double[] { test, test, test });
			Console.WriteLine();
			// Look Up 2
			Console.Write("    Look Up [" + (test / 3) + ", " + (test / 3) + ", " + (test / 3) + "]: ");
			omnitree_linked[(test / 3), (test / 3), (test / 3)]((int current) => { Console.Write(current); });
			Console.WriteLine();
			// Removal
			Console.Write("    Remove 0-" + test / 3 + ": ");
			omnitree_linked.Remove(
				new double[] { 0, 0, 0 },
				new double[] { test / 3, test / 3, test / 3 });
			omnitree_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.WriteLine("      Count: " + omnitree_linked.Count);
			// Clear
			Console.Write("    Clear: ");
			omnitree_linked.Clear();
			omnitree_linked.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.WriteLine("      Count: " + omnitree_linked.Count);
			// Saving to a file
			//string omnitreelinked_file = "omnitree_linkedlinkedlists." + ToExtension(omnitree_linked.GetType());
			//Console.WriteLine("    File: \"" + omnitreelinked_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(omnitreelinked_file, omnitree_linked));
			//OmnitreeLinkedLinkedLists<int, double> deserialized_omnitreeLinked;
			//Console.WriteLine("    Deserialized: " + Deserialize(omnitreelinked_file, out deserialized_omnitreeLinked));
			Console.WriteLine();

			//Console.WriteLine("  Testing Omnitree_LinkedArrayLists<int, double>--------");
			//// Construction
			//Omnitree<int, double> omnitree_array = new OmnitreeLinkedArray<int, double>(
			//	new double[] { -test - 1, -test - 1, -test - 1 }, // minimum dimensions of the omnitree
			//	new double[] { test + 1, test + 1, test + 1 }, // maximum dimensions of the omnitree
			//	(int index) => { return Accessor.Get(new double[] { index, index, index }); }, // "N-D" location function
			//	Compute<double>.Compare, // comparison function
			//	(double a, double b) => { return (a + b) / 2; }); // average function
			//// Properties
			//Console.WriteLine("      Origin: [" + omnitree_array.Origin(0) + ", " + omnitree_array.Origin(1) + ", " + omnitree_array.Origin(2) + "]");
			//Console.WriteLine("      Minimum: [" + omnitree_array.Min(0) + ", " + omnitree_array.Min(1) + ", " + omnitree_array.Min(2) + "]");
			//Console.WriteLine("      Maximum: [" + omnitree_array.Max(0) + ", " + omnitree_array.Max(1) + ", " + omnitree_array.Max(2) + "]");
			//Console.WriteLine("      Dimensions: " + omnitree_array.Dimensions);
			//Console.WriteLine("      Count: " + omnitree_array.Count);
			//// Addition
			//Console.Write("    Adding 0-" + test + ": ");
			//for (int i = 0; i < test; i++)
			//	omnitree_array.Add(i);
			//omnitree_array.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//Console.WriteLine("      Count: " + omnitree_array.Count);
			//// Traversal
			//Console.Write("    Traversal [ALL]: ");
			//			omnitree_array.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//// Look Up
			//Console.Write("    Traversal [" + (test / 2) + "-" + test + "]: ");
			//			omnitree_array.Stepper((int current) => { Console.Write(current); },
			//	new double[] { test / 2, test / 2, test / 2 },
			//	new double[] { test, test, test });
			//Console.WriteLine();
			//// Removal
			//Console.Write("    Remove 0-" + test / 3 + ": ");
			//omnitree_array.Remove(
			//	new double[] { 0, 0, 0 },
			//	new double[] { test / 3, test / 3, test / 3 });
			//omnitree_array.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//Console.WriteLine("      Count: " + omnitree_array.Count);
			//// Clear
			//Console.Write("    Clear: ");
			//omnitree_array.Clear();
			//			omnitree_array.Stepper((int current) => { Console.Write(current); });
			//Console.WriteLine();
			//Console.WriteLine("      Count: " + omnitree_array.Count);
			//// Saving to a file
			////string omnitreearray_file = "omnitree_linkedarraylists." + ToExtension(omnitree_array.GetType());
			////Console.WriteLine("    File: \"" + omnitreearray_file + "\"");
			////Console.WriteLine("    Serialized: " + Serialize(omnitreearray_file, omnitree_array));
			////OmnitreeLinkedLinkedLists<int, double> deserialized_omnitreearray;
			////Console.WriteLine("    Deserialized: " + Deserialize(omnitreearray_file, out deserialized_omnitreearray));
			//Console.WriteLine();

			#endregion

			#region KD Tree

			////List<KdTreeNode<float, string>> testNodes = new List_Linked<KdTreeNode<float, string>>();
			//KdTree_Linked<string, float> tree = new KdTree_Linked<string, float>(
			//	2,
			//	Logic.compare,
			//	float.MinValue,
			//	float.MaxValue,
			//	0,
			//	Arithmetic.Add,
			//	Arithmetic.Subtract,
			//	Arithmetic.Multiply);

			//List<KdTree_Linked<string, float>.Node> testNodes =
			//	new List_Linked<KdTree_Linked<string, float>.Node>
			//{
			//	new KdTree_Linked<string, float>.Node(new float[] { 5, 5 }, "Root"),
			//	new KdTree_Linked<string, float>.Node(new float[] { 2.5f, 2.5f }, "Root-Left"),
			//	new KdTree_Linked<string, float>.Node(new float[] { 7.5f, 7.5f }, "Root-Right"),
			//	new KdTree_Linked<string, float>.Node(new float[] { 1, 10 }, "Root-Left-Left"),
			//	new KdTree_Linked<string, float>.Node(new float[] { 10, 10 }, "Root-Right-Right")
			//};

			//foreach (var node in testNodes)
			//	if (!tree.Add(node.Point, node.Value))
			//		throw new Exception("Failed to add node to tree");

			//var nodesToRemove = new KdTreeNode<float, string>[] {
			//	testNodes[1], // Root-Left
			//	testNodes[0] // Root
			//};

			//foreach (var nodeToRemove in nodesToRemove)
			//{
			//	tree.RemoveAt(nodeToRemove.Point);
			//	testNodes.Remove(nodeToRemove);

			//	Assert.IsNull(tree.FindValue(nodeToRemove.Value));
			//	Assert.IsNull(tree.FindValueAt(nodeToRemove.Point));

			//	foreach (var testNode in testNodes)
			//	{
			//		Assert.AreEqual(testNode.Value, tree.FindValueAt(testNode.Point));
			//		Assert.AreEqual(testNode.Point, tree.FindValue(testNode.Value));
			//	}

			//	Assert.AreEqual(testNodes.Count, tree.Count);
			//}

			#endregion

			#region Graph

			Console.WriteLine("  Testing Graph_SetOmnitree<int>-------------");
			Graph<int> graph = new GraphSetOmnitree<int>(Compute<int>.Equate, Compute<int>.Compare, Hash.Default, 0, test, (int a, int b) => { return (a + b) / 2; });
			// add nodes
			for (int i = 0; i < test; i++)
				graph.Add(i);
			// add edges
			for (int i = 0; i < test - 1; i++)
				graph.Add(i, i + 1);
			Console.Write("    Traversal: ");
			graph.Stepper((int current) => { Console.Write(current); });
			Console.WriteLine();
			Console.WriteLine("    Edges: ");
			//((Graph_SetQuadtree<int>)graph)._edges.Foreach((Graph_SetQuadtree<int>.Edge e) => { Console.WriteLine("     " + e.Start + " " + e.End); });
			graph.Stepper(
					(int current) =>
					{
						Console.Write("     " + current + ": ");
						graph.Neighbors(current,
						(int a) =>
						{
							Console.Write(a);
						});
						Console.WriteLine();
					});
			// Saving to a file
			//string graph_file = "graph." + ToExtension(graph.GetType());
			//Console.WriteLine("    File: \"" + graph_file + "\"");
			//Console.WriteLine("    Serialized: " + Serialize(graph_file, graph));
			//GraphSetOmnitree<int> deserialized_graph;
			//Console.WriteLine("    Deserialized: " + Deserialize(graph_file, out deserialized_graph));
			Console.WriteLine();

			#endregion

			Console.WriteLine("============================================");
			Console.WriteLine("Examples Complete...");
			Console.ReadLine();
		}

		#region extra methods

		/// <summary>Serializes an object to a file.</summary>
		public static bool Serialize(string file, object obj)
		{
			try
			{
				IFormatter bin_formatter = new BinaryFormatter();
				using (Stream stream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
				{
					bin_formatter.Serialize(stream, obj);
				}
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		/// <summary>Deserialize a serialized object from a file.</summary>
		public static bool Deserialize<T>(string file, out T obj)
		{
			obj = default(T);
			try
			{
				IFormatter bin_formatter = new BinaryFormatter();
				using (Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.None))
				{
					obj = (T)bin_formatter.Deserialize(stream);
				}
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		/// <summary>Converts a C# type to a windows compliant file extension.</summary>
		public static string ToExtension(Type type)
		{
			string cs_string = Meta.ConvertTypeToCsharpSource(type);
			string truncated_namespaces = cs_string.Replace("Seven.Structures.", string.Empty).Replace("System.", string.Empty);
			string formatted = truncated_namespaces.Replace('<', '(').Replace('>', ')').Replace(",", string.Empty).Replace(' ', '+');
			string lower = formatted.ToLower();
			return formatted;
		}

		public static Get<double> Locate3D(int i)
		{
			return Accessor.Get(new double[] { i, i, i });
		}

		#endregion
	}
}