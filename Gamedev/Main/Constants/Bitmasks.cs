namespace Gamedev.Main.Constants
{
	public static class Bitmasks
	{
		public enum Physics2DLayer
		{
			Player = 0b1 << 0,
			Walls = 0b1 << 1,
			CameraTriggers = 0b1 << 2,
			DeathTriggers = 0b1 << 3,
			Pickups = 0b1 << 4,
			Enemies = 0b1 << 5,
			Obstacles = 0b1 << 6,
			Breakable = 0b1 << 7,
		}

		public enum Render2DLayer
		{
			Default = 0b1 << 0,
			BackgroundLit = 0b1 << 1,
			BackgroundOccluders = 0b1 << 2,
			BlockLit = 0b1 << 3,
			BlockOccluders = 0b1 << 4,
			PlayerLit = 0b1 << 5,
			PlayerOccluders = 0b1 << 6,
		}
	}
}