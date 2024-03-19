using System;
using Godot;

namespace Gamedev.Main.Events
{
	public static class CollisionEvents
	{
		public static event Action CollisionWall = delegate {};
		public static void OnCollisionWall() => CollisionWall();

		static CollisionEvents()
		{
			CollisionWall += () => GD.Print("Wall hit");
		}

		public static void Clear() {
			CollisionWall = delegate { };
		}
	}
}