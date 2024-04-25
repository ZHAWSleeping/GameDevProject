using Godot;
using static Gamedev.Main.Characters.Player.PlayerFSM;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.Characters.Player
{
	[GlobalClass]
	public partial class PlayerData : Resource
	{

		[Export]
		public float MovementSpeed;
		[Export]
		public float JumpVelocity;
		[Export]
		public float JumpVelocityIncrement;
		[Export]
		public float AirborneModifier;
		[Export]
		public int CoyoteTimeFrames;
		[Export]
		public int JumpTimeFrames;

		public Player Player;
		public PlayerSprite Sprite;
		public ShapeCast2D LeftWallCast;
		public ShapeCast2D RightWallCast;
		public RectangleShape2D Shape;
		public State State = State.Grounded;
		public State PreviousState = State.Grounded;
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 InputDirection = Vector2.Zero;
		public bool JumpHeld = false;
		public bool JumpJustPressed = false;
		public Direction WallSide = Direction.None;
		public double Delta = 0;
		public int CoyoteTime = 0;
		public int JumpTime = 0;


	}
}