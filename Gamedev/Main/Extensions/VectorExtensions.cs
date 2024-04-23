using System;
using Godot;

namespace Gamedev.Main.Extensions
{
	/// <summary>
	/// Utility class with some vector extension methods
	/// </summary>
	public static class VectorExtensions
	{

		private const int _octant = 8;
		private const int _quadrant = 4;

		/// <summary>
		/// Converts a floating-point vector into an integer vector
		/// </summary>
		/// <param name="vector">Input vector</param>
		/// <returns>New vector</returns>
		public static Vector2I ToVectorI(this Vector2 vector)
		{
			return new Vector2I((int)vector.X, (int)vector.Y);
		}

		/// <summary>
		/// Converts an integer vector into a floating-point vector
		/// </summary>
		/// <param name="vector">Input vector</param>
		/// <returns>New vector</returns>
		public static Vector2 ToVector(this Vector2I vector)
		{
			return new Vector2((float)vector.X, (float)vector.Y);
		}

		/// <summary>
		/// Enum for the eight directions
		/// </summary>
		public enum Direction
		{
			East,
			SouthEast,
			South,
			SouthWest,
			West,
			NorthWest,
			North,
			NorthEast,
			None,
		}

		/// <summary>
		/// Maps a Vector to one of eight directions. The output is the closest to the original direction. 
		/// </summary>
		/// <param name="direction">The Vector that will be mapped</param>
		/// <returns>The mapped direction as a String</returns>
		public static Direction ToOctalDirection(this Vector2 direction)
		{
			double angle = direction.Angle();
			Direction octant = (Direction)(Math.Round(_octant * angle / (2 * Math.PI) + _octant) % _octant);
			return octant;
		}

		/// <summary>
		/// Maps a Vector to one of four directions. The output is the closest to the original direction. 
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Direction ToQuadrantDirection(this Vector2 direction)
		{
			double angle = direction.Angle();
			Direction quadrant = (Direction)(2 * (Math.Round(_quadrant * angle / (2 * Math.PI) + _quadrant) % _quadrant));
			return quadrant;
		}

		/// <summary>
		/// Returns a normalized vector that points in the specified direction.
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		public static Vector2 ToVector(this Direction direction)
		{
			return Vector2.Right.Rotated(MathF.PI / 4 * (int)direction).Normalized();
		}

	}
}