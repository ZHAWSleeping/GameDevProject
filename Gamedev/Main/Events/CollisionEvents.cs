using System;
using Godot;

namespace Gamedev.Main.Events
{
	public static class CollisionEvents
	{
		public static event Action CollisionWall;
		public static void OnCollisionWall() => CollisionWall();

		public static event Action CollisionDeath;
		public static void OnCollisionDeath() => CollisionDeath();

		public static event Action BatteryCollected;
		public static void OnBatteryCollected() => BatteryCollected();

		public static event Action LightTouched;
		public static void OnLightTouched() => LightTouched();

		public static event Action ActivateLight = delegate { };
		public static void OnActivateLight() => ActivateLight();

		public static event Action CollectedPowerUp;
		public static void OnCollectedPowerUp(int num) => CollectedPowerUp();

		static CollisionEvents()
		{
			Clear();
		}

		public static void Clear()
		{
			CollisionWall = delegate { };
			CollisionDeath = delegate { };
			BatteryCollected = delegate { };
			LightTouched = delegate { };
			ActivateLight = delegate { };
			CollectedPowerUp = delegate { };
		}
	}
}