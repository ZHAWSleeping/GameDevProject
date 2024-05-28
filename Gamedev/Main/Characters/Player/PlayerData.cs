using Godot;
using static Gamedev.Main.Characters.Players.PlayerFSM;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.Characters.Players
{
	/// <summary>
	/// Contains parameters for the palyer.
	/// Should be changed in the editor.
	/// 
	/// </summary>
	[GlobalClass]
	public partial class PlayerData : Resource
	{

		[Export]
		public float MovementSpeed;
		[Export]
		public float MaxMovementSpeed;
		[Export]
		public float AirDrag;
		[Export]
		public float JumpVelocity;
		[Export]
		public float JumpVelocityIncrement;
		[Export]
		public Vector2 WallJumpVelocity;
		[Export]
		public float WallJumpVelocityIncrement;
		[Export]
		public float WallSlideModifier;
		[Export]
		public float AirborneModifier;
		[Export]
		public float DashVelocity;
		[Export]
		public float StompVelocity;
		[Export]
		public int CoyoteTimeFrames;
		[Export]
		public int JumpTimeFrames;
		[Export]
		public int DashTimeFrames;
		[Export]
		public Vector2 Gravity;

		public Player Player;
		public PlayerSprite Sprite;
		public PlayerParticleManager Particles;
		public PlayerAudioManager Audio;
		public ShapeCast2D LeftWallCast;
		public ShapeCast2D RightWallCast;
		public RectangleShape2D Shape;
		public State State = State.Grounded;
		public State PreviousState = State.Grounded;
		public Vector2 Velocity = Vector2.Zero;
		public Vector2 InputDirection = Vector2.Zero;
		public bool JumpHeld = false;
		public bool JumpJustPressed = false;
		public bool DiscardJustPressed = false;
		public int JustRespawned = 3;
		public Direction WallSide = Direction.None;
		public double Delta = 0;
		public int CoyoteTime = 0;
		public int JumpTime = 0;
		public int DashTime = 0;
		public Direction Facing = Direction.West;
		public Direction VisuallyFacing = Direction.West;

		/// <summary>
		/// Timers for dash, jump and coyote.
		/// Coyote timer is the timer for how many frames after leaving a platform the palyer has to still jump and not fall.
		/// Jump is for how much extra force is applied when holding the jump button.
		/// Dash is to manage the dash's duration.
		/// </summary>
		public void ResetTimers()
		{
			CoyoteTime = CoyoteTimeFrames;
			JumpTime = JumpTimeFrames;
			DashTime = DashTimeFrames;
		}
	}
}