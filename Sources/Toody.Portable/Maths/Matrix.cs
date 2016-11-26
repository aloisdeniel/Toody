using System;
using System.Runtime.InteropServices;

namespace Toody
{

	[StructLayout(LayoutKind.Sequential)]
	public struct Matrix : IEquatable<Matrix>
	{
		#region Fields & Access

		/// <summary>Row 0, Column 0</summary>
		public float R0C0;

		/// <summary>Row 0, Column 1</summary>
		public float R0C1;

		/// <summary>Row 1, Column 0</summary>
		public float R1C0;

		/// <summary>Row 1, Column 1</summary>
		public float R1C1;

		/// <summary>Gets the component at the given row and column in the matrix.</summary>
		/// <param name="row">The row of the matrix.</param>
		/// <param name="column">The column of the matrix.</param>
		/// <returns>The component at the given row and column in the matrix.</returns>
		public float this[int row, int column]
		{
			get
			{
				switch (row)
				{
					case 0:
						switch (column)
						{
							case 0: return R0C0;
							case 1: return R0C1;
						}
						break;

					case 1:
						switch (column)
						{
							case 0: return R1C0;
							case 1: return R1C1;
						}
						break;
				}

				throw new IndexOutOfRangeException();
			}
			set
			{
				switch (row)
				{
					case 0:
						switch (column)
						{
							case 0: R0C0 = value; return;
							case 1: R0C1 = value; return;
						}
						break;

					case 1:
						switch (column)
						{
							case 0: R1C0 = value; return;
							case 1: R1C1 = value; return;
						}
						break;
				}

				throw new IndexOutOfRangeException();
			}
		}

		/// <summary>Gets the component at the index into the matrix.</summary>
		/// <param name="index">The index into the components of the matrix.</param>
		/// <returns>The component at the given index into the matrix.</returns>
		public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return R0C0;
					case 1: return R0C1;
					case 2: return R1C0;
					case 3: return R1C1;
					default: throw new IndexOutOfRangeException();
				}
			}
			set
			{
				switch (index)
				{
					case 0: R0C0 = value; return;
					case 1: R0C1 = value; return;
					case 2: R1C0 = value; return;
					case 3: R1C1 = value; return;
					default: throw new IndexOutOfRangeException();
				}
			}
		}

		/// <summary>Converts the matrix into an IntPtr.</summary>
		/// <param name="matrix">The matrix to convert.</param>
		/// <returns>An IntPtr for the matrix.</returns>
		public static explicit operator IntPtr(Matrix matrix)
		{
			unsafe
			{
				return (IntPtr)(&matrix.R0C0);
			}
		}

		/// <summary>Converts the matrix into left float*.</summary>
		/// <param name="matrix">The matrix to convert.</param>
		/// <returns>A float* for the matrix.</returns>
		[CLSCompliant(false)]
		unsafe public static explicit operator float* (Matrix matrix)
		{
			return &matrix.R0C0;
		}

		/// <summary>Converts the matrix into an array of floats.</summary>
		/// <param name="matrix">The matrix to convert.</param>
		/// <returns>An array of floats for the matrix.</returns>
		public static explicit operator float[] (Matrix matrix)
		{
			return new float[4]
			{
				matrix.R0C0,
				matrix.R0C1,
				matrix.R1C0,
				matrix.R1C1,
			};
		}

		#endregion

		#region Constructors

		/// <summary>Constructs left matrix with the same components as the given matrix.</summary>
		/// <param name="vector">The matrix whose components to copy.</param>
		public Matrix(ref Matrix matrix)
		{
			this.R0C0 = matrix.R0C0;
			this.R0C1 = matrix.R0C1;
			this.R1C0 = matrix.R1C0;
			this.R1C1 = matrix.R1C1;
		}

		/// <summary>Constructs left matrix with the given values.</summary>
		/// <param name="r0c0">The value for row 0 column 0.</param>
		/// <param name="r0c1">The value for row 0 column 1.</param>
		/// <param name="r1c0">The value for row 1 column 0.</param>
		/// <param name="r1c1">The value for row 1 column 1.</param>
		public Matrix
		(
			float r0c0,
			float r0c1,
			float r1c0,
			float r1c1
		)
		{
			this.R0C0 = r0c0;
			this.R0C1 = r0c1;
			this.R1C0 = r1c0;
			this.R1C1 = r1c1;
		}

		/// <summary>Constructs left matrix from the given array of float-precision floating-point numbers.</summary>
		/// <param name="floatArray">The array of floats for the components of the matrix.</param>
		public Matrix(float[] floatArray)
		{
			if (floatArray == null || floatArray.GetLength(0) < 9) throw new ArgumentException("floatArray has bad size");

			this.R0C0 = floatArray[0];
			this.R0C1 = floatArray[1];
			this.R1C0 = floatArray[2];
			this.R1C1 = floatArray[3];
		}

		#endregion

		#region Equality

		/// <summary>Indicates whether the current matrix is equal to another matrix.</summary>
		/// <param name="matrix">The OpenTK.Matrix2 structure to compare with.</param>
		/// <returns>true if the current matrix is equal to the matrix parameter; otherwise, false.</returns>
		[CLSCompliant(false)]
		public bool Equals(Matrix matrix)
		{
			return
				R0C0 == matrix.R0C0 &&
				R0C1 == matrix.R0C1 &&
				R1C0 == matrix.R1C0 &&
				R1C1 == matrix.R1C1;
		}

		/// <summary>Indicates whether the current matrix is equal to another matrix.</summary>
		/// <param name="matrix">The OpenTK.Matrix2 structure to compare to.</param>
		/// <returns>true if the current matrix is equal to the matrix parameter; otherwise, false.</returns>
		public bool Equals(ref Matrix matrix)
		{
			return
				R0C0 == matrix.R0C0 &&
				R0C1 == matrix.R0C1 &&
				R1C0 == matrix.R1C0 &&
				R1C1 == matrix.R1C1;
		}

		/// <summary>Indicates whether the current matrix is equal to another matrix.</summary>
		/// <param name="left">The left-hand operand.</param>
		/// <param name="right">The right-hand operand.</param>
		/// <returns>true if the current matrix is equal to the matrix parameter; otherwise, false.</returns>
		public static bool Equals(ref Matrix left, ref Matrix right)
		{
			return
				left.R0C0 == right.R0C0 &&
				left.R0C1 == right.R0C1 &&
				left.R1C0 == right.R1C0 &&
				left.R1C1 == right.R1C1;
		}

		/// <summary>Indicates whether the current matrix is approximately equal to another matrix.</summary>
		/// <param name="matrix">The OpenTK.Matrix2 structure to compare with.</param>
		/// <param name="tolerance">The limit below which the matrices are considered equal.</param>
		/// <returns>true if the current matrix is approximately equal to the matrix parameter; otherwise, false.</returns>
		public bool EqualsApprox(ref Matrix matrix, float tolerance)
		{
			return
				System.Math.Abs(R0C0 - matrix.R0C0) <= tolerance &&
				System.Math.Abs(R0C1 - matrix.R0C1) <= tolerance &&
				System.Math.Abs(R1C0 - matrix.R1C0) <= tolerance &&
				System.Math.Abs(R1C1 - matrix.R1C1) <= tolerance;
		}

		/// <summary>Indicates whether the current matrix is approximately equal to another matrix.</summary>
		/// <param name="left">The left-hand operand.</param>
		/// <param name="right">The right-hand operand.</param>
		/// <param name="tolerance">The limit below which the matrices are considered equal.</param>
		/// <returns>true if the current matrix is approximately equal to the matrix parameter; otherwise, false.</returns>
		public static bool EqualsApprox(ref Matrix left, ref Matrix right, float tolerance)
		{
			return
				System.Math.Abs(left.R0C0 - right.R0C0) <= tolerance &&
				System.Math.Abs(left.R0C1 - right.R0C1) <= tolerance &&
				System.Math.Abs(left.R1C0 - right.R1C0) <= tolerance &&
				System.Math.Abs(left.R1C1 - right.R1C1) <= tolerance;
		}

		#endregion

		#region Arithmetic Operators


		/// <summary>Add left matrix to this matrix.</summary>
		/// <param name="matrix">The matrix to add.</param>
		public void Add(ref Matrix matrix)
		{
			R0C0 = R0C0 + matrix.R0C0;
			R0C1 = R0C1 + matrix.R0C1;
			R1C0 = R1C0 + matrix.R1C0;
			R1C1 = R1C1 + matrix.R1C1;
		}

		/// <summary>Add left matrix to this matrix.</summary>
		/// <param name="matrix">The matrix to add.</param>
		/// <param name="result">The resulting matrix of the addition.</param>
		public void Add(ref Matrix matrix, out Matrix result)
		{
			result.R0C0 = R0C0 + matrix.R0C0;
			result.R0C1 = R0C1 + matrix.R0C1;
			result.R1C0 = R1C0 + matrix.R1C0;
			result.R1C1 = R1C1 + matrix.R1C1;
		}

		/// <summary>Add left matrix to left matrix.</summary>
		/// <param name="matrix">The matrix on the matrix side of the equation.</param>
		/// <param name="right">The matrix on the right side of the equation</param>
		/// <param name="result">The resulting matrix of the addition.</param>
		public static void Add(ref Matrix left, ref Matrix right, out Matrix result)
		{
			result.R0C0 = left.R0C0 + right.R0C0;
			result.R0C1 = left.R0C1 + right.R0C1;
			result.R1C0 = left.R1C0 + right.R1C0;
			result.R1C1 = left.R1C1 + right.R1C1;
		}


		/// <summary>Subtract left matrix from this matrix.</summary>
		/// <param name="matrix">The matrix to subtract.</param>
		public void Subtract(ref Matrix matrix)
		{
			R0C0 = R0C0 - matrix.R0C0;
			R0C1 = R0C1 - matrix.R0C1;
			R1C0 = R1C0 - matrix.R1C0;
			R1C1 = R1C1 - matrix.R1C1;
		}

		/// <summary>Subtract matrix from this matrix.</summary>
		/// <param name="matrix">The matrix to subtract.</param>
		/// <param name="result">The resulting matrix of the subtraction.</param>
		public void Subtract(ref Matrix matrix, out Matrix result)
		{
			result.R0C0 = R0C0 - matrix.R0C0;
			result.R0C1 = R0C1 - matrix.R0C1;
			result.R1C0 = R1C0 - matrix.R1C0;
			result.R1C1 = R1C1 - matrix.R1C1;
		}

		/// <summary>Subtract right matrix from left matrix.</summary>
		/// <param name="matrix">The matrix on the matrix side of the equation.</param>
		/// <param name="right">The matrix on the right side of the equation</param>
		/// <param name="result">The resulting matrix of the subtraction.</param>
		public static void Subtract(ref Matrix left, ref Matrix right, out Matrix result)
		{
			result.R0C0 = left.R0C0 - right.R0C0;
			result.R0C1 = left.R0C1 - right.R0C1;
			result.R1C0 = left.R1C0 - right.R1C0;
			result.R1C1 = left.R1C1 - right.R1C1;
		}


		/// <summary>Multiply left martix times this matrix.</summary>
		/// <param name="matrix">The matrix to multiply.</param>
		public void Multiply(ref Matrix matrix)
		{
			float r0c0 = matrix.R0C0 * R0C0 + matrix.R0C1 * R1C0;
			float r0c1 = matrix.R0C0 * R0C1 + matrix.R0C1 * R1C1;

			float r1c0 = matrix.R1C0 * R0C0 + matrix.R1C1 * R1C0;
			float r1c1 = matrix.R1C0 * R0C1 + matrix.R1C1 * R1C1;

			R0C0 = r0c0;
			R0C1 = r0c1;

			R1C0 = r1c0;
			R1C1 = r1c1;
		}

		/// <summary>Multiply left martix times this matrix.</summary>
		/// <param name="matrix">The matrix to multiply.</param>
		public Matrix Multiply(Matrix matrix)
		{
			this.Multiply(ref matrix);
			return matrix;
		}

		/// <summary>Multiply matrix times this matrix.</summary>
		/// <param name="matrix">The matrix to multiply.</param>
		/// <param name="result">The resulting matrix of the multiplication.</param>
		public void Multiply(ref Matrix matrix, out Matrix result)
		{
			result.R0C0 = matrix.R0C0 * R0C0 + matrix.R0C1 * R1C0;
			result.R0C1 = matrix.R0C0 * R0C1 + matrix.R0C1 * R1C1;
			result.R1C0 = matrix.R1C0 * R0C0 + matrix.R1C1 * R1C0;
			result.R1C1 = matrix.R1C0 * R0C1 + matrix.R1C1 * R1C1;
		}

		/// <summary>Multiply left matrix times left matrix.</summary>
		/// <param name="matrix">The matrix on the matrix side of the equation.</param>
		/// <param name="right">The matrix on the right side of the equation</param>
		/// <param name="result">The resulting matrix of the multiplication.</param>
		public static void Multiply(ref Matrix left, ref Matrix right, out Matrix result)
		{
			result.R0C0 = right.R0C0 * left.R0C0 + right.R0C1 * left.R1C0;
			result.R0C1 = right.R0C0 * left.R0C1 + right.R0C1 * left.R1C1;
			result.R1C0 = right.R1C0 * left.R0C0 + right.R1C1 * left.R1C0;
			result.R1C1 = right.R1C0 * left.R0C1 + right.R1C1 * left.R1C1;
		}


		/// <summary>Multiply matrix times this matrix.</summary>
		/// <param name="matrix">The matrix to multiply.</param>
		public void Multiply(float scalar)
		{
			R0C0 = scalar * R0C0;
			R0C1 = scalar * R0C1;
			R1C0 = scalar * R1C0;
			R1C1 = scalar * R1C1;
		}

		/// <summary>Multiply matrix times this matrix.</summary>
		/// <param name="matrix">The matrix to multiply.</param>
		/// <param name="result">The resulting matrix of the multiplication.</param>
		public void Multiply(float scalar, out Matrix result)
		{
			result.R0C0 = scalar * R0C0;
			result.R0C1 = scalar * R0C1;
			result.R1C0 = scalar * R1C0;
			result.R1C1 = scalar * R1C1;
		}

		/// <summary>Multiply left matrix times left matrix.</summary>
		/// <param name="matrix">The matrix on the matrix side of the equation.</param>
		/// <param name="right">The matrix on the right side of the equation</param>
		/// <param name="result">The resulting matrix of the multiplication.</param>
		public static void Multiply(ref Matrix matrix, float scalar, out Matrix result)
		{
			result.R0C0 = scalar * matrix.R0C0;
			result.R0C1 = scalar * matrix.R0C1;
			result.R1C0 = scalar * matrix.R1C0;
			result.R1C1 = scalar * matrix.R1C1;
		}


		#endregion

		#region Swap helpers

		/// <summary>
		/// Swaps two float values.
		/// </summary>
		/// <param name="a">The first value.</param>
		/// <param name="b">The second value.</param>
		public static void Swap(ref float a, ref float b)
		{
			float temp = a;
			a = b;
			b = temp;
		}

		#endregion

		#region Functions

		public float Determinant
		{
			get
			{
				return R0C0 * R1C1 - R0C1 * R1C0;
			}
		}

		public void Transpose()
		{
			Swap(ref R0C1, ref R1C0);
		}
		public void Transpose(out Matrix result)
		{
			result.R0C0 = R0C0;
			result.R0C1 = R1C0;
			result.R1C0 = R0C1;
			result.R1C1 = R1C1;
		}
		public static void Transpose(ref Matrix matrix, out Matrix result)
		{
			result.R0C0 = matrix.R0C0;
			result.R0C1 = matrix.R1C0;
			result.R1C0 = matrix.R0C1;
			result.R1C1 = matrix.R1C1;
		}

		#endregion

		#region Transformation Functions

		public void Transform(ref Point vector)
		{
			float x = R0C0 * vector.X + R0C1 * vector.Y;
			vector.Y = R1C0 * vector.X + R1C1 * vector.Y;
			vector.X = x;
		}
		public static void Transform(ref Matrix matrix, ref Point vector)
		{
			float x = matrix.R0C0 * vector.X + matrix.R0C1 * vector.Y;
			vector.Y = matrix.R1C0 * vector.X + matrix.R1C1 * vector.Y;
			vector.X = x;
		}
		public void Transform(ref Point vector, out Point result)
		{
			result.X = R0C0 * vector.X + R0C1 * vector.Y;
			result.Y = R1C0 * vector.X + R1C1 * vector.Y;
		}
		public static void Transform(ref Matrix matrix, ref Point vector, out Point result)
		{
			result.X = matrix.R0C0 * vector.X + matrix.R0C1 * vector.Y;
			result.Y = matrix.R1C0 * vector.X + matrix.R1C1 * vector.Y;
		}

		public static Matrix Rotation(float angleRadians)
		{
			var sin = (float)System.Math.Sin(angleRadians);
			var cos = (float)System.Math.Cos(angleRadians);
			return new Matrix(cos,-sin,sin,cos);
		}

		public static Matrix Scale(float x, float y) => new Matrix(x, 0, 0, y);

		#endregion

		#region Constants

		/// <summary>The identity matrix.</summary>
		public static readonly Matrix Identity = new Matrix
		(
			1, 0,
			0, 1
		);

		/// <summary>A matrix of all zeros.</summary>
		public static readonly Matrix Zero = new Matrix
		(
			0, 0,
			0, 0
		);

		#endregion

		#region HashCode

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
		public override int GetHashCode()
		{
			return
				R0C0.GetHashCode() ^ R0C1.GetHashCode() ^
				R1C0.GetHashCode() ^ R1C1.GetHashCode();
		}

		#endregion

		#region String

		/// <summary>Returns the fully qualified type name of this instance.</summary>
		/// <returns>A System.String containing left fully qualified type name.</returns>
		public override string ToString()
		{
			return String.Format(
				"|{00}, {01}|\n" +
				"|{02}, {03}|\n",
				R0C0, R0C1,
				R1C0, R1C1);
		}

		#endregion
	}
#pragma warning restore 3019
}