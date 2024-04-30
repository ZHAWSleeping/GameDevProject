using Gamedev.Main.Constants;
using Godot;

namespace Gamedev.Main.Extensions
{
	public static class InputExtensions
	{
		public static Vector2 MovementVector()
		{
			return Input.GetVector(
				Inputs.West.ToString(),
				Inputs.East.ToString(),
				Inputs.North.ToString(),
				Inputs.South.ToString()
			);
		}

		public static bool Pressed(this Inputs input)
		{
			return Input.IsActionPressed(input.ToString());
		}

		public static bool JustPressed(this Inputs input)
		{
			return Input.IsActionJustPressed(input.ToString());
		}
	}
}