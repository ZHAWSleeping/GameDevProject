using System;
using System.Linq;
using Gamedev.Main.Objects.Cards;
using Godot;

namespace Gamedev.Main.Events
{
	public static class CollisionEvents
	{
		public static event Action CollisionDeath;
		public static void OnCollisionDeath() => CollisionDeath();

		public static event Action BatteryCollected;
		public static void OnBatteryCollected() => BatteryCollected();

		public static event Action LightTouched;
		public static void OnLightTouched() => LightTouched();

		public static event Action LightActivated = delegate { };
		public static void OnLightActivated() => LightActivated();

		public static event Action<PowerUpCard> CardCollected;
		public static void OnCardCollected(PowerUpCard card) => CardCollected(card);

		public static event Action<PowerUpCard> CardConsumed;
		public static void OnCardConsumed(PowerUpCard card) => CardConsumed(card);

		public static event Action<PowerUpCard> CurrentCardChanged;
		public static void OnCurrentCardChanged(PowerUpCard card) => CurrentCardChanged(card);

		public static event Action<Vector2> CameraTransitionTriggered;
		public static void OnCameraTransitionTriggered(Vector2 pos) => CameraTransitionTriggered(pos);

		public static event Action ObjectBroken;
		public static void OnObjectBroken() => ObjectBroken();

		static CollisionEvents()
		{
			Clear();
		}

		public static void Clear()
		{
			CollisionDeath = delegate { };
			BatteryCollected = delegate { };
			LightTouched = delegate { };
			LightActivated = delegate { };
			CardCollected = delegate { };
			CardConsumed = delegate { };
			CurrentCardChanged = delegate { };
			CameraTransitionTriggered = delegate { };
			ObjectBroken = delegate { };
		}
	}
}