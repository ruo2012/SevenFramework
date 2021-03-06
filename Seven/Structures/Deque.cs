﻿// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

namespace Seven.Structures
{
	public interface Deque<T> : Structure<T>,
		// Structure Properties
		Structure.Countable<T>,
		Structure.Clearable<T>
	{

	}

	/// <summary>Implements First-In-First-Out queue data structure.</summary>
	/// <remarks>The runtimes of each public member are included in the "remarks" xml tags.</remarks>
	[System.Serializable]
	public class DequeLinked<T> : Deque<T>
	{
		// fields
		private Node _head;
		private Node _tail;
		private int _count;
		// nested types
		#region Node
		/// <summary>This class just holds the data for each individual node of the list.</summary>
		private class Node
		{
			private T _value;
			private Node _next;

			internal T Value { get { return _value; } set { _value = value; } }
			internal Node Next { get { return _next; } set { _next = value; } }

			internal Node(T data) { _value = data; }
		}
		#endregion
		// constructors
		#region public Deque_Linked()
		/// <summary>Creates an instance of a queue.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public DequeLinked()
		{
			_head = _tail = null;
			_count = 0;
		}
		#endregion
		// properties
		#region public int Count
		/// <summary>Returns the number of items in the queue.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public int Count { get { return _count; } }
		#endregion
		// methods
		#region public void EnqueueBack(T enqueue)
		/// <summary>Adds an item to the back of the queue.</summary>
		/// <param name="enqueue">The item to add to the queue.</param>
		/// <remarks>Runtime: O(1).</remarks>
		public void EnqueueBack(T enqueue)
		{
			if (_tail == null)
				_head = _tail = new Node(enqueue);
			else
				_tail = _tail.Next = new Node(enqueue);
			_count++;
		}
		#endregion
		#region public T DequeueFront()
		/// <summary>Removes the oldest item in the queue.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public T DequeueFront()
		{
			if (_head == null)
				throw new System.InvalidOperationException("Attempting to remove a non-existing id value.");
			T value = _head.Value;
			if (_head == _tail)
				_tail = null;
			_head = null;
			_count--;
			return value;
		}
		#endregion
		#region public T PeekFront()
		/// <summary>Looks at the front-most value.</summary>
		/// <returns>The front-most value.</returns>
		public T PeekFront()
		{
			if (_head == null)
				throw new System.InvalidOperationException("Attempting to remove a non-existing id value.");
			T returnValue = _head.Value;
			return returnValue;
		}
		#endregion
		#region public void Clear()
		/// <summary>Resets the queue to an empty state.</summary>
		/// <remarks>Runtime: O(1).</remarks>
		public void Clear()
		{
			_head = _tail = null;
			_count = 0;
		}
		#endregion
		#region public T[] ToArray()
		/// <summary>Converts the list into a standard array.</summary>
		/// <returns>A standard array of all the items.</returns>
		/// /// <remarks>Runtime: Theta(n).</remarks>
		public T[] ToArray()
		{
			if (_count == 0)
				return null;
			T[] array = new T[_count];
			Node looper = _head;
			for (int i = 0; i < _count; i++)
			{
				array[i] = looper.Value;
				looper = looper.Next;
			}
			return array;
		}
		#endregion
		#region System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.IEnumerator
			System.Collections.IEnumerable.GetEnumerator()
		{
			Node current = this._head;
			while (current != null)
			{
				yield return current.Value;
				current = current.Next;
			}
		}
		#endregion
		#region System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		/// <summary>FOR COMPATIBILITY ONLY. AVOID IF POSSIBLE.</summary>
		System.Collections.Generic.IEnumerator<T>
			System.Collections.Generic.IEnumerable<T>.GetEnumerator()
		{
			Node current = this._head;
			while (current != null)
			{
				yield return current.Value;
				current = current.Next;
			}
		}
		#endregion
		#region public bool Contains(T item, Compare<T> compare)
		/// <summary>Checks to see if a given object is in this data structure.</summary>
		/// <param name="item">The item to check for.</param>
		/// <param name="compare">Delegate representing comparison technique.</param>
		/// <returns>true if the item is in this structure; false if not.</returns>
		public bool Contains(T item, Compare<T> compare)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region  public bool Contains<Key>(Key key, Compare<T, Key> compare)
		/// <summary>Checks to see if a given object is in this data structure.</summary>
		/// <typeparam name="Key">The type of the key to check for.</typeparam>
		/// <param name="key">The key to check for.</param>
		/// <param name="compare">Delegate representing comparison technique.</param>
		/// <returns>true if the item is in this structure; false if not.</returns>
		public bool Contains<Key>(Key key, Compare<T, Key> compare)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public void Stepper(Step<T> function)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(Step<T> function)
		{
			Node current = this._head;
			while (current != null)
			{
				function(current.Value);
				current = current.Next;
			}
		}
		#endregion
		#region public void Stepper(StepRef<T> function)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		public void Stepper(StepRef<T> function)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public StepStatus Stepper(StepBreak<T> function)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepBreak<T> function)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public StepStatus Stepper(StepRefBreak<T> function)
		/// <summary>Invokes a delegate for each entry in the data structure.</summary>
		/// <param name="function">The delegate to invoke on each item in the structure.</param>
		/// <returns>The resulting status of the iteration.</returns>
		public StepStatus Stepper(StepRefBreak<T> function)
		{
			throw new System.NotImplementedException();
		}
		#endregion
		#region public Structure<T> Clone()
		/// <summary>Creates a shallow clone of this data structure.</summary>
		/// <returns>A shallow clone of this data structure.</returns>
		public Structure<T> Clone()
		{
			throw new System.NotImplementedException();
		}
		#endregion
	}
}
