using System;
using System.Linq;
using Gamedev.Main.Objects.Cards;
using Godot;

namespace Gamedev.Main.Events
{
	/// <summary>
	/// Events that trigger when some collision occured. Player hits enemy, or spikes.
	/// </summary>
	public static class CollisionEvents
	{
		public static event Action CollisionDeath;
		public static void OnCollisionDeath() => CollisionDeath();

		public static event Action BatteryCollected;
		public static void OnBatteryCollected() => BatteryCollected();

		/// <summary>
		/// Triggers to check if the palyer has the needed battery to activate a light gem.
		/// </summary>
		public static event Action LightTouched;
		public static void OnLightTouched() => LightTouched();

		/// <summary>
		/// If the palyer has the battery one is deducted and the light gem is turned on.
		/// </summary>
		public static event Action LightActivated = delegate { };
		public static void OnLightActivated() => LightActivated();

		/// <summary>
		/// When a card is collected the card is added to the inventory. The inventory is informed by this signal about 
		/// what card type was collected.
		/// </summary>
		public static event Action<PowerUpCard> CardCollected;
		public static void OnCardCollected(PowerUpCard card) => CardCollected(card);

		/// <summary>
		/// Card is removed from the inventory
		/// </summary>
		public static event Action<PowerUpCard> CardConsumed;
		public static void OnCardConsumed(PowerUpCard card) => CardConsumed(card);

		/// <summary>
		/// When a card is consumed the display and headband need to chage.
		/// </summary>
		public static event Action<PowerUpCard> CurrentCardChanged;
		public static void OnCurrentCardChanged(PowerUpCard card) => CurrentCardChanged(card);

		/// <summary>
		/// Event for when the palyer moves into a new room.
		/// </summary>
		public static event Action<Vector2> CameraTransitionTriggered;
		public static void OnCameraTransitionTriggered(Vector2 pos) => CameraTransitionTriggered(pos);

		public static event Action ObjectBroken;
		public static void OnObjectBroken() => ObjectBroken();

		/// <summary>
		/// When the goal is reached the next level is opened and in the persistent save the finished level is marked.
		/// </summary>
		public static event Action GoalReached;
		public static void OnGoalReached() => GoalReached();

		static CollisionEvents()
		{
			Clear();
		}

		/// <summary>
		/// CLears all signals so that when a new level is loaded no old connections are up which could lead to nullpointers.
		/// </summary>
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
			GoalReached = delegate { };
		}
	}
}