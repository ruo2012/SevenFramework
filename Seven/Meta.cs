﻿// Seven
// https://github.com/53V3N1X/SevenFramework
// LISCENSE: See "LISCENSE.md" in th root project directory.
// SUPPORT: See "SUPPORT.md" in the root project directory.

using Seven.Structures;
using System;
using System.Linq.Expressions;

namespace Seven
{
	/// <summary>Generates code and executabel code at runtime.</summary>
	public static class Meta
	{
		/// <summary>Generates a generic object at runtime (typically delegates).</summary>
		/// <typeparam name="T">The type of the generic object to create.</typeparam>
		/// <param name="code">The object to generate.</param>
		/// <param name="method">The name of the method to generate the object in.</param>
		/// <param name="_class">The name of the class to generate the object in.</param>
		/// <param name="_namespace">The name of the namespace to generate the object in.</param>
		/// <param name="name_spaces">The required namespaces.</param>
		/// <param name="_unsafe">Is unsafe code allowed?</param>
		/// <param name="references">The required assembly references.</param>
		/// <returns>The generated object.</returns>
		internal static T Compile<T>(
			string code, string method, string _class, string _namespace, bool _unsafe, string[] name_spaces, string[] references)
		{
			if (code == null)
				throw new System.ArgumentNullException("code");
			if (method == null)
				throw new System.ArgumentNullException("method");
			if (_class == null)
				throw new System.ArgumentNullException("_class");
			if (_namespace == null)
				throw new System.ArgumentNullException("_namespace");

			string full_code = string.Empty;

			if (name_spaces != null)
				for (int i = 0; i < name_spaces.Length; i++)
					full_code += "using " + name_spaces[i] + ";";

			full_code += "namespace " + _namespace + " {";
			full_code += "public class " + _class + " {";
			full_code += "public static object " + method + "() { return " + code + "; } } }";

			System.CodeDom.Compiler.CompilerParameters parameters =
					new System.CodeDom.Compiler.CompilerParameters();

			if (references != null)
				foreach (string reference in references)
					parameters.ReferencedAssemblies.Add(reference);

			parameters.GenerateInMemory = true;

			if (_unsafe)
				parameters.CompilerOptions = "/optimize /unsafe";

			System.CodeDom.Compiler.CompilerResults results =
					new Microsoft.CSharp.CSharpCodeProvider().CompileAssemblyFromSource(parameters, full_code);

			if (results.Errors.HasErrors)
			{
				string error = string.Empty;

				foreach (System.CodeDom.Compiler.CompilerError compiler_error in results.Errors)
					error += compiler_error.ErrorText.ToString() + "\n";

				throw new System.FormatException(error);
			}

			System.Reflection.MethodInfo generate =
					results.CompiledAssembly.GetType(_namespace + "." + _class).GetMethod(method);

			return (T)generate.Invoke(null, null);
		}

		/// <summary>Generates a generic object at runtime (typically delegates).</summary>
		/// <typeparam name="T">The type of the generic object to create.</typeparam>
		/// <param name="code">The object to generate.</param>
		/// <returns>The generated object.</returns>
		internal static T Compile<T>(string code)
		{
			if (code == null)
				throw new System.ArgumentNullException("code == null");

			string type_string = Meta.ConvertTypeToCsharpSource(typeof(T));

			string full_code =
				string.Concat(
@"using Seven;
using Seven.Structures;
using Seven.Mathematics;
namespace Seven.Generated
{
	public class Generator
	{
		public static object Generate()
		{
			return (", type_string, ")(", code, @");
		}
	}
}");

			System.CodeDom.Compiler.CompilerParameters parameters =
					new System.CodeDom.Compiler.CompilerParameters();

			parameters.ReferencedAssemblies.Add("Seven.dll");

			parameters.GenerateInMemory = true;

			parameters.CompilerOptions = "/optimize";

#if unsafe_code
				parameters.CompilerOptions = "/unsafe";
#endif

			System.CodeDom.Compiler.CompilerResults results =
					new Microsoft.CSharp.CSharpCodeProvider().CompileAssemblyFromSource(parameters, full_code);

			if (results.Errors.HasErrors)
			{
				string error = "Generation Error\nType: " + type_string + "\nObject: " + code + "\n\nCompilation Error:";

				Exception[] exceptions = new Exception[results.Errors.Count];
				foreach (System.CodeDom.Compiler.CompilerError compiler_error in results.Errors)
					error += compiler_error.ErrorText.ToString() + "\n";

				throw new System.FormatException(error);
			}

			System.Reflection.MethodInfo generate =
					results.CompiledAssembly.GetType("Seven.Generated.Generator").GetMethod("Generate");

			return (T)generate.Invoke(null, null);
		}

		/// <summary>Converts a "System.Type" into a string as it would appear in C# source code.</summary>
		/// <param name="type">The "System.Type" to convert to a string.</param>
		/// <returns>The string as the "System.Type" would appear in C# source code.</returns>
		public static string ConvertTypeToCsharpSource(System.Type type)
		{
			if (type == null)
				throw new System.ArgumentNullException("type");
			// no generics
			if (!type.IsGenericType)
				return type.ToString();
			// non-nested generics
			else if (!(type.IsNested && type.DeclaringType.IsGenericType))
			{
				// non-generic initial parents
				string text = type.ToString();
				text = text.Substring(0, text.IndexOf('`')) + "<";
				text = text.Replace('+', '.');
				// generic string arguments
				System.Type[] generics = type.GetGenericArguments();
				for (int i = 0; i < generics.Length; i++)
				{
					if (i > 0)
						text += ", ";
					text += Meta.ConvertTypeToCsharpSource(generics[i]);
				}
				return text + ">";
			}
			// nested generics
			else
			{
				string text = string.Empty;
				// non-generic initial parents
				for (System.Type temp = type; temp != null; temp = temp.DeclaringType)
					if (!temp.IsGenericType)
					{
						text += temp.ToString() + '.';
						break;
					}
				// count generic parents
				int parent_count = 0;
				for (System.Type temp = type; temp != null && temp.IsGenericType; temp = temp.DeclaringType)
					parent_count++;
				// generic parents types
				System.Type[] parents = new System.Type[parent_count];
				System.Type _temp = type;
				for (int i = 0; _temp != null && _temp.IsGenericType; _temp = _temp.DeclaringType, i++)
					parents[i] = _temp;
				// count generic arguments per parent
				int[] generics_per_parent = new int[parent_count];
				for (int i = 0; i < parents.Length; i++)
					generics_per_parent[i] = parents[i].GetGenericArguments().Length;
				for (int i = parents.Length - 1, sum = 0; i > -1; i--)
				{
					generics_per_parent[i] -= sum;
					sum += generics_per_parent[i];
				}
				// generic string arguments
				System.Type[] generic_types = type.GetGenericArguments();
				string[] types_strings = new string[generic_types.Length];
				for (int i = 0; i < generic_types.Length; i++)
					types_strings[i] = ConvertTypeToCsharpSource(generic_types[i]);
				// combine types and generic arguments into result
				for (int i = parents.Length - 1, k = 0; i > -1; i--)
				{
					if (generics_per_parent[i] == 0)
					{
						text += parents[i].Name;
					}
					else
					{
						string current_type = parents[i].Name.Substring(0, parents[i].Name.IndexOf('`')) + '<';
						for (int j = 0; j < generics_per_parent[i]; j++)
						{
							current_type += types_strings[k++];
							if (j + 1 != generics_per_parent[i])
								current_type += ',';
						}
						current_type += ">";
						text += current_type;
					}
					if (i > 0)
						text += '.';
				}
				return text;
			}
		}

		/// <summary>Takes a C# source code string representation fo a type and converts it to its runtime string name.</summary>
		/// <param name="name">The name to format.</param>
		/// <returns>The type name formatted as it exists during runtime.</returns>
		internal static string FormatTypeNameFromCsharpSource(string name)
		{
			throw new System.NotImplementedException();
			//name = name.Trim();
			//if (name.Contains("<") || name.Contains(">") || name.Contains(","))
			//{
			//	if (!name.Contains("<"))
			//		throw new System.FormatException();
			//	if (!name.Contains(">"))
			//		throw new System.FormatException();
			//	string[] type_split = name.Split('<', '>');
			//	if (type_split.L)
			//	for (int i = 0; i < type_split.Length; i++)
			//		type_split[i] = type_split[i].Trim();
				

			//	if (type_split.Length != 3 || string.IsNullOrWhiteSpace


			//}
			//else
			//	return name;
		}

		internal delegate Expression UnaryOperationHelperDelegate(Expression operand, LabelTarget returnLabel);

		internal static Delegate UnaryOperationHelper<Delegate, Operand, Return>(UnaryOperationHelperDelegate operation)
		{
			// shared expressions
			ParameterExpression _operand = Expression.Parameter(typeof(Operand));
			LabelTarget _label = Expression.Label(typeof(Return));
			// code builder
			ListLinked<Expression> expressions = new ListLinked<Expression>();
			// null checks
			if (!typeof(Operand).IsValueType) // is nullable?
			{
				expressions.Add(
					Expression.IfThen(
						Expression.Equal(_operand, Expression.Constant(null, typeof(Operand))),
						Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("operand")))));
			}
			// code
			expressions.Add(operation(_operand, _label));
			expressions.Add(Expression.Label(_label, Expression.Constant(default(Return))));
			// build
			return Expression.Lambda<Delegate>(
				Expression.Block(expressions.ToArray()),
				_operand).Compile();
		}

		internal delegate Expression BinaryOperationHelperDelegate(Expression left, Expression right, LabelTarget returnLabel);

		internal static Delegate BinaryOperationHelper<Delegate, Left, Right, Return>(BinaryOperationHelperDelegate operation)
		{
			// shared expressions
			ParameterExpression _left = Expression.Parameter(typeof(Left));
			ParameterExpression _right = Expression.Parameter(typeof(Right));
			LabelTarget _label = Expression.Label(typeof(Return));
			// code builder
			ListLinked<Expression> expressions = new ListLinked<Expression>();
			// null checks
			if (!typeof(Left).IsValueType) // is nullable?
			{
				expressions.Add(
					Expression.IfThen(
						Expression.Equal(_left, Expression.Constant(null, typeof(Left))),
						Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("left")))));
			}
			if (!typeof(Right).IsValueType) // is nullable?
			{
				expressions.Add(
					Expression.IfThen(
						Expression.Equal(_right, Expression.Constant(null, typeof(Right))),
						Expression.Throw(Expression.New(typeof(System.ArgumentNullException).GetConstructor(new System.Type[] { typeof(string) }), Expression.Constant("right")))));
			}
			// code
			expressions.Add(operation(_left, _right, _label));
			expressions.Add(Expression.Label(_label, Expression.Constant(default(Return), typeof(Return))));
			// build expression
			Expression expression = Expression.Block(expressions.ToArray());
			// compile
			return Expression.Lambda<Delegate>(expression, _left, _right).Compile();
		}

		#region Unary

		#region Convert

		public static bool ValidateConvert<T>()
		{
			return ValidateConvert<T, T>();
		}

		public static bool ValidateConvert<A, B>()
		{
			try
			{
				Expression.Lambda<Func<B>>(
					Expression.Convert(
						Expression.Default(typeof(A)), typeof(B))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Negate

		public static bool ValidateNegate<T>()
		{
			return ValidateNegate<T, T>();
		}

		public static bool ValidateNegate<A, B>()
		{
			try
			{
				Expression.Lambda<Func<B>>(
					Expression.Negate(
						Expression.Default(typeof(A)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region UnaryPlus

		public static bool ValidateUnaryPlus<T>()
		{
			return ValidateUnaryPlus<T, T>();
		}

		public static bool ValidateUnaryPlus<A, B>()
		{
			try
			{
				Expression.Lambda<Func<B>>(
					Expression.UnaryPlus(
						Expression.Default(typeof(A)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region LogicalNot

		public static bool ValidateLogicalNot<T>()
		{
			return ValidateLogicalNot<T, T>();
		}

		public static bool ValidateLogicalNot<A, B>()
		{
			try
			{
				Expression.Lambda<Func<B>>(
					Expression.Not(
						Expression.Default(typeof(A)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Increment

		public static bool ValidateIncrement<T>()
		{
			return ValidateIncrement<T, T>();
		}

		public static bool ValidateIncrement<A, B>()
		{
			try
			{
				Expression.Lambda<Func<B>>(
					Expression.Increment(
						Expression.Default(typeof(A)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Decrement

		public static bool ValidateDecrement<T>()
		{
			return ValidateDecrement<T, T>();
		}

		public static bool ValidateDecrement<A, B>()
		{
			try
			{
				Expression.Lambda<Func<B>>(
					Expression.Decrement(
						Expression.Default(typeof(A)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#endregion

		#region Binary

		#region Add

		public static bool ValidateAdd<T>()
		{
			return ValidateAdd<T, T, T>();
		}

		public static bool ValidateAdd<A, B, C>()
		{
			try
			{
				Expression.Lambda<Func<C>>(
					Expression.Add(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Subtract

		public static bool ValidateSubtract<T>()
		{
			return ValidateSubtract<T, T, T>();
		}

		public static bool ValidateSubtract<A, B, C>()
		{
			try
			{
				Expression.Lambda<Func<C>>(
					Expression.Subtract(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Multiply

		public static bool ValidateMultiply<T>()
		{
			return ValidateMultiply<T, T, T>();
		}

		public static bool ValidateMultiply<A, B, C>()
		{
			try
			{
				Expression.Lambda<Func<C>>(
					Expression.Multiply(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Divide

		public static bool ValidateDivide<T>()
		{
			return ValidateDivide<T, T, T>();
		}

		public static bool ValidateDivide<A, B, C>()
		{
			try
			{
				Expression.Lambda<Func<C>>(
					Expression.Divide(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Modulo

		public static bool ValidateModulo<T>()
		{
			return ValidateModulo<T, T, T>();
		}

		public static bool ValidateModulo<A, B, C>()
		{
			try
			{
				Expression.Lambda<Func<C>>(
					Expression.Modulo(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region Equal

		public static bool ValidateEqual<T>()
		{
			return ValidateEqual<T, T>();
		}

		public static bool ValidateEqual<A, B>()
		{
			try
			{
				Expression.Lambda<Func<bool>>(
					Expression.Equal(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region NotEqual

		public static bool ValidateNotEqual<T>()
		{
			return ValidateNotEqual<T, T>();
		}

		public static bool ValidateNotEqual<A, B>()
		{
			try
			{
				Expression.Lambda<Func<bool>>(
					Expression.NotEqual(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region GreaterThan

		public static bool ValidateGreaterThan<T>()
		{
			return ValidateGreaterThan<T, T>();
		}

		public static bool ValidateGreaterThan<A, B>()
		{
			try
			{
				Expression.Lambda<Func<bool>>(
					Expression.GreaterThan(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region LessThan

		public static bool ValidateLessThan<T>()
		{
			return ValidateLessThan<T, T>();
		}

		public static bool ValidateLessThan<A, B>()
		{
			try
			{
				Expression.Lambda<Func<bool>>(
					Expression.LessThan(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region GreaterThanOrEqual

		public static bool ValidateGreaterThanOrEqual<T>()
		{
			return ValidateGreaterThanOrEqual<T, T>();
		}

		public static bool ValidateGreaterThanOrEqual<A, B>()
		{
			try
			{
				Expression.Lambda<Func<bool>>(
					Expression.GreaterThanOrEqual(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#region LessThanOrEqual

		public static bool ValidateLessThanOrEqual<T>()
		{
			return ValidateLessThanOrEqual<T, T>();
		}

		public static bool ValidateLessThanOrEqual<A, B>()
		{
			try
			{
				Expression.Lambda<Func<bool>>(
					Expression.LessThanOrEqual(
						Expression.Default(typeof(A)),
						Expression.Default(typeof(B)))).Compile();
				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion

		#endregion
	}
}
