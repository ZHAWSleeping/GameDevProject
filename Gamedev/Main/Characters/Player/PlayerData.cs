
using Godot;

namespace Gamedev.Main.Characters.Player
{
	public record class PlayerData
	{
		public Player.PlayerState State { get; set; } = Player.PlayerState.Grounded;
		public Player.PlayerState PreviousState { get; set; } = Player.PlayerState.Grounded;
		public Vector2 Velocity { get; set; } = Vector2.Zero;
		public Vector2 InputDirection { get; set; } = Vector2.Zero;
		public bool JumpHeld { get; set; } = false;
		public bool JumpJustPressed { get; set; } = false;
		public int CoyoteTime { get; set; } = 0;
		public int JumpTime { get; set; } = 0;
		public double Delta = 0;
	}
}