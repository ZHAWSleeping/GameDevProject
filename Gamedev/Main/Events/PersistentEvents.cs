using System;
using Gamedev.Main.Objects.Cards;
using Gamedev.Main.Peristent;
using Godot;

namespace Gamedev.Main.Events
{
	public static class PersistentEvents
	{
		public static event Action QuitRequested;
		public static void OnQuitRequested() => QuitRequested();

		public static event Action<Vector2> PlayerDied;
		public static void OnPlayerDied(Vector2 playerPosition) => PlayerDied(playerPosition);

		public static event Action PauseRequested;
		public static void OnPauseRequested() => PauseRequested();

		public static event Action ResumeRequested;
		public static void OnResumeRequested() => ResumeRequested();

		public static event Action<PackedScene> SceneChangeRequested;
		public static void OnSceneChangeRequested(PackedScene scene) => SceneChangeRequested(scene);

		/// <summary>
		/// Triggered when a the lower resolution game scene is supposed to be replaced.
		/// </summary>
		public static event Action<PackedScene> GameSceneChangeRequested;
		/// <summary>
		/// Replaces the lower resolution game scene with the specified scene.
		/// </summary>
		/// <param name="scene"></param>
		public static void OnGameSceneChangeRequested(PackedScene scene) => GameSceneChangeRequested(scene);

		/// <summary>
		/// Triggered when a new level is supposed to be loaded. Does not contain the actual level.
		/// </summary>
		public static event Action<int, int> LevelChangeRequested;
		/// <summary>
		/// Triggers the change of the current level to the specified one.
		/// </summary>
		/// <param name="world">Which world</param>
		/// <param name="level">Which level</param>
		public static void OnLevelChangeRequested(int world, int level) => LevelChangeRequested(world, level);

		public static event Action<int> RoomChanged;
		public static void OnRoomChanged(int room) => RoomChanged(room);

		public static event Action<int, int> LevelFinished;
		public static void OnLevelFinished(int world, int level) => LevelFinished(world, level);


		public static event Action BatteryCollected;
		public static void OnBatteryCollected() => BatteryCollected();

		public static event Action LightActivated;
		public static void OnLightActivated() => LightActivated();

		public static event Action<PowerUpCard> CardCollected;
		public static void OnCardCollected(PowerUpCard card) => CardCollected(card);

		public static event Action<PowerUpCard> CardConsumed;
		public static void OnCardConsumed(PowerUpCard card) => CardConsumed(card);

		public static event Action<PowerUpCard> CurrentCardChanged;
		public static void OnCurrentCardChanged(PowerUpCard card) => CurrentCardChanged(card);

		public static event Action<SaveFile> SaveSelected;
		public static void OnSaveSelected(SaveFile file) => SaveSelected(file);

		public static event Action<SaveFile, int> WorldSelected;
		public static void OnWorldSelected(SaveFile file, int world) => WorldSelected(file, world);

		public static event Action<int, int> LevelSelected;
		public static void OnWorldSelected(int world, int level) => LevelSelected(world, level);



		static PersistentEvents()
		{
			Clear();
		}

		private static void Clear()
		{
			QuitRequested = delegate { };
			PlayerDied = delegate { };
			PauseRequested = delegate { };
			ResumeRequested = delegate { };
			SceneChangeRequested = delegate { };
			GameSceneChangeRequested = delegate { };
			LevelChangeRequested = delegate { };
			RoomChanged = delegate { };
			LevelFinished = delegate { };

			BatteryCollected = delegate { };
			LightActivated = delegate { };
			CardCollected = delegate { };
			CardConsumed = delegate { };
			CurrentCardChanged = delegate { };
		}
	}
}