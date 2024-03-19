using System;
using Godot;

namespace Gamedev.Main.Events
{
	public static class CollisionEvents
	{
		public static event Action CollisionWall = delegate { };
		public static void OnCollisionWall() => CollisionWall();
		public static event Action CollisionDeath = delegate { };
		public static void OnCollisionDeath() => CollisionDeath();

		static CollisionEvents()
		{
			CollisionWall += () => GD.Print("Wall hit");
			CollisionDeath += () => GD.Print("Collided to death");
		}

		public static void Clear()
		{
			CollisionWall = delegate { };
			CollisionDeath = delegate { };
		}


	}
}