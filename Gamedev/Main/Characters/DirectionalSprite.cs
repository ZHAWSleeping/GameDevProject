using Gamedev.Main.Extensions;
using Godot;
using System;

namespace Gamedev.Main.Characters
{


	public partial class DirectionalSprite : AnimatedSprite2D
	{
		/// <summary>
		/// Rotates a sprite towards the general direction of the given rotation.
		/// </summary>
		/// <param name="direction">The general direction</param>
		public void AlignSprite(Vector2 direction)
		{
			switch (direction.ToQuadrantDirection())
			{
				case VectorExtensions.Direction.North:
					FlipH = false;
					Rotation = (float)-(Math.PI / 2.0);
					break;
				case VectorExtensions.Direction.South:
					FlipH = false;
					Rotation = (float)(Math.PI / 2.0);
					break;
				case VectorExtensions.Direction.East:
					FlipH = false;
					Rotation = 0;
					break;
				case VectorExtensions.Direction.West:
					FlipH = true;
					Rotation = 0;
					break;
			}

		}
	}
}
