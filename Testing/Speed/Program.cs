﻿using Seven;
using Seven.Mathematics;
using Seven.Structures;
using Seven.Algorithms;
using Seven.Diagnostics;

using System;
using System.Collections.Generic;
//using System.Diagnostics;
using System.Linq;
using System.Threading;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;

namespace Speed
{
	public class IntegerClass
	{
		public int _int;

		public IntegerClass(int integer)
		{
			this._int = integer;
		}

		public static bool operator <(IntegerClass a, IntegerClass b)
		{
			return a._int < b._int;
		}

		public static bool operator >(IntegerClass a, IntegerClass b)
		{
			return a._int > b._int;
		}

		public static bool operator ==(IntegerClass a, IntegerClass b)
		{
			if (object.ReferenceEquals(a, null))
				if (object.ReferenceEquals(b, null))
					return true;
				else
					return false;
			if (object.ReferenceEquals(b, null))
				return false;
			return a._int == b._int;
		}

		public static bool operator !=(IntegerClass a, IntegerClass b)
		{
			if (object.ReferenceEquals(a, null))
				if (object.ReferenceEquals(b, null))
					return false;
				else
					return true;
			if (object.ReferenceEquals(b, null))
				return true;
			return a._int != b._int;
		}

		public override bool Equals(object obj)
		{
			if (obj is IntegerClass)
				return this == (obj as IntegerClass);
			return base.Equals(obj);
		}

		public static implicit operator IntegerClass(int integer)
		{
			return new IntegerClass(integer);
		}

		public override int GetHashCode()
		{
			return this._int % 10;
			//return base.GetHashCode();
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			int iterationsperrandom = 3;
			Action<Random> testrandom = (Random random) =>
				{
					for (int i = 0; i < iterationsperrandom; i++)
						Console.WriteLine(i + ": " + random.Next());
					Console.WriteLine();
				};
			Arbitrary mcg_2pow59_13pow13 = Arbitrary.MultiplicativeCongruentGenerator_Modulus2power59_Multiplier13power13();
			Console.WriteLine("mcg_2pow59_13pow13 randoms:");
			testrandom(mcg_2pow59_13pow13);
			Arbitrary mcg_2pow31m1_1132489760 = Arbitrary.MultiplicativeCongruentGenerator_Modulus2power31minus1_Multiplier1132489760();
			Console.WriteLine("mcg_2pow31m1_1132489760 randoms:");
			testrandom(mcg_2pow31m1_1132489760);
			Arbitrary mersenneTwister = Arbitrary.MersenneTwister();
			Console.WriteLine("mersenneTwister randoms:");
			testrandom(mersenneTwister);
			Arbitrary cmr32_c2_o3 = Arbitrary.CombinedMultipleRecursiveGenerator32bit_components2_order3();
			Console.WriteLine("mersenneTwister randoms:");
			testrandom(cmr32_c2_o3);
			Arbitrary wh1982cmcg = Arbitrary.WichmannHills1982_CombinedMultiplicativeCongruentialGenerator();
			Console.WriteLine("mersenneTwister randoms:");
			testrandom(wh1982cmcg);
			Arbitrary wh2006cmcg = Arbitrary.WichmannHills2006_CombinedMultiplicativeCongruentialGenerator();
			Console.WriteLine("mersenneTwister randoms:");
			testrandom(wh2006cmcg);
			Arbitrary mwcxorsg = Arbitrary.MultiplyWithCarryXorshiftGenerator();
			Console.WriteLine("mwcxorsg randoms:");
			testrandom(mwcxorsg);

			#region Set Tests
			//{
			//	int iterations = int.MaxValue / 1000;

			//	HashSet<int> validation = new HashSet<int>();
			//	//for (int i = 0; i < interations; i++)
			//	//	validation.Add(i);

			//	{
			//		HashSet<int> set0 = new HashSet<int>();
			//		SetHashList<int> set1 = new SetHashList<int>();
			//		SetHashArray<int> set2 = new SetHashArray<int>();

			//		for (int i = 0; i < iterations; i++) set0.Add(i);
			//		for (int i = 0; i < iterations; i++) set1.Add(i);
			//		for (int i = 0; i < iterations; i++) set2.Add(i);
			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		foreach (int i in set0) { validation.Remove(i); }
			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		set1.Stepper((int i) => { validation.Remove(i); });
			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		set2.Stepper((int i) => { validation.Remove(i); });
			//		for (int i = 0; i < iterations; i++) set0.Contains(i);
			//		for (int i = 0; i < iterations; i++) set1.Contains(i);
			//		for (int i = 0; i < iterations; i++) set2.Contains(i);
			//		for (int i = 0; i < iterations; i++) set0.Remove(i);
			//		for (int i = 0; i < iterations; i++) set1.Remove(i);
			//		for (int i = 0; i < iterations; i++) set2.Remove(i);

			//		Console.WriteLine("Adding HashSet:               " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set0.Add(i); }));
			//		Console.WriteLine("Adding Set_HashLinkedList:    " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set1.Add(i); }));
			//		Console.WriteLine("Adding SetHash:               " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set2.Add(i); }));

			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		foreach (int i in set0) { validation.Remove(i); }
			//		Console.WriteLine("Validate HashSet:             " + (validation.Count == 0));

			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		set1.Stepper((int i) => { validation.Remove(i); });
			//		Console.WriteLine("Validate Set_HashLinkedList:  " + (validation.Count == 0));

			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		set2.Stepper((int i) => { validation.Remove(i); });
			//		Console.WriteLine("Validate SetHas:              " + (validation.Count == 0));

			//		Console.WriteLine("Size HashSet:                 " + (typeof(HashSet<int>).GetField("m_buckets", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(set0) as int[]).Length);
			//		Console.WriteLine("Size Set_HashLinkedList:      " + set1.TableSize);
			//		Console.WriteLine("Size SetHash:                 " + set2.TableSize);

			//		Console.WriteLine("Constains HashSet:            " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set0.Contains(i); }));
			//		Console.WriteLine("Constains Set_HashLinkedList: " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set1.Contains(i); }));
			//		Console.WriteLine("Constains SetHash:            " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set2.Contains(i); }));

			//		//Console.WriteLine("Removed HashSet:              " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set0.Remove(i); }));
			//		//Console.WriteLine("Removed Set_HashLinkedList:   " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set1.Remove(i); }));
			//		//Console.WriteLine("Remove SetHash:               " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) set2.Remove(i); }));

			//		Console.WriteLine("Removed HashSet:              " + Seven.Diagnostics.Performance.Time(() => { for (int i = iterations - 1; i >= 0; i--) set0.Remove(i); }));
			//		Console.WriteLine("Removed Set_HashLinkedList:   " + Seven.Diagnostics.Performance.Time(() => { for (int i = iterations - 1; i >= 0; i--) set1.Remove(i); }));
			//		Console.WriteLine("Remove SetHash:               " + Seven.Diagnostics.Performance.Time(() => { for (int i = iterations - 1; i >= 0; i--) set2.Remove(i); }));

			//		Console.WriteLine("Size HashSet:                 " + (typeof(HashSet<int>).GetField("m_buckets", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(set0) as int[]).Length);
			//		Console.WriteLine("Size Set_HashLinkedList:      " + set1.TableSize);
			//		Console.WriteLine("Size SetHash:                 " + set2.TableSize);
			//	}

			//	Console.WriteLine();
			//}
			#endregion

			#region Map/Dictionary
			//{
			//	int iterations = int.MaxValue / 10000;

			//	HashSet<int> validation = new HashSet<int>();
			//	//for (int i = 0; i < interations; i++)
			//	//	validation.Add(i);

			//	{
			//		Dictionary<int, int> map0 = new Dictionary<int, int>();
			//		//MapSetHashList<int, int> map1 = new MapSetHashList<int, int>();
			//		MapHashLinked<int, int> map2 = new MapHashLinked<int, int>();
			//		MapHashArray<int, int> map3 = new MapHashArray<int, int>();


			//		Console.WriteLine("Adding 0:    " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) map0.Add(i, i); }));
			//		//Console.WriteLine("Adding 1:    " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) map1.Add(i, i); }));
			//		Console.WriteLine("Adding 2:    " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) map2.Add(i, i); }));
			//		Console.WriteLine("Adding 3:    " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) map3.Add(i, i); }));

			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		foreach (KeyValuePair<int, int> i in map0) { validation.Remove(i.Key); }
			//		Console.WriteLine("Validate 0:  " + (validation.Count == 0));

			//		//for (int i = 0; i < iterations; i++)
			//		//	validation.Add(i);
			//		////foreach (int i in map1) { validation.Remove(i); }
			//		//map1.Stepper((int i) => { validation.Remove(i); });
			//		//Console.WriteLine("Validate 1:  " + (validation.Count == 0));

			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		//foreach (int i in map1) { validation.Remove(i); }
			//		map2.Stepper((int i) => { validation.Remove(i); });
			//		Console.WriteLine("Validate 2:  " + (validation.Count == 0));

			//		for (int i = 0; i < iterations; i++)
			//			validation.Add(i);
			//		//foreach (int i in map1) { validation.Remove(i); }
			//		map3.Stepper((int i) => { validation.Remove(i); });
			//		Console.WriteLine("Validate 3:  " + (validation.Count == 0));

			//		int temp;
			//		Console.WriteLine("Get 0:       " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) temp = map0[i]; }));
			//		//Console.WriteLine("Get 1:       " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) temp = map1[i]; }));
			//		Console.WriteLine("Get 2:       " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) temp = map2[i]; }));
			//		Console.WriteLine("Get 3:       " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) temp = map3[i]; }));

			//		Console.WriteLine("Removed 0:   " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) map0.Remove(i); }));
			//		//Console.WriteLine("Removed 1:   " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) map1.Remove(i); }));
			//		Console.WriteLine("Removed 2:   " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) map2.Remove(i); }));
			//		Console.WriteLine("Removed 3:   " + Seven.Diagnostics.Performance.Time(() => { for (int i = 0; i < iterations; i++) map3.Remove(i); }));
			//	}
			//}
			#endregion

			#region Vector Test

			//Console.WriteLine();
			//Console.WriteLine("Vector Testing-------------------------------------");

			//Random random = new Random();
			//const int vector_size = 4;
			//const int vector_iterations = int.MaxValue / 100;

			//Vector<double> vector_a = new Vector<double>(vector_size);
			//Vector<double> vector_b = new Vector<double>(vector_size);
			//Vector<double> vector_c;

			//for (int i = 0; i < vector_size; i++)
			//{
			//	vector_a[i] = random.Next();
			//	vector_b[i] = random.Next();
			//}

			//Console.WriteLine("Compile 1: " + Seven.Diagnostics.Performance.Time(() => { vector_c = Vector<double>.Vector_Add(vector_a, vector_b); }));
			//Console.WriteLine("Compile 2: " + Seven.Diagnostics.Performance.Time(() => { vector_c = Vector<double>.Vector_Add2(vector_a, vector_b); }));
			//Console.WriteLine("Compile 3: " + Seven.Diagnostics.Performance.Time(() => { vector_c = Vector<double>.Vector_Add3(vector_a, vector_b); }));
			//Console.WriteLine("Compile 4: " + Seven.Diagnostics.Performance.Time(() => { vector_c = Vector<double>.Vector_Add4(vector_a, vector_b); }));

			//Console.WriteLine("Test 1:    " + Seven.Diagnostics.Performance.Time(() => {
			//	for (int i = 0; i < vector_iterations; i++)
			//		vector_c = Vector<double>.Vector_Add(vector_a, vector_b);
			//}));

			//Console.WriteLine("Test 2:    " + Seven.Diagnostics.Performance.Time(() =>
			//{
			//	for (int i = 0; i < vector_iterations; i++)
			//		vector_c = Vector<double>.Vector_Add2(vector_a, vector_b);
			//}));

			//Console.WriteLine("Test 3:    " + Seven.Diagnostics.Performance.Time(() =>
			//{
			//	for (int i = 0; i < vector_iterations; i++)
			//		vector_c = Vector<double>.Vector_Add3(vector_a, vector_b);
			//}));

			//Console.WriteLine("Test 4:    " + Seven.Diagnostics.Performance.Time(() =>
			//{
			//	for (int i = 0; i < vector_iterations; i++)
			//		vector_c = Vector<double>.Vector_Add4(vector_a, vector_b);
			//}));

			#endregion

			#region Sorting Speed
			//{
			//	int size = int.MaxValue / 1000000;
			//	int[] dataSet = new int[size];
			//	for (int i = 0; i < size; i++)
			//		dataSet[i] = i;

			//	Console.WriteLine("Sorting Algorithms----------------------");
			//	Console.WriteLine();

			//	//Sort<int>.Shuffle(dataSet);
			//	//Console.Write("Bubble:      " + Seven.Diagnostics.Performance.Time(() => { Sort<int>.Bubble(dataSet); }));
			//	Sort<int>.Shuffle(dataSet);
			//	Console.WriteLine("Selection:   " + Seven.Diagnostics.Performance.Time(() => { Sort<int>.Selection(dataSet); }));
			//	Sort<int>.Shuffle(dataSet);
			//	Console.WriteLine("Insertion:   " + Seven.Diagnostics.Performance.Time(() => { Sort<int>.Insertion(dataSet); }));
			//	Sort<int>.Shuffle(dataSet);
			//	Console.WriteLine("Quick:       " + Seven.Diagnostics.Performance.Time(() => { Sort<int>.Quick(dataSet); }));
			//	Sort<int>.Shuffle(dataSet);
			//	Console.WriteLine("Merge:       " + Seven.Diagnostics.Performance.Time(() => { Sort<int>.Merge(dataSet); }));
			//	Sort<int>.Shuffle(dataSet);
			//	Console.WriteLine("Heap:        " + Seven.Diagnostics.Performance.Time(() => { Sort<int>.Heap(dataSet); }));
			//	Sort<int>.Shuffle(dataSet);
			//	Console.WriteLine("OddEven:     " + Seven.Diagnostics.Performance.Time(() => { Sort<int>.OddEven(dataSet); }));

			//	Sort<int>.Shuffle(dataSet);
			//	Console.WriteLine("IEnumerable: " + Seven.Diagnostics.Performance.Time(() => { dataSet.OrderBy(item => item); }));
			//	Sort<int>.Shuffle(dataSet);
			//	Console.WriteLine("Array.Sort:  " + Seven.Diagnostics.Performance.Time(() => { Array.Sort(dataSet); }));
			//}
			#endregion

			//Random random = new Random();
			//const int matrix_rows = 4;
			//const int matrix_columns = 4;
			//const int matrix_iterations = int.MaxValue / 100;

			//{

			//	Seven.Mathematics.Matrix<double> matrix_a = new Seven.Mathematics.Matrix<double>(matrix_rows, matrix_columns);
			//	Seven.Mathematics.Matrix<double> matrix_b = new Seven.Mathematics.Matrix<double>(matrix_rows, matrix_columns);
			//	Seven.Mathematics.Matrix<double> matrix_c;

			//	matrix_c = matrix_a + matrix_b;
			//	matrix_c = matrix_b + matrix_a;
			//	//matrix_a = matrix_b + matrix_c;
			//	//matrix_a = matrix_c + matrix_b;

			//	for (int i = 0; i < matrix_rows; i++)
			//		for (int j = 0; j < matrix_columns; j++)
			//		{
			//			matrix_a[i, j] = random.Next();
			//			matrix_b[i, j] = random.Next();
			//		}

			//	Console.WriteLine("Test 1:    " + Seven.Diagnostics.Performance.Time(() =>
			//	{
			//		for (int i = 0; i < matrix_iterations; i++)
			//			matrix_c = matrix_a + matrix_b;
			//	}));

			//matrix_c = Matrix<double>.Matrix_Negate2(matrix_a);
			//Console.WriteLine("Test 2:    " + Seven.Diagnostics.Performance.Time(() =>
			//{
			//	for (int i = 0; i < matrix_iterations; i++)
			//		matrix_c = Matrix<double>.Matrix_Negate2(matrix_a);
			//}));

			//Console.WriteLine("Test 2:    " + Seven.Diagnostics.Performance.Time(() =>
			//{
			//	for (int i = 0; i < matrix_iterations; i++)
			//		Matrix<double>.Matrix_IsSymetric2(matrix_a);
			//}));

			//Console.WriteLine("Compile 1: " + Seven.Diagnostics.Performance.Time(() => { matrix_c = matrix_a + matrix_b; }));

			//Console.WriteLine("Test 1:    " + Seven.Diagnostics.Performance.Time(() =>
			//{
			//	for (int i = 0; i < matrix_iterations; i++)
			//		matrix_c = matrix_a + matrix_b;
			//}));

			//}

			Console.WriteLine();
			Console.WriteLine("Done...");
			Console.ReadLine();
		}
	}
}
