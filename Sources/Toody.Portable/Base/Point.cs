// MIT License - Copyright (C) The Mono.Xna Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace Toody
{
	using System;
	using System.Diagnostics;
	using System.Runtime.Serialization;

	/// <summary>
	/// Describes a 2D-vector.
	/// </summary>
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Point : IEquatable<Point>
	{
		#region Private Fields

		private static readonly Point zeroVector = new Point(0f, 0f);
		private static readonly Point unitVector = new Point(1f, 1f);
		private static readonly Point unitXVector = new Point(1f, 0f);
		private static readonly Point unitYVector = new Point(0f, 1f);

		#endregion

		#region Public Fields

		/// <summary>
		/// The x coordinate of this <see cref="Point"/>.
		/// </summary>
		public float X;

		/// <summary>
		/// The y coordinate of this <see cref="Point"/>.
		/// </summary>
		public float Y;

		#endregion

		#region Properties

		/// <summary>
		/// Returns a <see cref="Point"/> with components 0, 0.
		/// </summary>
		public static Point Zero
		{
			get { return zeroVector; }
		}

		/// <summary>
		/// Returns a <see cref="Point"/> with components 1, 1.
		/// </summary>
		public static Point One
		{
			get { return unitVector; }
		}

		/// <summary>
		/// Returns a <see cref="Point"/> with components 1, 0.
		/// </summary>
		public static Point UnitX
		{
			get { return unitXVector; }
		}

		/// <summary>
		/// Returns a <see cref="Point"/> with components 0, 1.
		/// </summary>
		public static Point UnitY
		{
			get { return unitYVector; }
		}

		#endregion

		#region Internal Properties

		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(
					this.X.ToString(), "  ",
					this.Y.ToString()
				);
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a 2d vector with X and Y from two values.
		/// </summary>
		/// <param name="x">The x coordinate in 2d-space.</param>
		/// <param name="y">The y coordinate in 2d-space.</param>
		public Point(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Constructs a 2d vector with X and Y set to the same value.
		/// </summary>
		/// <param name="value">The x and y coordinates in 2d-space.</param>
		public Point(float value)
		{
			this.X = value;
			this.Y = value;
		}

		#endregion

		#region Operators

		/// <summary>
		/// Inverts values in the specified <see cref="Point"/>.
		/// </summary>
		/// <param name="value">Source <see cref="Point"/> on the right of the sub sign.</param>
		/// <returns>Result of the inversion.</returns>
		public static Point operator -(Point value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		/// <summary>
		/// Adds two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/> on the left of the add sign.</param>
		/// <param name="value2">Source <see cref="Point"/> on the right of the add sign.</param>
		/// <returns>Sum of the vectors.</returns>
		public static Point operator +(Point value1, Point value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		/// <summary>
		/// Subtracts a <see cref="Point"/> from a <see cref="Point"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/> on the left of the sub sign.</param>
		/// <param name="value2">Source <see cref="Point"/> on the right of the sub sign.</param>
		/// <returns>Result of the vector subtraction.</returns>
		public static Point operator -(Point value1, Point value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		/// <summary>
		/// Multiplies the components of two vectors by each other.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/> on the left of the mul sign.</param>
		/// <param name="value2">Source <see cref="Point"/> on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication.</returns>
		public static Point operator *(Point value1, Point value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		/// <summary>
		/// Multiplies the components of vector by a scalar.
		/// </summary>
		/// <param name="value">Source <see cref="Point"/> on the left of the mul sign.</param>
		/// <param name="scaleFactor">Scalar value on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication with a scalar.</returns>
		public static Point operator *(Point value, float scaleFactor)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		/// <summary>
		/// Multiplies the components of vector by a scalar.
		/// </summary>
		/// <param name="scaleFactor">Scalar value on the left of the mul sign.</param>
		/// <param name="value">Source <see cref="Point"/> on the right of the mul sign.</param>
		/// <returns>Result of the vector multiplication with a scalar.</returns>
		public static Point operator *(float scaleFactor, Point value)
		{
			value.X *= scaleFactor;
			value.Y *= scaleFactor;
			return value;
		}

		/// <summary>
		/// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/> on the left of the div sign.</param>
		/// <param name="value2">Divisor <see cref="Point"/> on the right of the div sign.</param>
		/// <returns>The result of dividing the vectors.</returns>
		public static Point operator /(Point value1, Point value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Point"/> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/> on the left of the div sign.</param>
		/// <param name="divider">Divisor scalar on the right of the div sign.</param>
		/// <returns>The result of dividing a vector by a scalar.</returns>
		public static Point operator /(Point value1, float divider)
		{
			float factor = 1 / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		/// <summary>
		/// Compares whether two <see cref="Point"/> instances are equal.
		/// </summary>
		/// <param name="value1"><see cref="Point"/> instance on the left of the equal sign.</param>
		/// <param name="value2"><see cref="Point"/> instance on the right of the equal sign.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public static bool operator ==(Point value1, Point value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y;
		}

		/// <summary>
		/// Compares whether two <see cref="Point"/> instances are not equal.
		/// </summary>
		/// <param name="value1"><see cref="Point"/> instance on the left of the not equal sign.</param>
		/// <param name="value2"><see cref="Point"/> instance on the right of the not equal sign.</param>
		/// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
		public static bool operator !=(Point value1, Point value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Performs vector addition on <paramref name="value1"/> and <paramref name="value2"/>.
		/// </summary>
		/// <param name="value1">The first vector to add.</param>
		/// <param name="value2">The second vector to add.</param>
		/// <returns>The result of the vector addition.</returns>
		public static Point Add(Point value1, Point value2)
		{
			value1.X += value2.X;
			value1.Y += value2.Y;
			return value1;
		}

		/// <summary>
		/// Performs vector addition on <paramref name="value1"/> and
		/// <paramref name="value2"/>, storing the result of the
		/// addition in <paramref name="result"/>.
		/// </summary>
		/// <param name="value1">The first vector to add.</param>
		/// <param name="value2">The second vector to add.</param>
		/// <param name="result">The result of the vector addition.</param>
		public static void Add(ref Point value1, ref Point value2, out Point result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		/// <summary>
		/// Returns the distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The distance between two vectors.</returns>
		public static float Distance(Point value1, Point value2)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (float)Math.Sqrt((v1 * v1) + (v2 * v2));
		}

		/// <summary>
		/// Returns the distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The distance between two vectors as an output parameter.</param>
		public static void Distance(ref Point value1, ref Point value2, out float result)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (float)Math.Sqrt((v1 * v1) + (v2 * v2));
		}

		/// <summary>
		/// Returns the squared distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The squared distance between two vectors.</returns>
		public static float DistanceSquared(Point value1, Point value2)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (v1 * v1) + (v2 * v2);
		}

		/// <summary>
		/// Returns the squared distance between two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The squared distance between two vectors as an output parameter.</param>
		public static void DistanceSquared(ref Point value1, ref Point value2, out float result)
		{
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (v1 * v1) + (v2 * v2);
		}

		/// <summary>
		/// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="value2">Divisor <see cref="Point"/>.</param>
		/// <returns>The result of dividing the vectors.</returns>
		public static Point Divide(Point value1, Point value2)
		{
			value1.X /= value2.X;
			value1.Y /= value2.Y;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Point"/> by the components of another <see cref="Point"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="value2">Divisor <see cref="Point"/>.</param>
		/// <param name="result">The result of dividing the vectors as an output parameter.</param>
		public static void Divide(ref Point value1, ref Point value2, out Point result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		/// <summary>
		/// Divides the components of a <see cref="Point"/> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="divider">Divisor scalar.</param>
		/// <returns>The result of dividing a vector by a scalar.</returns>
		public static Point Divide(Point value1, float divider)
		{
			float factor = 1 / divider;
			value1.X *= factor;
			value1.Y *= factor;
			return value1;
		}

		/// <summary>
		/// Divides the components of a <see cref="Point"/> by a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="divider">Divisor scalar.</param>
		/// <param name="result">The result of dividing a vector by a scalar as an output parameter.</param>
		public static void Divide(ref Point value1, float divider, out Point result)
		{
			float factor = 1 / divider;
			result.X = value1.X * factor;
			result.Y = value1.Y * factor;
		}

		/// <summary>
		/// Returns a dot product of two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The dot product of two vectors.</returns>
		public static float Dot(Point value1, Point value2)
		{
			return (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		/// <summary>
		/// Returns a dot product of two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The dot product of two vectors as an output parameter.</param>
		public static void Dot(ref Point value1, ref Point value2, out float result)
		{
			result = (value1.X * value2.X) + (value1.Y * value2.Y);
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Object"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Point)
			{
				return Equals((Point)obj);
			}

			return false;
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Point"/>.
		/// </summary>
		/// <param name="other">The <see cref="Point"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public bool Equals(Point other)
		{
			return (X == other.X) && (Y == other.Y);
		}

		/// <summary>
		/// Gets the hash code of this <see cref="Point"/>.
		/// </summary>
		/// <returns>Hash code of this <see cref="Point"/>.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode();
		}

		/// <summary>
		/// Returns the length of this <see cref="Point"/>.
		/// </summary>
		/// <returns>The length of this <see cref="Point"/>.</returns>
		public float Length()
		{
			return (float)Math.Sqrt((X * X) + (Y * Y));
		}

		/// <summary>
		/// Returns the squared length of this <see cref="Point"/>.
		/// </summary>
		/// <returns>The squared length of this <see cref="Point"/>.</returns>
		public float LengthSquared()
		{
			return (X * X) + (Y * Y);
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a maximal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The <see cref="Point"/> with maximal values from the two vectors.</returns>
		public static Point Max(Point value1, Point value2)
		{
			return new Point(value1.X > value2.X ? value1.X : value2.X,
							   value1.Y > value2.Y ? value1.Y : value2.Y);
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a maximal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The <see cref="Point"/> with maximal values from the two vectors as an output parameter.</param>
		public static void Max(ref Point value1, ref Point value2, out Point result)
		{
			result.X = value1.X > value2.X ? value1.X : value2.X;
			result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a minimal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The <see cref="Point"/> with minimal values from the two vectors.</returns>
		public static Point Min(Point value1, Point value2)
		{
			return new Point(value1.X < value2.X ? value1.X : value2.X,
							   value1.Y < value2.Y ? value1.Y : value2.Y);
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a minimal values from the two vectors.
		/// </summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="result">The <see cref="Point"/> with minimal values from the two vectors as an output parameter.</param>
		public static void Min(ref Point value1, ref Point value2, out Point result)
		{
			result.X = value1.X < value2.X ? value1.X : value2.X;
			result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a multiplication of two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="value2">Source <see cref="Point"/>.</param>
		/// <returns>The result of the vector multiplication.</returns>
		public static Point Multiply(Point value1, Point value2)
		{
			value1.X *= value2.X;
			value1.Y *= value2.Y;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a multiplication of two vectors.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="value2">Source <see cref="Point"/>.</param>
		/// <param name="result">The result of the vector multiplication as an output parameter.</param>
		public static void Multiply(ref Point value1, ref Point value2, out Point result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a multiplication of <see cref="Point"/> and a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <returns>The result of the vector multiplication with a scalar.</returns>
		public static Point Multiply(Point value1, float scaleFactor)
		{
			value1.X *= scaleFactor;
			value1.Y *= scaleFactor;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a multiplication of <see cref="Point"/> and a scalar.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="scaleFactor">Scalar value.</param>
		/// <param name="result">The result of the multiplication with a scalar as an output parameter.</param>
		public static void Multiply(ref Point value1, float scaleFactor, out Point result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains the specified vector inversion.
		/// </summary>
		/// <param name="value">Source <see cref="Point"/>.</param>
		/// <returns>The result of the vector inversion.</returns>
		public static Point Negate(Point value)
		{
			value.X = -value.X;
			value.Y = -value.Y;
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains the specified vector inversion.
		/// </summary>
		/// <param name="value">Source <see cref="Point"/>.</param>
		/// <param name="result">The result of the vector inversion as an output parameter.</param>
		public static void Negate(ref Point value, out Point result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		/// <summary>
		/// Turns this <see cref="Point"/> to a unit vector with the same direction.
		/// </summary>
		public void Normalize()
		{
			float val = 1.0f / (float)Math.Sqrt((X * X) + (Y * Y));
			X *= val;
			Y *= val;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a normalized values from another vector.
		/// </summary>
		/// <param name="value">Source <see cref="Point"/>.</param>
		/// <returns>Unit vector.</returns>
		public static Point Normalize(Point value)
		{
			float val = 1.0f / (float)Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
			value.X *= val;
			value.Y *= val;
			return value;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains a normalized values from another vector.
		/// </summary>
		/// <param name="value">Source <see cref="Point"/>.</param>
		/// <param name="result">Unit vector as an output parameter.</param>
		public static void Normalize(ref Point value, out Point result)
		{
			float val = 1.0f / (float)Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
			result.X = value.X * val;
			result.Y = value.Y * val;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains reflect vector of the given vector and normal.
		/// </summary>
		/// <param name="vector">Source <see cref="Point"/>.</param>
		/// <param name="normal">Reflection normal.</param>
		/// <returns>Reflected vector.</returns>
		public static Point Reflect(Point vector, Point normal)
		{
			Point result;
			float val = 2.0f * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
			return result;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains reflect vector of the given vector and normal.
		/// </summary>
		/// <param name="vector">Source <see cref="Point"/>.</param>
		/// <param name="normal">Reflection normal.</param>
		/// <param name="result">Reflected vector as an output parameter.</param>
		public static void Reflect(ref Point vector, ref Point normal, out Point result)
		{
			float val = 2.0f * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains subtraction of on <see cref="Point"/> from a another.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="value2">Source <see cref="Point"/>.</param>
		/// <returns>The result of the vector subtraction.</returns>
		public static Point Subtract(Point value1, Point value2)
		{
			value1.X -= value2.X;
			value1.Y -= value2.Y;
			return value1;
		}

		/// <summary>
		/// Creates a new <see cref="Point"/> that contains subtraction of on <see cref="Point"/> from a another.
		/// </summary>
		/// <param name="value1">Source <see cref="Point"/>.</param>
		/// <param name="value2">Source <see cref="Point"/>.</param>
		/// <param name="result">The result of the vector subtraction as an output parameter.</param>
		public static void Subtract(ref Point value1, ref Point value2, out Point result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		/// <summary>
		/// Returns a <see cref="String"/> representation of this <see cref="Point"/> in the format:
		/// {X:[<see cref="X"/>] Y:[<see cref="Y"/>]}
		/// </summary>
		/// <returns>A <see cref="String"/> representation of this <see cref="Point"/>.</returns>
		public override string ToString()
		{
			return "{X:" + X + " Y:" + Y + "}";
		}

		#endregion
	}
}