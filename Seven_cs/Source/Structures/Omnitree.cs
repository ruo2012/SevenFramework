﻿// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

namespace Seven.Structures
{
	/// <summary>Sorts items along N dimensions. The one data structure to rule them all. Made by Zachary Patten.</summary>
	/// <typeparam name="T">The generice type of items to be stored in this omnitree.</typeparam>
	/// <typeparam name="M">The type of the axis dimensions to sort the "T" values upon.</typeparam>
	public interface Omnitree<T, M> : Structure<T>
	{
		#region member

		/// <summary>Gets the dimensions of the center point of the Omnitree.</summary>
		Get<M> Origin { get; }
		/// <summary>The minimum dimensions of the Omnitree.</summary>
		Get<M> Min { get; }
		/// <summary>The maximum dimensions of the Omnitree.</summary>
		Get<M> Max { get; }
		/// <summary>The compare function the Omnitree is using.</summary>
		Compare<M> Compare { get; }
		/// <summary>The location function the Omnitree is using.</summary>
		Omnitree.Locate_Final<T, M> Locate { get; }
		/// <summary>The average function the Omnitree is using.</summary>
		Omnitree.Average<M> Average { get; }
		/// <summary>The number of dimensions in this tree.</summary>
		int Dimensions { get; }
		/// <summary>The current number of items in the tree.</summary>
		int Count { get; }

		/// <summary>Adds an item to the tree.</summary>
		/// <param name="addition">The item to be added.</param>
		void Add(T addition);
		/// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
		void Update();
		/// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
		/// <param name="min">The minimum dimensions of the space to update.</param>
		/// <param name="max">The maximum dimensions of the space to update.</param>
		void Update(Get<M> min, Get<M> max);
		/// <summary>Removes all the items in a given space.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		void Remove(Get<M> min, Get<M> max);
		/// <summary>Removes all the items in a given space where equality is met.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		void Remove(Get<M> min, Get<M> max, Predicate<T> where);
		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		void Stepper(Step<T> function, Get<M> min, Get<M> max);
		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		void Stepper(StepRef<T> function, Get<M> min, Get<M> max);
		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		StepStatus Stepper(StepBreak<T> function, Get<M> min, Get<M> max);
		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		StepStatus Stepper(StepRefBreak<T> function, Get<M> min, Get<M> max);
		/// <summary>Returns the tree to an empty state.</summary>
		void Clear();

		#endregion
	}

	/// <summary>Extension class for the Omnitree interface.</summary>
	public static class Omnitree
	{
		#region delegates

		///// <summary>Locates an item along the given dimensions.</summary>
		///// <typeparam name="T">The type of the item to locate.</typeparam>
		///// <typeparam name="M">The type of axis type of the Omnitree.</typeparam>
		///// <param name="item">The item to be located.</param>
		///// <returns>The computed locations of the item.</returns>
		//public delegate M[] Locate_NonOut<T, M>(T item);

		///// <summary>Locates an item along the given dimensions.</summary>
		///// <typeparam name="T">The type of the item to locate.</typeparam>
		///// <typeparam name="M">The type of axis type of the Omnitree.</typeparam>
		///// <param name="item">The item to be located.</param>
		///// <param name="ms">The computed locations of the item.</param>
		//public delegate void Locate<T, M>(T item, out M[] ms);

		/// <summary>Locates an item along the given dimensions.</summary>
		/// <typeparam name="T">The type of the item to locate.</typeparam>
		/// <typeparam name="M">The type of axis type of the Omnitree.</typeparam>
		/// <param name="item">The item to be located.</param>
		/// <param name="ms">The computed locations of the item.</param>
		public delegate Get<M> Locate_Final<T, M>(T item);

		/// <summary>Locates an item along the given dimensions.</summary>
		/// <typeparam name="T">The type of the item to locate.</typeparam>
		/// <typeparam name="M">The type of axis type of the Omnitree.</typeparam>
		/// <param name="item">The item to be located.</param>
		/// <param name="ms">The computed locations of the item.</param>
		public delegate System.Collections.Generic.IList<M> Locate_Final_wrapper<T, M>(T item);

		/// <summary>Computes the average between two items.</summary>
		/// <typeparam name="M">The type of items to average.</typeparam>
		/// <param name="left">The first item of the average.</param>
		/// <param name="right">The second item of the average.</param>
		/// <returns>The computed average between the two items.</returns>
		public delegate M Average<M>(M left, M right);

		#endregion

		#region extensions

		/// <summary>Gets the dimensions of the center point of the Omnitree.</summary>
		public static M[] Get_Origin<T, M>(this Omnitree<T, M> omniTree)
		{
			M[] array = new M[omniTree.Dimensions];
			for (int i = 0; i < omniTree.Dimensions; i++)
				array[i] = omniTree.Origin(i);
			return array;
		}

		/// <summary>The minimum dimensions of the Omnitree.</summary>
		public static M[] Get_Min<T, M>(this Omnitree<T, M> omniTree)
		{
			M[] array = new M[omniTree.Dimensions];
			for (int i = 0; i < omniTree.Dimensions; i++)
				array[i] = omniTree.Min(i);
			return array;
		}

		/// <summary>The maximum dimensions of the Omnitree.</summary>
		public static M[] Get_Max<T, M>(this Omnitree<T, M> omniTree)
		{
			M[] array = new M[omniTree.Dimensions];
			for (int i = 0; i < omniTree.Dimensions; i++)
				array[i] = omniTree.Max(i);
			return array;
		}

		/// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
		/// <param name="min">The minimum dimensions of the space to update.</param>
		/// <param name="max">The maximum dimensions of the space to update.</param>
		public static void Update<T, M>(this Omnitree<T, M> omniTree,
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max)
		{
			omniTree.Update(
				Accessor.Get(min),
				Accessor.Get(max));
		}

		/// <summary>Removes all the items in a given space.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		public static void Remove<T, M>(this Omnitree<T, M> omniTree,
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max)
		{
			omniTree.Remove(
				Accessor.Get(min),
				Accessor.Get(max));
		}

		/// <summary>Removes all the items in a given space where equality is met.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		public static void Remove<T, M>(this Omnitree<T, M> omniTree,
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max,
			Predicate<T> where)
		{
			omniTree.Remove(
				Accessor.Get(min),
				Accessor.Get(max),
				where);
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public static void Stepper<T, M>(this Omnitree<T, M> omniTree, Step<T> function,
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max)
		{
			omniTree.Stepper(
				function,
				Accessor.Get(min),
				Accessor.Get(max));
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public static void Stepper<T, M>(this Omnitree<T, M> omniTree, StepRef<T> function,
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max)
		{
			omniTree.Stepper(
				function,
				Accessor.Get(min),
				Accessor.Get(max));
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public static StepStatus Stepper<T, M>(this Omnitree<T, M> omniTree, StepBreak<T> function,
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max)
		{
			return omniTree.Stepper(
				function,
				Accessor.Get(min),
				Accessor.Get(max));
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public static StepStatus Stepper<T, M>(this Omnitree<T, M> omniTree, StepRefBreak<T> function,
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max)
		{
			return omniTree.Stepper(
				function,
				Accessor.Get(min),
				Accessor.Get(max));
		}

		#endregion
	}

	/// <summary>Sorts items along N dimensions. The one data structure to rule them all. Made by Zachary Patten.</summary>
	/// <typeparam name="T">The generice type of items to be stored in this octree.</typeparam>
	/// <typeparam name="M">The type of the axis dimensions to sort the "T" values upon.</typeparam>
	[System.Serializable]
	public class Omnitree_LinkedArrayLists<T, M> : Omnitree<T, M>
	{
		#region class

		/// <summary>Can be a leaf or a branch.</summary>
		[System.Serializable]
		private abstract class Node
		{
			private Get<M> _min;
			private Get<M> _max;
			private Branch _parent;
			private int _index;
			private int _count;

			internal Get<M> Min { get { return this._min; } }
			internal Get<M> Max { get { return this._max; } }
			internal Branch Parent { get { return this._parent; } }
			internal int Index { get { return this._index; } }
			internal int Count { get { return this._count; } set { this._count = value; } }

			internal Node(Get<M> min, Get<M> max, Branch parent, int index)
			{
				this._min = min;
				this._max = max;
				this._parent = parent;
				this._index = index;
			}
		}

		/// <summary>A leaf in the tree. Only contains items.</summary>
		[System.Serializable]
		private class Leaf : Node
		{
			private T[] _contents;

			public T[] Contents { get { return this._contents; } set { this._contents = value; } }

			public Leaf(Get<M> min, Get<M> max, Branch parent, int index)
				: base(min, max, parent, index)
			{
				this._contents = new T[_defaultLoad];
			}
		}

		/// <summary>A branch in the tree. Only contains nodes.</summary>
		[System.Serializable]
		private class Branch : Node
		{
			private Node[] _children;
			private int _childCount;

			internal int ChildCount { get { return this._childCount; } set { this._childCount = value; } }
			internal Node[] Children { get { return this._children; } set { this._children = value; } }

			public Branch(Get<M> min, Get<M> max, Branch parent, int index, int initialSize)
				: base(min, max, parent, index)
			{
				this._children = new Node[initialSize];
				this._childCount = 0;
			}
		}

		#endregion

		#region Omnitree_Array<T, M>

		#region field

		private const int _defaultLoad = 2;

		private Omnitree.Locate_Final<T, M> _locate;
		private Omnitree.Average<M> _average;
		private Compare<M> _compare;
		private int _dimensions;
		private int _children;
		private Get<M> _origin;

		private Node _top;
		private int _count;
		/// <summary>count ^ (1 / dimensions)</summary>
		private int _load;
		/// <summary>load ^ dimensions</summary>
		private int _loadPowered;
		/// <summary>(load + 1) ^ dimensions</summary>
		private int _loadPlusOnePowered;
		/// <summary>leaf depth of previous addition (sequencial addition optimization)</summary>
		private Node _previousAddition;
		/// <summary>tree depth of previous addition (sequencial addition optimization)</summary>
		private int _previousAdditionDepth;

		#endregion

		#region construct

		/// <summary>Constructor for an Omnitree_Linked.</summary>
		/// <param name="min">The minimum values of the tree.</param>
		/// <param name="max">The maximum values of the tree.</param>
		/// <param name="locate">A function for locating an item along the provided dimensions.</param>
		/// <param name="compare">A function for comparing two items of the types of the axis.</param>
		/// <param name="average">A function for computing the average between two items of the axis types.</param>
		public Omnitree_LinkedArrayLists(
			int dimensions,
			Get<M> min, Get<M> max,
			Omnitree.Locate_Final<T, M> locate,
			Compare<M> compare,
			Omnitree.Average<M> average)
		{
			// check the locate and compare delegates
			if (locate == null)
				throw new Error("null reference on location delegate during Omnitree construction");
			if (compare == null)
				throw new Error("null reference on compare delegate during Omnitree construction");

			// Check the min/max values
			if (min == null)
				throw new Error("null reference on min dimensions during Omnitree construction");
			if (max == null)
				throw new Error("null reference on max dimensions during Omnitree construction");
			for (int i = 0; i < dimensions; i++)
				if (compare(min(i), max(i)) != Comparison.Less)
					throw new Error("invalid min/max values. not all min values are less than the max values");

			// Check the average delegate
			if (average == null)
				throw new Error("null reference on average delegate during Omnitree construction");
			M[] origin = new M[dimensions];
			for (int i = 0; i < dimensions; i++)
				origin[i] = average(min(i), max(i));
			for (int i = 0; i < dimensions; i++)
				if (compare(min(i), origin[i]) != Comparison.Less || compare(origin[i], max(i)) != Comparison.Less)
					throw new Error("invalid average function. not all average values were computed to be between the min/max values.");

			this._locate = locate;
			this._average = average;
			this._compare = compare;
			this._dimensions = dimensions;
			this._children = 1 << this._dimensions;

			this._count = 0;
			this._top = new Leaf(min, max, null, -1);

			this._load = _defaultLoad;
			this._loadPlusOnePowered =
				Omnitree_LinkedArrayLists<T, M>.Int_Power(this._load + 1, this._dimensions);
			this._loadPowered =
				Omnitree_LinkedArrayLists<T, M>.Int_Power(_load, _dimensions);

			this._origin = Accessor.Get(origin);

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Constructor for an Omnitree_Linked.</summary>
		/// <param name="min">The minimum values of the tree.</param>
		/// <param name="max">The maximum values of the tree.</param>
		/// <param name="locate">A function for locating an item along the provided dimensions.</param>
		/// <param name="compare">A function for comparing two items of the types of the axis.</param>
		/// <param name="average">A function for computing the average between two items of the axis types.</param>
		public Omnitree_LinkedArrayLists(
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max,
			Omnitree.Locate_Final<T, M> locate,
			Compare<M> compare,
			Omnitree.Average<M> average)
		{
			#region error
#if no_error_checking
			// nothing
#else
			if (min.Count != max.Count)
				throw new Error("invalid min/max dimensions when constructing an Omnitree_Array");
#endif
			#endregion
		}

		#endregion

		#region methods

		/// <summary>Recursive version of the add function.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <param name="node">The current location of the tree.</param>
		/// <param name="ms">The location of the addition.</param>
		/// <param name="depth">The current depth of iteration.</param>
		private void Add(T addition, Node node, Get<M> ms, int depth)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				if (depth >= this._load || !(leaf.Count >= this._load))
				{
					Omnitree_LinkedArrayLists<T, M>.Leaf_Add(leaf, addition);

					this._previousAddition = leaf;
					this._previousAdditionDepth = depth;

					return;
				}
				else
				{
					Branch parent = node.Parent;
					Branch growth = GrowBranch(parent, leaf.Min, leaf.Max, this.DetermineChild(parent, ms));

					for (int i = 0; i < leaf.Count; i++)
					{
						Get<M> child_ms = this._locate(leaf.Contents[i]);

						if (child_ms == null)
							throw new Error("the location function for omnitree is invalid.");

						if (EncapsulationCheck(growth.Min, growth.Max, child_ms))
							Add(leaf.Contents[i], growth, child_ms, depth);
						else
						{
							if (EncapsulationCheck(this._top.Min, this._top.Max, child_ms))
							{
								this._count--;
								throw new Error("a node was updated to be out of bounds (found in an addition)");
							}
							else
								Add(leaf.Contents[i], this._top, child_ms, depth);
						}
					}

					Add(addition, growth, ms, depth);
					return;
				}
			}
			else
			{
				Branch branch = node as Branch;
				int child = this.DetermineChild(branch, ms);
				Node child_node = this.Branch_GetChild(branch, child);

				// null children in branches are just empty leaves
				if (child_node == null)
				{
					Leaf leaf = GrowLeaf(branch, child);
					Omnitree_LinkedArrayLists<T, M>.Leaf_Add(leaf, addition);

					this._previousAddition = leaf;
					this._previousAdditionDepth = depth + 1;

					branch.Count++;
					return;
				}

				Add(addition, child_node, ms, depth + 1);
				branch.Count++;
				return;
			}
		}

		/// <summary>Adds an item to a leaf.</summary>
		/// <param name="leaf">The leaf to add to.</param>
		/// <param name="addition">The item to add to the leaf.</param>
		private static void Leaf_Add(Leaf leaf, T addition)
		{
			if (leaf.Count == leaf.Contents.Length)
			{
				T[] newAllocaiton = new T[leaf.Contents.Length * 2];
				for (int i = 0; i < leaf.Contents.Length; i++)
					newAllocaiton[i] = leaf.Contents[i];
				leaf.Contents = newAllocaiton;
			}
			leaf.Contents[leaf.Count++] = addition;
		}

		/// <summary>Gets a child at of a given index in a branch.</summary>
		/// <param name="branch">The branch to get the child of.</param>
		/// <param name="index">The index of the child to get.</param>
		/// <returns>The value of the child.</returns>
		private Node Branch_GetChild(Branch branch, int index)
		{
			// fully allocated branch - use true indeces
			if (branch.Children.Length == this._children)
				return branch.Children[index];
			// partially allocated branch - use fake indeces
			for (int i = 0; i < branch.ChildCount; i++)
				if (branch.Children[i].Index == index)
					return branch.Children[i];
			return null;
		}

		/// <summary>Sets a child at of a given index in a branch.</summary>
		/// <param name="branch">The branch to set the child of.</param>
		/// <param name="index">The index of the child to set.</param>
		/// <param name="value">The value to be set.</param>
		private Node Branch_SetChild(Branch branch, int index, Node value)
		{
			// leaves are completely new nodes (as oposed to branches replacing leaves)
			if (value is Leaf)
			{
				// if the branch is fully allocated, treat the index as the
				// actual index we want to set
				if (branch.Children.Length == this._children)
					branch.Children[index] = value;
				// if the branch is not yet fully allocated - the index must
				// first be found in the partial allocation
				else
				{
					// the list array has reached capacity and needs to grow
					if (branch.ChildCount == branch.Children.Length)
					{
						Node[] newAllocation =
							new Node[branch.Children.Length * 2 > this._children ?
								branch.Children.Length * 2 : this._children];

						// if the growth will result in a fully allocated branch -
						// make the indeces true indeces
						if (newAllocation.Length == this._children)
						{
							foreach (Node node in branch.Children)
								newAllocation[node.Index] = node;
							newAllocation[index] = value;
							branch.Children = newAllocation;
							branch.ChildCount++;
						}
						// if the branch will not yet be fully allocated - just
						// keep using fake indeces
						else
						{
							for (int i = 0; i < branch.Children.Length; i++)
								newAllocation[i] = branch.Children[i];
							newAllocation[branch.ChildCount++] = value;
							branch.Children = newAllocation;
						}
					}
					// now growth required - just add using fake indeces
					else
						branch.Children[branch.ChildCount++] = value;
				}
			}
			else
			{
				// value is a branch; we need to overwrite a leaf
				for (int i = 0; i < branch.Children.Length; i++)
					if (branch.Children[i] == null || branch.Children[i].Index == index)
					{
						branch.Children[i] = value;
						break;
					}
			}
			return value;
		}

		/// <summary>Takes an integer to the power of the exponent.</summary>
		/// <param name="_base">The base operand of the power function.</param>
		/// <param name="exponent">The exponent operand of the power operand.</param>
		/// <returns>The computed (bease ^ exponent) value.</returns>
		private static int Int_Power(int _base, int exponent)
		{
			int result = 1;
			while (exponent > 0)
			{
				if ((exponent & 1) > 0)
					result *= _base;
				exponent >>= 1;
				_base *= _base;
			}
			return result;
		}

		/// <summary>Removes all the items qualified by the delegate.</summary>
		/// <param name="where">The predicate to qualify removals.</param>
		public void Remove(Predicate<T> where)
		{
			this._count -= this.Remove(this._top, where);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_LinkedArrayLists<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_LinkedArrayLists<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="where">The predicate to qualify removals.</param>
		private int Remove(Node node, Predicate<T> where)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					if (where(leaf.Contents[i]))
						removals++;
					else
						leaf.Contents[i - removals] = leaf.Contents[i];
				}
				leaf.Count -= removals;
				this._count -= removals;

				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null)
						{
							removals += this.Remove(branch.Children[i], where);
							if (branch.Children[i].Count == 0)
								ChopChild(branch, branch.Children[i--].Index);
						}
				}
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
					{
						removals += this.Remove(branch.Children[i], where);
						if (branch.Children[i].Count == 0)
							this.ChopChild(branch, branch.Children[i--].Index);
					}
				}
				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
			}
		}

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="min">The min dimensions of the removal space.</param>
		/// <param name="max">The max dimensions of the removal space.</param>
		private int Remove(Node node, Get<M> min, Get<M> max)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> ms = this._locate(leaf.Contents[i]);

					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");

					if (this.EncapsulationCheck(min, max, ms))
						removals++;
					else
						leaf.Contents[i - removals] = leaf.Contents[i];
				}
				leaf.Count -= removals;
				this._count -= removals;

				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
						{
							if (this.EncapsulationCheck(min, max, node.Min, node.Max))
							{
								removals += node.Count;
								ChopChild(branch, branch.Children[i--].Index);
							}
							else
							{
								removals += this.Remove(branch.Children[i], min, max);
								if (branch.Children[i].Count == 0)
									ChopChild(branch, branch.Children[i--].Index);
							}
						}
				}
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
						{
							if (this.EncapsulationCheck(min, max, node.Min, node.Max))
							{
								removals += node.Count;
								this.ChopChild(branch, branch.Children[i--].Index);
							}
							else
							{
								removals += this.Remove(branch.Children[i], min, max);
								if (branch.Children[i].Count == 0)
									this.ChopChild(branch, branch.Children[i--].Index);
							}
						}
				}
				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
			}
		}

		/// <summary>Converts a branch back into a leaf when the count is reduced.</summary>
		/// <param name="parent">The parent to shrink a child of.</param>
		/// <param name="child">The index of the child to shrink.</param>
		private void ShrinkChild(Branch parent, int child)
		{
			if (parent == null)
			{
				Node oldChild = this._top;
				this._top = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
				this.Stepper((T i) => { this.Add(i); }, oldChild);
			}
			else if (parent.Children.Length == this._children)
			{
				Node oldChild = parent.Children[child];
				parent.Children[child] = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
				this.Stepper((T i) => { this.Add(i); }, oldChild);
			}
			else
			{
				for (int i = 0; i < parent.ChildCount; i++)
				{
					if (parent.Children[i].Index == child)
					{
						Node oldChild = parent.Children[i];
						parent.Children[i] = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
						this.Stepper((T j) => { this.Add(j); }, oldChild);
						break;
					}
				}
			}
		}

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="min">The min dimensions of the removal space.</param>
		/// <param name="max">The max dimensions of the removal space.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		private int Remove(Node node, Get<M> min, Get<M> max, Predicate<T> where)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> ms = this._locate(leaf.Contents[i]);

					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");

					if (this.EncapsulationCheck(min, max, ms) && where(leaf.Contents[i]))
						removals++;
					else
						leaf.Contents[i - removals] = leaf.Contents[i];
				}
				leaf.Count -= removals;
				this._count -= removals;

				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null)
						{
							removals += this.Remove(branch.Children[i], min, max, where);
							if (branch.Children[i].Count == 0)
								ChopChild(branch, branch.Children[i--].Index);
						}
				}
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
					{
						removals += this.Remove(branch.Children[i], min, max, where);
						if (branch.Children[i].Count == 0)
							this.ChopChild(branch, branch.Children[i--].Index);
					}
				}
				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
			}
		}

		/// <summary>Grows a branch on the tree at the desired location</summary>
		/// <param name="branch">The branch to grow a branch on.</param>
		/// <param name="min">The minimum dimensions of the new branch.</param>
		/// <param name="max">The maximum dimensions of the new branch.</param>
		/// <param name="child">The child index to grow the branch on.</param>
		/// <returns>The newly constructed branch.</returns>
		private Branch GrowBranch(Branch branch, Get<M> min, Get<M> max, int child)
		{
			return this.Branch_SetChild(branch, child,
				new Branch(min, max, branch, child, _defaultLoad)) as Branch;
		}

		/// <summary>Grows a leaf on the tree at the desired location.</summary>
		/// <param name="branch">The branch to grow a leaf on.</param>
		/// <param name="child">The index to grow a leaf on.</param>
		/// <returns>The constructed leaf.</returns>
		private Leaf GrowLeaf(Branch branch, int child)
		{
			Get<M> min, max;
			this.DetermineChildBounds(branch, child, out min, out max);
			return this.Branch_SetChild(branch, child,
				new Leaf(min, max, branch, child)) as Leaf;
		}

		/// <summary>Computes the child index that contains the desired dimensions.</summary>
		/// <param name="node">The node to compute the child index of.</param>
		/// <param name="ms">The coordinates to find the child index of.</param>
		/// <returns>The computed child index based on the coordinates relative to the center of the node.</returns>
		private int DetermineChild(Node node, Get<M> ms)
		{
			int child = 0;
			for (int i = 0; i < this._dimensions; i++)
				if (!(_compare(ms(i), _average(node.Min(i), node.Max(i))) == Comparison.Less))
					child += 1 << i;
			return child;
		}

		/// <summary>Determins the dimensions of the child at the given index.</summary>
		/// <param name="node">The parent of the node to compute dimensions for.</param>
		/// <param name="child">The index of the child to compute dimensions for.</param>
		/// <param name="min">The computed minimum dimensions of the child node.</param>
		/// <param name="max">The computed maximum dimensions of the child node.</param>
		private void DetermineChildBounds(Node node, int child, out Get<M> min, out Get<M> max)
		{
			M[] min_array = new M[this._dimensions];
			M[] max_array = new M[this._dimensions];
			for (int i = _dimensions - 1; i >= 0; i--)
			{
				int temp = 1 << i;
				if (child >= temp)
				{
					min_array[i] = this._average(node.Min(i), node.Max(i));
					max_array[i] = node.Max(i);
					child -= temp;
				}
				else
				{
					min_array[i] = node.Min(i);
					max_array[i] = this._average(node.Min(i), node.Max(i));
				}
			}
			min = Accessor.Get(min_array);
			max = Accessor.Get(max_array);
		}

		/// <summary>Checks a node for inclusion (overlap) between two spaces.</summary>
		/// <param name="left_min">The minimum values of the left space.</param>
		/// <param name="left_max">The maximum values of the left space.</param>
		/// <param name="right_min">The minimum values of the right space.</param>
		/// <param name="right_max">The maximum values of the right space.</param>
		/// <returns>True if the spaces overlap; False if not.</returns>
		private bool InclusionCheck(Get<M> left_min, Get<M> left_max, Get<M> right_min, Get<M> right_max)
		{
			// if the right space is not outside the left space, they must overlap
			for (int j = 0; j < this._dimensions; j++)
				if (this._compare(left_max(j), right_min(j)) == Comparison.Less ||
					this._compare(left_min(j), right_max(j)) == Comparison.Greater)
					return false;
			return true;
		}

		/// <summary>Checks if a space encapsulates a point.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <param name="ms">The point.</param>
		/// <returns>True if the space encapsulates the point; False if not.</returns>
		private bool EncapsulationCheck(Get<M> min, Get<M> max, Get<M> ms)
		{
			// if the space is not outside the bounds, it must be inside
			for (int j = 0; j < this._dimensions; j++)
				if (this._compare(ms(j), min(j)) == Comparison.Less ||
					this._compare(ms(j), max(j)) == Comparison.Greater)
					return false;
			return true;
		}

		/// <summary>Checks if a space (left) encapsulates another space (right).</summary>
		/// <param name="left_min">The minimum values of the left space.</param>
		/// <param name="left_max">The maximum values of the left space.</param>
		/// <param name="right_min">The minimum values of the right space.</param>
		/// <param name="right_max">The maximum values of the right space.</param>
		/// <returns>True if the left space encapsulates the right; False if not.</returns>
		private bool EncapsulationCheck(Get<M> left_min, Get<M> left_max, Get<M> right_min, Get<M> right_max)
		{
			// if all the dimensions of the left space are 
			// beyond that of the right, the right is encapsulated
			for (int j = 0; j < this._dimensions; j++)
				if (this._compare(left_min(j), right_min(j)) != Comparison.Less ||
					this._compare(left_max(j), right_max(j)) != Comparison.Greater)
					return false;
			return true;
		}

		/// <summary>Chops (removes) a child from a branch.</summary>
		/// <param name="branch">The parent of the child to chop.</param>
		/// <param name="child">The index of the child to chop.</param>
		private void ChopChild(Branch branch, int child)
		{
			if (branch.Children.Length == this._children)
				branch.Children[child] = null;
			else
			{
				for (int i = 0; i < branch.ChildCount; i++)
					if (branch.Children[i].Index == child)
					{
						branch.Children[i] = branch.Children[branch.ChildCount - 1];
						branch.ChildCount--;
						break;
					}
			}
		}

		/// <summary>Recursive version of the Update method.</summary>
		/// <param name="node">The current node of iteration.</param>
		/// <param name="depth">The current depth of iteration.</param>
		private void Update(Node node, int depth)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> ms = this._locate(leaf.Contents[i]);

					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");

					if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
					{
						T item = leaf.Contents[i];
						leaf.Contents[i] = leaf.Contents[--leaf.Count];
						Node temp = leaf.Parent;
						while (temp != null)
						{
							if (EncapsulationCheck(temp.Min, temp.Max, ms))
								break;
							temp.Count--;
							temp = temp.Parent;
						}

						if (temp == null)
						{
							this._count--;
							throw new Error("a node was updated to be out of bounds (found in an update)");
						}
						else
							this.Add(item, temp, ms, depth);
					}
				}
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null)
						{
							this.Update(branch.Children[i], depth + 1);
							if (branch.Children[i].Count == 0)
								ChopChild(branch, branch.Children[i].Index);
						}
				}
				else
					for (int i = 0; i < branch.ChildCount; i++)
					{
						this.Update(branch.Children[i], depth + 1);
						if (branch.Children[i].Count == 0)
							ChopChild(branch, branch.Children[i].Index);
					}

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);
			}
		}

		/// <summary>Recursive version of the Update method.</summary>
		/// <param name="min">The minimum dimensions of the space to update.</param>
		/// <param name="max">The maximum dimensions of the space to update.</param>
		/// <param name="node">The current node of iteration.</param>
		/// <param name="depth">The current depth in the tree.</param>
		private void Update(Get<M> min, Get<M> max, Node node, int depth)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> ms = this._locate(leaf.Contents[i]);

					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");


					if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
					{
						T item = leaf.Contents[i];
						leaf.Contents[i] = leaf.Contents[--leaf.Count];
						Node temp = leaf.Parent;
						while (temp != null)
						{
							if (EncapsulationCheck(temp.Min, temp.Max, ms))
								break;
							temp.Count--;
							temp = temp.Parent;
						}

						if (temp == null)
						{
							this._count--;
							throw new Error("a node was updated to be out of bounds (found in an update)");
						}
						else
							this.Add(item, temp, ms, depth);
					}
				}
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (branch.Children[i] != null)
						{
							this.Update(branch.Children[i], depth + 1);
							if (branch.Children[i].Count == 0)
								ChopChild(branch, branch.Children[i].Index);
						}
				}
				else
					for (int i = 0; i < branch.ChildCount; i++)
					{
						this.Update(branch.Children[i], depth + 1);
						if (branch.Children[i].Count == 0)
							ChopChild(branch, branch.Children[i].Index);
					}

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);
			}
		}

		private void Stepper(StepRef<T> function, Node node)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
					function(ref leaf.Contents[i]);
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					foreach (Node child in branch.Children)
						if (child != null)
							this.Stepper(function, child);
				}
				else
					for (int i = 0; i < branch.ChildCount; i++)
						this.Stepper(function, branch.Children[i]);
			}
		}

		/// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
		/// <param name="function">The delegate to perform on every item in the tree.</param>
		public StepStatus Stepper(StepRefBreak<T> function)
		{
			return Stepper(function, this._top);
		}
		private StepStatus Stepper(StepRefBreak<T> function, Node node)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
					if (function(ref leaf.Contents[i]) == StepStatus.Break)
						return StepStatus.Break;
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					foreach (Node child in branch.Children)
						if (child != null)
							if (this.Stepper(function, child) == StepStatus.Break)
								return StepStatus.Break;
				}
				else
					for (int i = 0; i < branch.ChildCount; i++)
						if (this.Stepper(function, branch.Children[i]) == StepStatus.Break)
							return StepStatus.Break;
			}
			return StepStatus.Continue;
		}

		private void Stepper(Step<T> function, Node node, Get<M> min, Get<M> max)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> ms = this._locate(leaf.Contents[i]);

					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");

					if (EncapsulationCheck(min, max, ms))
						function(leaf.Contents[i]);
				}
			}
			else
			{
				Branch branch = node as Branch;
				// if the length of the child array is equal to its maximum capacity,
				// use real indeces and skip null children
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.Children.Length; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							this.Stepper(function, branch.Children[i], min, max);
				}
				// if the length of the child array is less than its maximum capacity,
				// iterate through the artificially index children where there will be no null values
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							this.Stepper(function, branch.Children[i], min, max);
				}
			}
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public void Stepper(StepRef<T> function, Get<M> min, Get<M> max)
		{
			Stepper(function, _top, min, max);
		}
		private void Stepper(StepRef<T> function, Node node, Get<M> min, Get<M> max)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> ms = this._locate(leaf.Contents[i]);

					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");

					if (EncapsulationCheck(min, max, ms))
						function(ref leaf.Contents[i]);
				}
			}
			else
			{
				Branch branch = node as Branch;
				// if the length of the child array is equal to its maximum capacity,
				// use real indeces and skip null children
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.Children.Length; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							this.Stepper(function, branch.Children[i], min, max);
				}
				// if the length of the child array is less than its maximum capacity,
				// iterate through the artificially index children where there will be no null values
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							this.Stepper(function, branch.Children[i], min, max);
				}
			}
		}
				
		private StepStatus Stepper(StepBreak<T> function, Node node, Get<M> min, Get<M> max)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> ms = this._locate(leaf.Contents[i]);

					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");

					if (EncapsulationCheck(min, max, ms))
						if (function(leaf.Contents[i]) == StepStatus.Break)
							return StepStatus.Break;
				}
			}
			else
			{
				Branch branch = node as Branch;
				// if the length of the child array is equal to its maximum capacity,
				// use real indeces and skip null children
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.Children.Length; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							if (this.Stepper(function, branch.Children[i], min, max) == StepStatus.Break)
								return StepStatus.Break;
				}
				// if the length of the child array is less than its maximum capacity,
				// iterate through the artificially index children where there will be no null values
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							if (this.Stepper(function, branch.Children[i], min, max) == StepStatus.Break)
								return StepStatus.Break;
				}
			}
			return StepStatus.Continue;
		}
				
		private StepStatus Stepper(StepRefBreak<T> function, Node node, Get<M> min, Get<M> max)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> ms = this._locate(leaf.Contents[i]);

					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");

					if (EncapsulationCheck(min, max, ms))
						if (function(ref leaf.Contents[i]) == StepStatus.Break)
							return StepStatus.Break;
				}
			}
			else
			{
				Branch branch = node as Branch;
				// if the length of the child array is equal to its maximum capacity,
				// use real indeces and skip null children
				if (branch.Children.Length == this._children)
				{
					for (int i = 0; i < branch.Children.Length; i++)
						if (branch.Children[i] != null && InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							if (this.Stepper(function, branch.Children[i], min, max) == StepStatus.Break)
								return StepStatus.Break;
				}
				// if the length of the child array is less than its maximum capacity,
				// iterate through the artificially index children where there will be no null values
				else
				{
					for (int i = 0; i < branch.ChildCount; i++)
						if (InclusionCheck(branch.Children[i].Min, branch.Children[i].Max, min, max))
							if (this.Stepper(function, branch.Children[i], min, max) == StepStatus.Break)
								return StepStatus.Break;
				}
			}
			return StepStatus.Continue;
		}

		/// <summary>Puts all the items on this tree into an array.</summary>
		/// <returns>The array containing all the items within the tree.</returns>
		public T[] ToArray()
		{
			int index = 0;
			T[] array = new T[this._count];
			this.Stepper((T entry) => { array[index++] = entry; });
			return array;
		}

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public Omnitree_LinkedArrayLists<T, M> Clone()
		{
			// OPTIMIZATION NEEDED
			Omnitree_LinkedArrayLists<T, M> clone = new Omnitree_LinkedArrayLists<T, M>(
				this._dimensions,
				this._top.Min,this._top.Max,
				this._locate, this._compare, this._average);
			this.Stepper((T current) => { clone.Add(current); });
			return clone;
		}

		#endregion

		#endregion

		#region Omnitree<T, M>

		#region property

		/// <summary>Gets the dimensions of the center point of the Omnitree.</summary>
		public Get<M> Origin { get { return this._origin; } }

		/// <summary>The minimum dimensions of the Omnitree.</summary>
		public Get<M> Min { get { return this._top.Min; } }

		/// <summary>The maximum dimensions of the Omnitree.</summary>
		public Get<M> Max { get { return this._top.Max; } }

		/// <summary>The number of dimensions in this tree.</summary>
		public int Dimensions { get { return this._dimensions; } }

		/// <summary>The current number of items in the tree.</summary>
		public int Count { get { return this._count; } }

		/// <summary>The compare function the Omnitree is using.</summary>
		public Compare<M> Compare { get { return this._compare; } }

		/// <summary>The location function the Omnitree is using.</summary>
		public Omnitree.Locate_Final<T, M> Locate { get { return this._locate; } }

		/// <summary>The average function the Omnitree is using.</summary>
		public Omnitree.Average<M> Average { get { return this._average; } }

		#endregion

		#region method

		/// <summary>Adds an item to the tree.</summary>
		/// <param name="addition">The item to be added.</param>
		public void Add(T addition)
		{
			if (this._count == int.MaxValue)
				throw new Error("(Count == int.MaxValue) max Omnitree size reached (change ints to longs if you need to).");

			// dynamic tree sizes
			if (this._loadPlusOnePowered < this._count)
			{
				this._load++;
				this._loadPlusOnePowered =
					Omnitree_LinkedArrayLists<T, M>.Int_Power(this._load + 1, this._dimensions);
				this._loadPowered =
					Omnitree_LinkedArrayLists<T, M>.Int_Power(this._load, this._dimensions);
			}

			Get<M> ms = this._locate(addition);

			if (ms == null)
				throw new Error("the location function for omnitree is invalid.");

			if (!EncapsulationCheck(this._top.Min, this._top.Max, ms))
				throw new Error("out of bounds during addition");

			if (this._top is Leaf && (this._top as Leaf).Count >= this._load)
			{
				Leaf leaf = this._top as Leaf;
				this._top = new Branch(this._top.Min, this._top.Max, null, -1, _defaultLoad);

				for (int i = 0; i < leaf.Count; i++)
				{
					Get<M> child_ms = this._locate(leaf.Contents[i]);

					if (child_ms == null)
						throw new Error("the location function for omnitree is invalid.");

					if (!EncapsulationCheck(this._top.Min, this._top.Max, child_ms))
					{
						this._count--;
						throw new Error("a node was updated to be out of bounds (found in an addition)");
					}
					else
						Add(leaf.Contents[i], this._top, child_ms, 0);
				}
			}

			if (this.EncapsulationCheck(this._previousAddition.Min, this._previousAddition.Max, ms))
				this.Add(addition, this._previousAddition, ms, this._previousAdditionDepth);
			else
				this.Add(addition, _top, ms, 0);
			this._count++;
		}

		/// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
		public void Update()
		{
			this.Update(this._top, 0);

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}


		/// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
		/// <param name="min">The minimum dimensions of the space to update.</param>
		/// <param name="max">The maximum dimensions of the space to update.</param>
		public void Update(Get<M> min, Get<M> max)
		{
			this.Update(min, max, this._top, 0);

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}


		/// <summary>Removes all the items in a given space.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <returns>The number of items that were removed.</returns>
		public void Remove(Get<M> min, Get<M> max)
		{
			this._count -= this.Remove(this._top, min, max);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_LinkedArrayLists<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_LinkedArrayLists<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}


		/// <summary>Removes all the items in a given space validated by a predicate.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		public void Remove(Get<M> min, Get<M> max, Predicate<T> where)
		{
			this._count -= this.Remove(this._top, min, max, where);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_LinkedArrayLists<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_LinkedArrayLists<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public void Stepper(Step<T> function, Get<M> min, Get<M> max)
		{
			Stepper(function, _top, min, max);
		}

		/// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
		/// <param name="function">The delegate to perform on every item in the tree.</param>
		public void Stepper(StepRef<T> function)
		{
			Stepper(function, this._top);
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public StepStatus Stepper(StepBreak<T> function, Get<M> min, Get<M> max)
		{
			return Stepper(function, _top, min, max);
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public StepStatus Stepper(StepRefBreak<T> function, Get<M> min, Get<M> max)
		{
			return Stepper(function, _top, min, max);
		}

		/// <summary>Returns the tree to an empty state.</summary>
		public void Clear()
		{
			this._top = new Leaf(this._top.Min, this._top.Max, null, -1);
			this._count = 0;

			this._load = _defaultLoad;
			this._loadPlusOnePowered =
				Omnitree_LinkedArrayLists<T, M>.Int_Power(this._load + 1, this._dimensions);
			this._loadPowered =
				Omnitree_LinkedArrayLists<T, M>.Int_Power(_load, _dimensions);
		}

		#endregion

		#endregion

		#region Structure<T>

		/// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
		/// <param name="function">The delegate to perform on every item in the tree.</param>
		public void Stepper(Step<T> function)
		{
			this.Stepper(function, this._top);
		}
		private void Stepper(Step<T> function, Node node)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
					function(leaf.Contents[i]);
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.Children.Length == this._children)
				{
					foreach (Node child in branch.Children)
						if (child != null)
							this.Stepper(function, child);
				}
				else
					for (int i = 0; i < branch.ChildCount; i++)
						this.Stepper(function, branch.Children[i]);
			}
		}

		/// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
		/// <param name="function">The delegate to perform on every item in the tree.</param>
		public StepStatus Stepper(StepBreak<T> function)
		{
			return Stepper(function, this._top);
		}
		private StepStatus Stepper(StepBreak<T> function, Node node)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				for (int i = 0; i < leaf.Count; i++)
					if (function(leaf.Contents[i]) == StepStatus.Break)
						return StepStatus.Break;
			}
			else
			{
				Branch branch = node as Branch;
				if (branch.ChildCount == this._children)
				{
					foreach (Node child in branch.Children)
						if (child != null)
							if (this.Stepper(function, child) == StepStatus.Break)
								return StepStatus.Break;
				}
				else
					for (int i = 0; i < branch.ChildCount; i++)
						if (this.Stepper(function, branch.Children[i]) == StepStatus.Break)
							return StepStatus.Break;
			}
			return StepStatus.Continue;
		}

		#endregion

		#region IEnumerator<T>

		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			T[] array = this.ToArray();
			return array.GetEnumerator();
		}

		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			T[] array = this.ToArray();
			return ((System.Collections.Generic.IEnumerable<T>)array).GetEnumerator();
		}

		#endregion
	}

	/// <summary>Sorts items along N dimensions. The one data structure to rule them all. Made by Zachary Patten.</summary>
	/// <typeparam name="T">The generice type of items to be stored in this octree.</typeparam>
	/// <typeparam name="M">The type of the axis dimensions to sort the "T" values upon.</typeparam>
	[System.Serializable]
	public class Omnitree_LinkedLinkedLists<T, M> : Omnitree<T, M>
	{
		#region class

		/// <summary>Can be a leaf or a branch.</summary>
		[System.Serializable]
		private abstract class Node
		{
			private Get<M> _min;
			private Get<M> _max;
			private Branch _parent;
			private int _index;
			private int _count;

			internal Get<M> Min { get { return this._min; } }
			internal Get<M> Max { get { return this._max; } }
			internal Branch Parent { get { return this._parent; } }
			internal int Index { get { return this._index; } }
			internal int Count { get { return this._count; } set { this._count = value; } }

			internal Node(Get<M> min, Get<M> max, Branch parent, int index)
			{
				this._min = min;
				this._max = max;
				this._parent = parent;
				this._index = index;
			}
		}

		/// <summary>A branch in the tree. Only contains items.</summary>
		[System.Serializable]
		private class Leaf : Node
		{
			internal class Node
			{
				internal T _value;
				internal Node _next;

				public T Value { get { return _value; } set { _value = value; } }
				public Node Next { get { return _next; } set { _next = value; } }

				public Node(T value, Node next)
				{
					_value = value;
					_next = next;
				}
			}

			private Node _head;
			private int _count;

			public Node Head { get { return this._head; } set { this._head = value; } }

			public Leaf(Get<M> min, Get<M> max, Branch parent, int index)
				: base(min, max, parent, index) { }
		}

		/// <summary>A branch in the tree. Only contains nodes.</summary>
		[System.Serializable]
		private class Branch : Node
		{
			internal class Node
			{
				private Omnitree_LinkedLinkedLists<T, M>.Node _value;
				private Branch.Node _next;

				public Omnitree_LinkedLinkedLists<T, M>.Node Value { get { return this._value; } }
				public Branch.Node Next { get { return this._next; } set { this._next = value; } }

				public Node(Omnitree_LinkedLinkedLists<T, M>.Node value, Branch.Node next)
				{
					this._value = value;
					this._next = next;
				}
			}

			private Branch.Node _head;

			public Branch.Node Head { get { return this._head; } set { this._head = value; } }

			public Branch(Get<M> min, Get<M> max, Branch parent, int index)
				: base(min, max, parent, index) { }
		}

		#endregion

		#region field

		private const int _defaultLoad = 2;

		private Omnitree.Locate_Final<T, M> _locate;
		private Omnitree.Average<M> _average;
		private Compare<M> _compare;
		private int _dimensions;
		private int _children;
		private Get<M> _origin;

		private Node _top;
		private int _count;
		/// <summary>count ^ (1 / dimensions)</summary>
		private int _load;
		/// <summary>load ^ dimensions</summary>
		private int _loadPowered;
		/// <summary>(load + 1) ^ dimensions</summary>
		private int _loadPlusOnePowered;
		/// <summary>leaf depth of previous addition (sequencial addition optimization)</summary>
		private Node _previousAddition;
		/// <summary>tree depth of previous addition (sequencial addition optimization)</summary>
		private int _previousAdditionDepth;

		#endregion
		
		#region Property

		/// <summary>Gets the dimensions of the center point of the Omnitree.</summary>
		public Get<M> Origin { get { return this._origin; } }

		/// <summary>The minimum dimensions of the Omnitree.</summary>
		public Get<M> Min { get { return this._top.Min; } }

		/// <summary>The maximum dimensions of the Omnitree.</summary>
		public Get<M> Max { get { return this._top.Max; } }

		/// <summary>The number of dimensions in this tree.</summary>
		public int Dimensions { get { return this._dimensions; } }

		/// <summary>The current number of items in the tree.</summary>
		public int Count { get { return this._count; } }

		/// <summary>The compare function the Omnitree is using.</summary>
		public Compare<M> Compare { get { return this._compare; } }

		/// <summary>The location function the Omnitree is using.</summary>
		public Omnitree.Locate_Final<T, M> Locate { get { return this._locate; } }

		/// <summary>The average function the Omnitree is using.</summary>
		public Omnitree.Average<M> Average { get { return this._average; } }

		#endregion

		#region construct

		/// <summary>Constructor for an Omnitree_Linked2.</summary>
		/// <param name="min">The minimum values of the tree.</param>
		/// <param name="max">The maximum values of the tree.</param>
		/// <param name="locate">A function for locating an item along the provided dimensions.</param>
		/// <param name="compare">A function for comparing two items of the types of the axis.</param>
		/// <param name="average">A function for computing the average between two items of the axis types.</param>
		public Omnitree_LinkedLinkedLists(
			int dimensions,
			Get<M> min, Get<M> max,
			Omnitree.Locate_Final<T, M> locate,
			Compare<M> compare,
			Omnitree.Average<M> average)
		{
			#region error
#if no_error_checking
			// nothing
#else
			// check the locate and compare delegates
			if (locate == null)
				throw new Error("null reference on location delegate during Omnitree construction");
			if (compare == null)
				throw new Error("null reference on compare delegate during Omnitree construction");

			// Check the min/max values
			if (min == null)
				throw new Error("null reference on min dimensions during Omnitree construction");
			if (max == null)
				throw new Error("null reference on max dimensions during Omnitree construction");
			for (int i = 0; i < dimensions; i++)
				if (compare(min(i), max(i)) != Comparison.Less)
					throw new Error("invalid min/max values. not all min values are less than the max values");

			// Check the average delegate
			if (average == null)
				throw new Error("null reference on average delegate during Omnitree construction");
#endif
			#endregion

			M[] origin = new M[dimensions];
			for (int i = 0; i < dimensions; i++)
				origin[i] = average(min(i), max(i));

			this._locate = locate;
			this._average = average;
			this._compare = compare;
			this._dimensions = dimensions;
			this._children = 1 << this._dimensions;

			this._count = 0;
			this._top = new Leaf(min, max, null, -1);

			this._load = _defaultLoad;
			this._loadPlusOnePowered =
				Omnitree_LinkedLinkedLists<T, M>.Int_Power(this._load + 1, this._dimensions);
			this._loadPowered =
				Omnitree_LinkedLinkedLists<T, M>.Int_Power(_load, _dimensions);

			this._origin = Accessor.Get(origin);

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Constructor for an Omnitree_Linked2.</summary>
		/// <param name="min">The minimum values of the tree.</param>
		/// <param name="max">The maximum values of the tree.</param>
		/// <param name="locate">A function for locating an item along the provided dimensions.</param>
		/// <param name="compare">A function for comparing two items of the types of the axis.</param>
		/// <param name="average">A function for computing the average between two items of the axis types.</param>
		public Omnitree_LinkedLinkedLists(
			System.Collections.Generic.IList<M> min,
			System.Collections.Generic.IList<M> max,
			Omnitree.Locate_Final<T, M> locate,
			Compare<M> compare,
			Omnitree.Average<M> average) :
			this(
				min.Count,
				Accessor.Get(min),
				Accessor.Get(max),
				locate,
				compare,
				average)
		{
			#region error
#if no_error_checking
			// nothing
#else
			if (min.Count != max.Count)
				throw new Error("invalid min/max dimensions when constructing an Omnitree_Linked");
#endif
			#endregion
		}

		///// <summary>Constructor for an Omnitree_Linked2.</summary>
		///// <param name="min">The minimum values of the tree.</param>
		///// <param name="max">The maximum values of the tree.</param>
		///// <param name="locate">A function for locating an item along the provided dimensions.</param>
		///// <param name="compare">A function for comparing two items of the types of the axis.</param>
		///// <param name="average">A function for computing the average between two items of the axis types.</param>
		//public Omnitree_Linked2(
		//	int dimensions,
		//	Get<M> min, Get<M> max,
		//	Omnitree.Locate_NonOut<T, M> locate,
		//	Compare<M> compare,
		//	Omnitree.Average<M> average)
		//	: this(
		//	dimensions,
		//	min, max,
		//		// this is just an adapter - JIT should optimize
		//	(T item, out M[] ms) => { ms = locate(item); },
		//	compare, average)
		//{

		//}

		#endregion

		#region method

		/// <summary>Adds an item to the tree.</summary>
		/// <param name="addition">The item to be added.</param>
		public void Add(T addition)
		{
			#region error
#if no_error_checking
			// nothing
#else
			if (this._count == int.MaxValue)
				throw new Error("(Count == int.MaxValue) max Omnitree size reached (change ints to longs if you need to).");
#endif
			#endregion

			// dynamic tree sizes
			if (this._loadPlusOnePowered < this._count)
			{
				this._load++;
				this._loadPlusOnePowered =
					Omnitree_LinkedLinkedLists<T, M>.Int_Power(this._load + 1, this._dimensions);
				this._loadPowered =
					Omnitree_LinkedLinkedLists<T, M>.Int_Power(this._load, this._dimensions);
			}

			Get<M> ms = this._locate(addition);

			#region error
#if no_error_checking
			// nothing
#else
			if (ms == null)
				throw new Error("the location function for omnitree is invalid.");
#endif
			#endregion

			#region error
#if no_error_checking
			// nothing
#else
			if (!EncapsulationCheck(this._top.Min, this._top.Max, ms))
				throw new Error("a node was updated to be out of bounds (found in an addition)");
#endif
			#endregion

			if (this._top is Leaf && (this._top as Leaf).Count >= this._load)
			{
				Leaf leaf = this._top as Leaf;
				this._top = new Branch(this._top.Min, this._top.Max, null, -1);

				for (Leaf.Node list = leaf.Head; list != null; list = list.Next)
				{
					Get<M> child_ms = this._locate(list.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (child_ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (!EncapsulationCheck(this._top.Min, this._top.Max, child_ms))
					{
						this._count--;

						#region error
#if no_error_checking
						// nothing
#else
						throw new Error("a node was updated to be out of bounds (found in an addition)");
#endif
						#endregion
					}
					else
						Add(list.Value, this._top, child_ms, 0);
				}

				this._previousAddition = this._top;
				this._previousAdditionDepth = 0;
			}

			if (this.EncapsulationCheck(this._previousAddition.Min, this._previousAddition.Max, ms))
				this.Add(addition, this._previousAddition, ms, this._previousAdditionDepth);
			else
				this.Add(addition, _top, ms, 0);
			this._count++;
		}

		/// <summary>Recursive version of the add function.</summary>
		/// <param name="addition">The item to be added.</param>
		/// <param name="node">The current location of the tree.</param>
		/// <param name="ms">The location of the addition.</param>
		/// <param name="depth">The current depth of iteration.</param>
		private void Add(T addition, Node node, Get<M> ms, int depth)
		{
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				if (depth >= this._load || !(leaf.Count >= this._load))
				{
					Omnitree_LinkedLinkedLists<T, M>.Leaf_Add(leaf, addition);

					this._previousAddition = leaf;
					this._previousAdditionDepth = depth;

					return;
				}
				else
				{
					Branch parent = node.Parent;
					Branch growth = GrowBranch(parent, leaf.Min, leaf.Max, this.DetermineChild(parent, ms));

					for (Leaf.Node list = leaf.Head; list != null; list = list.Next)
					{
						Get<M> child_ms = this._locate(list.Value);

						#region error
#if no_error_checking
						// nothing
#else
						if (child_ms == null)
							throw new Error("the location function for omnitree is invalid.");
#endif
						#endregion

						if (EncapsulationCheck(growth.Min, growth.Max, child_ms))
							Add(list.Value, growth, child_ms, depth);
						else
						{
							if (EncapsulationCheck(this._top.Min, this._top.Max, child_ms))
							{
								this._count--;

								#region error
#if no_error_checking
								// nothing
#else
								throw new Error("a node was updated to be out of bounds (found in an addition)");
#endif
								#endregion
							}
							else
								Add(list.Value, this._top, child_ms, depth);
						}
					}

					Add(addition, growth, ms, depth);
					return;
				}
			}
			else
			{
				Branch branch = node as Branch;
				int child = this.DetermineChild(branch, ms);
				Node child_node = this.Branch_GetChild(branch, child);

				// null children in branches are just empty leaves
				if (child_node == null)
				{
					Leaf leaf = GrowLeaf(branch, child);
					Omnitree_LinkedLinkedLists<T, M>.Leaf_Add(leaf, addition);

					this._previousAddition = leaf;
					this._previousAdditionDepth = depth + 1;

					return;
				}

				Add(addition, child_node, ms, depth + 1);
				branch.Count++;
				return;
			}
			node.Count++;
		}

		/// <summary>Adds an item to a leaf.</summary>
		/// <param name="leaf">The leaf to add to.</param>
		/// <param name="addition">The item to add to the leaf.</param>
		private static void Leaf_Add(Leaf leaf, T addition)
		{
			leaf.Head = new Leaf.Node(addition, leaf.Head);
			leaf.Count++;
		}

		/// <summary>Gets a child at of a given index in a branch.</summary>
		/// <param name="branch">The branch to get the child of.</param>
		/// <param name="index">The index of the child to get.</param>
		/// <returns>The value of the child.</returns>
		private Node Branch_GetChild(Branch branch, int index)
		{
			Branch.Node child = branch.Head;
			while (child != null && child.Value.Index != index)
				child = child.Next;
			if (child == null)
				return null;
			else
				return child.Value;
		}

		/// <summary>Sets a child at of a given index in a branch.</summary>
		/// <param name="branch">The branch to set the child of.</param>
		/// <param name="index">The index of the child to set.</param>
		/// <param name="value">The value to be set.</param>
		private Node Branch_SetChild(Branch branch, int index, Node value)
		{
			// leaves are completely new nodes (as oposed to branches replacing leaves)
			if (value is Leaf)
				branch.Head = new Branch.Node(value, branch.Head);
			else
			{
				if (branch.Head.Value.Index == index)
					branch.Head = new Branch.Node(value, branch.Head.Next);
				else
				{
					Branch.Node list = branch.Head;
					while (list.Next.Value.Index != index)
						list = list.Next;
					list.Next = new Branch.Node(value, list.Next.Next);
				}
			}
			return value;
		}

		/// <summary>Takes an integer to the power of the exponent.</summary>
		/// <param name="_base">The base operand of the power function.</param>
		/// <param name="exponent">The exponent operand of the power operand.</param>
		/// <returns>The computed (bease ^ exponent) value.</returns>
		private static int Int_Power(int _base, int exponent)
		{
			int result = 1;
			while (exponent > 0)
			{
				if ((exponent & 1) > 0)
					result *= _base;
				exponent >>= 1;
				_base *= _base;
			}
			return result;
		}

		/// <summary>Removes all the items qualified by the delegate.</summary>
		/// <param name="where">The predicate to qualify removals.</param>
		public void Remove(Predicate<T> where)
		{
			this._count -= this.Remove(this._top, where);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_LinkedLinkedLists<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_LinkedLinkedLists<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="where">The predicate to qualify removals.</param>
		private int Remove(Node node, Predicate<T> where)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				while (leaf.Head != null && where(leaf.Head.Value))
				{
					leaf.Head = leaf.Head.Next;
					removals++;
				}
				if (leaf.Head != null)
				{
					Leaf.Node list = leaf.Head;
					while (list.Next != null)
					{
						if (where(list.Next.Value))
						{
							list.Next = list.Next.Next;
							removals++;
						}
					}
				}

				leaf.Count -= removals;
				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				Branch.Node list = branch.Head;

				while (list != null)
				{
					removals += this.Remove(list.Value, where);
					if (list.Value.Count == 0)
						ChopChild(branch, list.Value.Index);
					list = list.Next;
				}

				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
			}
		}

		/// <summary>Removes all the items in a given space.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <returns>The number of items that were removed.</returns>
		public void Remove(Get<M> min, Get<M> max)
		{
			this._count -= this.Remove(this._top, min, max);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_LinkedLinkedLists<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_LinkedLinkedLists<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="min">The min dimensions of the removal space.</param>
		/// <param name="max">The max dimensions of the removal space.</param>
		private int Remove(Node node, Get<M> min, Get<M> max)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				while (leaf.Head != null)
				{
					Get<M> ms = this._locate(leaf.Head.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (EncapsulationCheck(min, max, ms))
					{
						leaf.Head = leaf.Head.Next;
						removals++;
					}
				}
				if (leaf.Head != null)
				{
					Leaf.Node list = leaf.Head;
					while (list.Next != null)
					{
						Get<M> ms = this._locate(list.Next.Value);

						#region error
#if no_error_checking
						// nothing
#else
						if (ms == null)
							throw new Error("the location function for omnitree is invalid.");
#endif
						#endregion

						if (EncapsulationCheck(min, max, ms))
						{
							list.Next = list.Next.Next;
							removals++;
						}
					}
				}

				leaf.Count -= removals;
				return removals;
			}
			else
			{
				Branch branch = node as Branch;
				while (branch.Head != null && EncapsulationCheck(min, max, branch.Head.Value.Min, branch.Head.Value.Max))
				{
					removals += branch.Head.Value.Count;
					branch.Head = branch.Head.Next;
				}
				if (branch.Head != null)
				{
					Branch.Node list = branch.Head;
					while (list.Next != null)
					{
						if (EncapsulationCheck(min, max, list.Next.Value.Min, list.Next.Value.Max))
						{
							removals += list.Next.Value.Count;
							list.Next = list.Next.Next;
						}
						else
						{
							removals += Remove(list.Next.Value, min, max);
							if (list.Next.Value.Count == 0)
								ChopChild(branch, list.Next.Value.Index);
						}
					}
				}

				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);

				return removals;
			}
		}

		/// <summary>Converts a branch back into a leaf when the count is reduced.</summary>
		/// <param name="parent">The parent to shrink a child of.</param>
		/// <param name="child">The index of the child to shrink.</param>
		private void ShrinkChild(Branch parent, int child)
		{
			if (parent == null)
			{
				Node oldChild = this._top;
				this._top = new Leaf(oldChild.Min, oldChild.Max, parent, oldChild.Index);
				this.Stepper((T i) => { this.Add(i); }, oldChild);
			}
			else
			{
				Branch.Node list = parent.Head;
				if (list.Value.Index == child)
					parent.Head = parent.Head.Next;
				else
				{
					while (list != null)
					{
						if (list.Next.Value.Index == child)
						{
							Branch.Node shrinking = list.Next;
							list.Next = new Branch.Node(new Leaf(shrinking.Value.Min, shrinking.Value.Max, parent, shrinking.Value.Index), list.Next.Next);
							this.Stepper((T j) => { this.Add(j); }, shrinking.Value);
							break;
						}
						list = list.Next;
					}
				}
			}
		}

		/// <summary>Removes all the items in a given space validated by a predicate.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		public void Remove(Get<M> min, Get<M> max, Predicate<T> where)
		{
			this._count -= this.Remove(this._top, min, max, where);

			// dynamic tree sizes
			while (this._loadPowered > this._count && this._load > _defaultLoad)
			{
				this._load--;
				this._loadPlusOnePowered =
					Omnitree_LinkedLinkedLists<T, M>.Int_Power(_load + 1, _dimensions);
				this._loadPowered =
					Omnitree_LinkedLinkedLists<T, M>.Int_Power(_load, _dimensions);
			}

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Recursive version of the remove method.</summary>
		/// <param name="node">The current node of traversal.</param>
		/// <param name="min">The min dimensions of the removal space.</param>
		/// <param name="max">The max dimensions of the removal space.</param>
		/// <param name="where">The equality constraint of the removal.</param>
		private int Remove(Node node, Get<M> min, Get<M> max, Predicate<T> where)
		{
			int removals = 0;
			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				while (leaf.Head != null)
				{
					Get<M> ms = this._locate(leaf.Head.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (this.EncapsulationCheck(min, max, ms) && where(leaf.Head.Value))
					{
						leaf.Head = leaf.Head.Next;
						removals++;
					}
					else
						break;
				}
				if (leaf != null)
				{
					Leaf.Node list = (node as Leaf).Head;
					while (list.Next != null)
					{
						Get<M> ms = this._locate(list.Next.Value);

						if (this.EncapsulationCheck(min, max, ms) && where(list.Next.Value))
						{
							list.Next = list.Next.Next;
							removals++;
						}
					}
				}

				leaf.Count -= removals;
				this._count -= removals;

				return removals;
			}
			else
			{
				Branch.Node list = (node as Branch).Head;
				while (list != null)
				{
					this.Remove(list.Value, min, max, where);
					if (list.Value.Count == 0)
						this.ChopChild(list.Value.Parent, list.Value.Index);
				}

				node.Count -= removals;

				if (node.Count < this._load && node.Count != 0)
					ShrinkChild(node.Parent, node.Index);

				return removals;
			}
		}

		/// <summary>Grows a branch on the tree at the desired location</summary>
		/// <param name="branch">The branch to grow a branch on.</param>
		/// <param name="min">The minimum dimensions of the new branch.</param>
		/// <param name="max">The maximum dimensions of the new branch.</param>
		/// <param name="child">The child index to grow the branch on.</param>
		/// <returns>The newly constructed branch.</returns>
		private Branch GrowBranch(Branch branch, Get<M> min, Get<M> max, int child)
		{
			return this.Branch_SetChild(branch, child,
				new Branch(min, max, branch, child)) as Branch;
		}

		/// <summary>Grows a leaf on the tree at the desired location.</summary>
		/// <param name="branch">The branch to grow a leaf on.</param>
		/// <param name="child">The index to grow a leaf on.</param>
		/// <returns>The constructed leaf.</returns>
		private Leaf GrowLeaf(Branch branch, int child)
		{
			Get<M> min, max;
			this.DetermineChildBounds(branch, child, out min, out max);
			return this.Branch_SetChild(branch, child,
				new Leaf(min, max, branch, child)) as Leaf;
		}

		/// <summary>Computes the child index that contains the desired dimensions.</summary>
		/// <param name="node">The node to compute the child index of.</param>
		/// <param name="ms">The coordinates to find the child index of.</param>
		/// <returns>The computed child index based on the coordinates relative to the center of the node.</returns>
		private int DetermineChild(Node node, Get<M> ms)
		{
			int child = 0;
			for (int i = 0; i < this._dimensions; i++)
				if (!(_compare(ms(i), _average(node.Min(i), node.Max(i))) == Comparison.Less))
					child += 1 << i;
			return child;
		}

		/// <summary>Determins the dimensions of the child at the given index.</summary>
		/// <param name="node">The parent of the node to compute dimensions for.</param>
		/// <param name="child">The index of the child to compute dimensions for.</param>
		/// <param name="min">The computed minimum dimensions of the child node.</param>
		/// <param name="max">The computed maximum dimensions of the child node.</param>
		private void DetermineChildBounds(Node node, int child, out Get<M> min, out Get<M> max)
		{
			M[] min_array = new M[this._dimensions];
			M[] max_array = new M[this._dimensions];
			for (int i = _dimensions - 1; i >= 0; i--)
			{
				int temp = 1 << i;
				if (child >= temp)
				{
					min_array[i] = this._average(node.Min(i), node.Max(i));
					max_array[i] = node.Max(i);
					child -= temp;
				}
				else
				{
					min_array[i] = node.Min(i);
					max_array[i] = this._average(node.Min(i), node.Max(i));
				}
			}

			min = (int dimension) => { return min_array[dimension]; };
			max = (int dimension) => { return max_array[dimension]; };
		}

		/// <summary>Checks a node for inclusion (overlap) between two spaces.</summary>
		/// <param name="left_min">The minimum values of the left space.</param>
		/// <param name="left_max">The maximum values of the left space.</param>
		/// <param name="right_min">The minimum values of the right space.</param>
		/// <param name="right_max">The maximum values of the right space.</param>
		/// <returns>True if the spaces overlap; False if not.</returns>
		private bool InclusionCheck(Get<M> left_min, Get<M> left_max, Get<M> right_min, Get<M> right_max)
		{
			// if the right space is not outside the left space, they must overlap
			for (int j = 0; j < this._dimensions; j++)
				if (this._compare(left_max(j), right_min(j)) == Comparison.Less ||
					this._compare(left_min(j), right_max(j)) == Comparison.Greater)
					return false;
			return true;
		}

		/// <summary>Checks if a space encapsulates a point.</summary>
		/// <param name="min">The minimum values of the space.</param>
		/// <param name="max">The maximum values of the space.</param>
		/// <param name="ms">The point.</param>
		/// <returns>True if the space encapsulates the point; False if not.</returns>
		private bool EncapsulationCheck(Get<M> min, Get<M> max, Get<M> ms)
		{
			// if the space is not outside the bounds, it must be inside
			for (int j = 0; j < this._dimensions; j++)
				if (this._compare(ms(j), min(j)) == Comparison.Less ||
					this._compare(ms(j), max(j)) == Comparison.Greater)
					return false;
			return true;
		}

		/// <summary>Checks if a space (left) encapsulates another space (right).</summary>
		/// <param name="left_min">The minimum values of the left space.</param>
		/// <param name="left_max">The maximum values of the left space.</param>
		/// <param name="right_min">The minimum values of the right space.</param>
		/// <param name="right_max">The maximum values of the right space.</param>
		/// <returns>True if the left space encapsulates the right; False if not.</returns>
		private bool EncapsulationCheck(Get<M> left_min, Get<M> left_max, Get<M> right_min, Get<M> right_max)
		{
			// if all the dimensions of the left space are 
			// beyond that of the right, the right is encapsulated
			for (int j = 0; j < this._dimensions; j++)
				if (this._compare(left_min(j), right_min(j)) != Comparison.Less ||
					this._compare(left_max(j), right_max(j)) != Comparison.Greater)
					return false;
			return true;
		}

		/// <summary>Chops (removes) a child from a branch.</summary>
		/// <param name="branch">The parent of the child to chop.</param>
		/// <param name="child">The index of the child to chop.</param>
		private void ChopChild(Branch branch, int child)
		{
			if (branch.Head.Value.Index == child)
				branch.Head = branch.Head.Next;
			else
			{
				Branch.Node list = branch.Head;
				while (list.Next != null)
				{
					if (list.Next.Value.Index == child)
					{
						list.Next = list.Next.Next;
						break;
					}
					list = list.Next;
				}
			}
		}

		/// <summary>Iterates through the entire tree and ensures each item is in the proper leaf.</summary>
		public void Update()
		{
			this.Update(this._top, 0);

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Recursive version of the Update method.</summary>
		/// <param name="node">The current node of iteration.</param>
		/// <param name="depth">The current depth of iteration.</param>
		private int Update(Node node, int depth)
		{
			int removals = 0;

			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				while (leaf.Head != null)
				{
					Get<M> ms = this._locate(leaf.Head.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
					{
						T item = leaf.Head.Value;
						leaf.Head = leaf.Head.Next;
						Node temp = leaf.Parent;
						while (temp != null)
						{
							if (EncapsulationCheck(temp.Min, temp.Max, ms))
								break;
							removals++;
							temp = temp.Parent;
						}

						if (temp == null)
						{
							this._count--;
							#region error
#if no_error_checking
							// nothing
#else
							throw new Error("a node was updated to be out of bounds (found in an update)");
#endif
							#endregion
						}
						else
							this.Add(item, temp, ms, depth);
					}
					else
						break;

					leaf.Count -= removals;
				}
				if (leaf.Head != null)
				{
					Leaf.Node list = leaf.Head;
					while (list.Next != null)
					{
						Get<M> ms = this._locate(list.Next.Value);

						#region error
#if no_error_checking
						// nothing
#else
						if (ms == null)
							throw new Error("the location function for omnitree is invalid.");
#endif
						#endregion

						if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
						{
							T item = list.Next.Value;
							list.Next = list.Next.Next;
							Node temp = leaf.Parent;
							while (temp != null)
							{
								if (EncapsulationCheck(temp.Min, temp.Max, ms))
									break;
								temp.Count--;
								temp = temp.Parent;
							}

							if (temp == null)
							{
								this._count--;

								#region error
#if no_error_checking
								// nothing
#else
								throw new Error("a node was updated to be out of bounds (found in an update)");
#endif
								#endregion
							}
							else
								this.Add(item, temp, ms, depth);
						}
					}
				}
			}
			else
			{
				Branch branch = node as Branch;
				Branch.Node list = branch.Head;
				while (list != null)
				{
					removals += this.Update(list.Value, depth + 1);
					if (list.Value.Count == 0)
						ChopChild(branch, list.Value.Index);
				}

				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);
			}

			return removals;
		}

		/// <summary>Iterates through the provided dimensions and ensures each item is in the proper leaf.</summary>
		/// <param name="min">The minimum dimensions of the space to update.</param>
		/// <param name="max">The maximum dimensions of the space to update.</param>
		public void Update(Get<M> min, Get<M> max)
		{
			this.Update(min, max, this._top, 0);

			this._previousAddition = this._top;
			this._previousAdditionDepth = 0;
		}

		/// <summary>Recursive version of the Update method.</summary>
		/// <param name="min">The minimum dimensions of the space to update.</param>
		/// <param name="max">The maximum dimensions of the space to update.</param>
		/// <param name="node">The current node of iteration.</param>
		/// <param name="depth">The current depth in the tree.</param>
		private int Update(Get<M> min, Get<M> max, Node node, int depth)
		{
			int removals = 0;

			if (node is Leaf)
			{
				Leaf leaf = node as Leaf;
				while (leaf.Head != null)
				{
					Get<M> ms = this._locate(leaf.Head.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
					{
						T item = leaf.Head.Value;
						leaf.Head = leaf.Head.Next;
						Node temp = leaf.Parent;
						while (temp != null)
						{
							if (EncapsulationCheck(temp.Min, temp.Max, ms))
								break;
							removals++;
							temp = temp.Parent;
						}

						if (temp == null)
						{
							this._count--;

							#region error
#if no_error_checking
							// nothing
#else
							throw new Error("a node was updated to be out of bounds (found in an update)");
#endif
							#endregion
						}
						else
							this.Add(item, temp, ms, depth);
					}
					else
						break;

					leaf.Count -= removals;
				}
				if (leaf.Head != null)
				{
					Leaf.Node list = leaf.Head;
					while (list.Next != null)
					{
						Get<M> ms = this._locate(list.Next.Value);

						#region error
#if no_error_checking
						// nothing
#else
						if (ms == null)
							throw new Error("the location function for omnitree is invalid.");
#endif
						#endregion

						if (!EncapsulationCheck(leaf.Min, leaf.Max, ms))
						{
							T item = list.Next.Value;
							list.Next = list.Next.Next;
							Node temp = leaf.Parent;
							while (temp != null)
							{
								if (EncapsulationCheck(temp.Min, temp.Max, ms))
									break;
								temp.Count--;
								temp = temp.Parent;
							}

							if (temp == null)
							{
								this._count--;

								#region error
#if no_error_checking
								// nothing
#else
								throw new Error("a node was updated to be out of bounds (found in an update)");
#endif
								#endregion
							}
							else
								this.Add(item, temp, ms, depth);
						}
					}
				}
			}
			else
			{
				Branch branch = node as Branch;
				Branch.Node list = branch.Head;
				while (list != null)
				{
					if (this.InclusionCheck(list.Value.Min, list.Value.Max, min, max))
					{
						removals += this.Update(min, max, list.Value, depth + 1);
						if (list.Value.Count == 0)
							ChopChild(branch, list.Value.Index);
					}
				}

				branch.Count -= removals;

				if (branch.Count < this._load && branch.Count != 0)
					ShrinkChild(branch.Parent, branch.Index);
			}

			return removals;
		}

		/// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
		/// <param name="function">The delegate to perform on every item in the tree.</param>
		public void Stepper(Step<T> function)
		{
			this.Stepper(function, this._top);
		}
		private void Stepper(Step<T> function, Node node)
		{
			if (node is Leaf)
			{
				Leaf.Node list = (node as Leaf).Head;
				while (list != null)
				{
					function(list.Value);
					list = list.Next;
				}
			}
			else
			{
				Branch.Node list = (node as Branch).Head;
				for (; list != null; list = list.Next)
					this.Stepper(function, list.Value);
			}
		}

		/// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
		/// <param name="function">The delegate to perform on every item in the tree.</param>
		public void Stepper(StepRef<T> function)
		{
			Stepper(function, this._top);
		}
		private void Stepper(StepRef<T> function, Node node)
		{
			if (node is Leaf)
			{
				Leaf.Node list = (node as Leaf).Head;
				for (; list != null; list = list.Next)
				{
					T temp = list.Value;
					function(ref temp);
					list.Value = temp;
				}
			}
			else
			{
				Branch.Node list = (node as Branch).Head;
				for (; list != null; list = list.Next)
					this.Stepper(function, list.Value);
			}
		}

		/// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
		/// <param name="function">The delegate to perform on every item in the tree.</param>
		public StepStatus Stepper(StepBreak<T> function)
		{
			return Stepper(function, this._top);
		}
		private StepStatus Stepper(StepBreak<T> function, Node node)
		{
			if (node is Leaf)
			{
				Leaf.Node list = (node as Leaf).Head;
				for (; list != null; list = list.Next)
					if (function(list.Value) == StepStatus.Break)
						return StepStatus.Break;
			}
			else
			{
				Branch.Node list = (node as Branch).Head;
				for (; list != null; list = list.Next)
				{
					if (this.Stepper(function, list.Value) == StepStatus.Break)
						return StepStatus.Break;
				}
			}
			return StepStatus.Continue;
		}

		/// <summary>Traverses every item in the tree and performs the delegate in them.</summary>
		/// <param name="function">The delegate to perform on every item in the tree.</param>
		public StepStatus Stepper(StepRefBreak<T> function)
		{
			return Stepper(function, this._top);
		}
		private StepStatus Stepper(StepRefBreak<T> function, Node node)
		{
			if (node is Leaf)
			{
				Leaf.Node list = (node as Leaf).Head;
				for (; list != null; list = list.Next)
				{
					T temp = list.Value;
					StepStatus status = function(ref temp);
					list.Value = temp;
					if (status == StepStatus.Break)
						return StepStatus.Break;
				}
			}
			else
			{
				Branch.Node list = (node as Branch).Head;
				for (; list != null; list = list.Next)
				{
					if (this.Stepper(function, list.Value) == StepStatus.Break)
						return StepStatus.Break;
				}
			}
			return StepStatus.Continue;
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public void Stepper(Step<T> function, Get<M> min, Get<M> max)
		{
			Stepper(function, _top, min, max);
		}
		private void Stepper(Step<T> function, Node node, Get<M> min, Get<M> max)
		{
			if (node is Leaf)
			{
				Leaf.Node list = (node as Leaf).Head;
				for (; list != null; list = list.Next)
				{
					Get<M> ms = this._locate(list.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (EncapsulationCheck(min, max, ms))
						function(list.Value);
				}
			}
			else
			{
				Branch.Node list = (node as Branch).Head;
				for (; list != null; list = list.Next)
					if (InclusionCheck(list.Value.Min, list.Value.Max, min, max))
						this.Stepper(function, list.Value, min, max);
			}
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public void Stepper(StepRef<T> function, Get<M> min, Get<M> max)
		{
			Stepper(function, _top, min, max);
		}
		private void Stepper(StepRef<T> function, Node node, Get<M> min, Get<M> max)
		{
			if (node is Leaf)
			{
				Leaf.Node list = (node as Leaf).Head;
				for (; list != null; list = list.Next)
				{
					Get<M> ms = this._locate(list.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (EncapsulationCheck(min, max, ms))
						function(ref list._value);
				}
			}
			else
			{
				Branch.Node list = (node as Branch).Head;
				while (list != null)
				{
					if (InclusionCheck(list.Value.Min, list.Value.Max, min, max))
						this.Stepper(function, list.Value, min, max);

					list = list.Next;
				}
			}
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public StepStatus Stepper(StepBreak<T> function, Get<M> min, Get<M> max)
		{
			return Stepper(function, _top, min, max);
		}
		private StepStatus Stepper(StepBreak<T> function, Node node, Get<M> min, Get<M> max)
		{
			if (node is Leaf)
			{
				Leaf.Node list = (node as Leaf).Head;
				for (; list != null; list = list.Next)
				{
					Get<M> ms = this._locate(list.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (EncapsulationCheck(min, max, ms))
						if (function(list.Value) == StepStatus.Break)
							return StepStatus.Break;
				}
			}
			else
			{
				for (Branch.Node list = (node as Branch).Head; list != null; list = list.Next)
					if (InclusionCheck(list.Value.Min, list.Value.Max, min, max))
						if (this.Stepper(function, list.Value, min, max) == StepStatus.Break)
							return StepStatus.Break;
			}
			return StepStatus.Continue;
		}

		/// <summary>Performs and specialized traversal of the structure and performs a delegate on every node within the provided dimensions.</summary>
		/// <param name="function">The delegate to perform on all items in the tree within the given bounds.</param>
		/// <param name="min">The minimum dimensions of the traversal.</param>
		/// <param name="max">The maximum dimensions of the traversal.</param>
		public StepStatus Stepper(StepRefBreak<T> function, Get<M> min, Get<M> max)
		{
			return Stepper(function, _top, min, max);
		}
		private StepStatus Stepper(StepRefBreak<T> function, Node node, Get<M> min, Get<M> max)
		{
			if (node is Leaf)
			{
				for (Leaf.Node list = (node as Leaf).Head; list != null; list = list.Next)
				{
					Get<M> ms = this._locate(list.Value);

					#region error
#if no_error_checking
					// nothing
#else
					if (ms == null)
						throw new Error("the location function for omnitree is invalid.");
#endif
					#endregion

					if (EncapsulationCheck(min, max, ms))
						if (function(ref list._value) == StepStatus.Break)
							return StepStatus.Break;
				}
			}
			else
			{
				for (Branch.Node list = (node as Branch).Head; list != null; list = list.Next)
					if (InclusionCheck(list.Value.Min, list.Value.Max, min, max))
						if (this.Stepper(function, list.Value, min, max) == StepStatus.Break)
							return StepStatus.Break;
			}
			return StepStatus.Continue;
		}

		/// <summary>Puts all the items on this tree into an array.</summary>
		/// <returns>The array containing all the items within the tree.</returns>
		public T[] ToArray()
		{
			int index = 0;
			T[] array = new T[this._count];
			this.Stepper((T entry) => { array[index++] = entry; });
			return array;
		}

		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public Omnitree_LinkedLinkedLists<T, M> Clone()
		{
			// OPTIMIZATION NEEDED
			Omnitree_LinkedLinkedLists<T, M> clone = new Omnitree_LinkedLinkedLists<T, M>(
				this._dimensions,
				this._top.Min, this._top.Max,
				this._locate,
				this._compare,
				this._average);
			this.Stepper((T current) => { clone.Add(current); });
			return clone;
		}

		/// <summary>Returns the tree to an empty state.</summary>
		public void Clear()
		{
			this._top = new Leaf(this._top.Min, this._top.Max, null, -1);
			this._count = 0;

			this._load = _defaultLoad;
			this._loadPlusOnePowered =
				Omnitree_LinkedLinkedLists<T, M>.Int_Power(this._load + 1, this._dimensions);
			this._loadPowered =
				Omnitree_LinkedLinkedLists<T, M>.Int_Power(_load, _dimensions);
		}

		#endregion

		#region IEnumerator<T>

		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			T[] array = this.ToArray();
			return array.GetEnumerator();
		}

		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			T[] array = this.ToArray();
			return ((System.Collections.Generic.IEnumerable<T>)array).GetEnumerator();
		}

		#endregion
	}
}
