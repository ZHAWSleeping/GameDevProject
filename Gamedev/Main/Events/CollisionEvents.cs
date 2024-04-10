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
		public static event Action BatteryCollected = delegate { };
		public static void OnBatteryCollected() => BatteryCollected();


		static CollisionEvents()
		{
			CollisionWall += () => GD.Print("Wall hit");
			CollisionDeath += () => GD.Print("Collided to death");
			BatteryCollected += () => GD.Print("Battery collected");
		}

		public static void Clear()
		{
			CollisionWall = delegate { };
			CollisionDeath = delegate { };
			BatteryCollected = delegate { };
		}


	}
}