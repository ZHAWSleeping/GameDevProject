using System;
using Gamedev.Main.Characters.Player;
using Gamedev.Main.Extensions;
using Godot;

namespace Gamedev.Main.Events
{
	public static class DebugEvents
	{
		public static event Action<Vector2> PlayerSpeed;
		public static void OnPlayerSpeed(Vector2 speed) => PlayerSpeed(speed);

		public static event Action<bool> PlayerGrounded;
		public static void OnPlayerGrounded(bool grounded) => PlayerGrounded(grounded);

		public static event Action<bool> PlayerWall;
		public static void OnPlayerWall(bool wall) => PlayerWall(wall);

		public static event Action<bool> PlayerFalling;
		public static void OnPlayerFalling(bool falling) => PlayerFalling(falling);

		public static event Action<PlayerData> PlayerDataEvent;
		public static void OnPlayerDataEvent(PlayerData data) => PlayerDataEvent(data);

		public static event Action<VectorExtensions.Direction?> PlayerWallSide;
		public static void OnPlayerWallSide(VectorExtensions.Direction? side) => PlayerWallSide(side);

		public static event Action<LevelManager> RoomChanged;
		public static void OnRoomChanged(LevelManager manager) => RoomChanged(manager);


		static DebugEvents()
		{
			Clear();
		}

		public static void Clear()
		{
			PlayerSpeed = delegate { };
			PlayerGrounded = delegate { };
			PlayerWall = delegate { };
			PlayerFalling = delegate { };
			PlayerWallSide = delegate { };
			PlayerDataEvent = delegate { };
			RoomChanged = delegate { };
		}
	}
}