using System;
using Gamedev.Main.Objects.Cards;
using Gamedev.Main.Persistent;
using Godot;

namespace Gamedev.Main.Events
{
	public static class PersistentEvents
	{
		/// <summary>
		/// Triggered when the game should be closed
		/// </summary>
		public static event Action QuitRequested;
		/// <summary>
		/// Closes the game
		/// </summary>
		public static void OnQuitRequested() => QuitRequested();

		/// <summary>
		/// Fired when the player dies
		/// </summary>
		public static event Action<Vector2> PlayerDied;
		/// <summary>
		/// Notifies that the player has died
		/// </summary>
		/// <param name="playerPosition">Where the player died</param>
		public static void OnPlayerDied(Vector2 playerPosition) => PlayerDied(playerPosition);

		/// <summary>
		/// Triggered when the game should be paused
		/// </summary>
		public static event Action PauseRequested;
		/// <summary>
		/// Pauses the game
		/// </summary>
		public static void OnPauseRequested() => PauseRequested();

		/// <summary>
		/// Triggered when the game should be unpaused
		/// </summary>
		public static event Action ResumeRequested;
		/// <summary>
		/// Unpauses the game
		/// </summary>
		public static void OnResumeRequested() => ResumeRequested();

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
		public static event Action<GameState> LevelChangeRequested;
		/// <summary>
		/// Triggers the change of the current level to the specified one.
		/// </summary>
		public static void OnLevelChangeRequested(GameState state) => LevelChangeRequested(state);

		/// <summary>
		/// Triggered when the room has changed
		/// </summary>
		public static event Action<int> RoomChanged;
		/// <summary>
		/// Notifies a room change
		/// </summary>
		/// <param name="room">The new room</param>
		public static void OnRoomChanged(int room) => RoomChanged(room);

		/// <summary>
		/// Triggered when a level was finished
		/// </summary>
		public static event Action<GameState> LevelFinished;
		/// <summary>
		/// Notifies that a level has been finished
		/// </summary>
		/// <param name="state">The new game state</param>
		public static void OnLevelFinished(GameState state) => LevelFinished(state);

		/// <summary>
		/// Triggered when the playtime tracker ticks
		/// </summary>
		public static event Action PlaytimeTicked;
		/// <summary>
		/// Notifies that the playtime tracker has ticked
		/// </summary>
		public static void OnPlaytimeTicked() => PlaytimeTicked();


		/// <summary>
		/// Triggered when a battery was collected
		/// </summary>
		public static event Action BatteryCollected;
		/// <summary>
		/// Notifiy that a battery has been collected
		/// </summary>
		public static void OnBatteryCollected() => BatteryCollected();

		/// <summary>
		/// Triggered when a light gets activated
		/// </summary>
		public static event Action LightActivated;
		/// <summary>
		/// Notifies that a light has been activated
		/// </summary>
		public static void OnLightActivated() => LightActivated();

		/// <summary>
		/// Triggered when a card was collected
		/// </summary>
		public static event Action<PowerUpCard> CardCollected;
		/// <summary>
		/// Notifies that a cart has been activated
		/// </summary>
		/// <param name="card"></param>
		public static void OnCardCollected(PowerUpCard card) => CardCollected(card);

		/// <summary>
		/// Triggered when a card gets consumed
		/// </summary>
		public static event Action<PowerUpCard> CardConsumed;
		/// <summary>
		/// Notifies that a card has been consumed
		/// </summary>
		/// <param name="card"></param>
		public static void OnCardConsumed(PowerUpCard card) => CardConsumed(card);

		/// <summary>
		/// Triggered when the current card has changed
		/// </summary>
		public static event Action<PowerUpCard> CurrentCardChanged;
		/// <summary>
		/// Notifies that the current card has changed
		/// </summary>
		/// <param name="card"></param>
		public static void OnCurrentCardChanged(PowerUpCard card) => CurrentCardChanged(card);


		///<summary>
		/// Triggered when the title screen has been opened
		///</summary>
		public static event Action TitleScreenRequested;
		///<summary>
		/// Notifies that the title screen should be opened
		///</summary>
		public static void OnTitleScreenRequested() => TitleScreenRequested();

		///<summary>
		/// Triggered when the file select has should be opened
		///</summary>
		public static event Action FileSelectOpened;
		///<summary>
		/// Notifies that the file select should be opened
		///</summary>
		public static void OnFileSelectOpened() => FileSelectOpened();

		///<summary>
		/// Triggered when the save file select has been opened
		///</summary>
		public static event Action<GameState> SaveSelected;
		///<summary>
		/// Notifies that the save file select should be opened
		///</summary>
		public static void OnSaveSelected(GameState state) => SaveSelected(state);

		///<summary>
		/// Triggered when the world select has been opened
		///</summary>
		public static event Action<GameState> WorldSelected;
		///<summary>
		/// Notifies that the world select should be opened
		///</summary>
		public static void OnWorldSelected(GameState state) => WorldSelected(state);

		///<summary>
		/// Triggered when the level select has been opened
		///</summary>
		public static event Action<GameState> LevelSelected;
		///<summary>
		/// Notifies that the level select should be opened
		///</summary>
		public static void OnLevelSelected(GameState state) => LevelSelected(state);

		static PersistentEvents()
		{
			Clear();
		}

		/// <summary>
		/// Clears all subscribers from all events.
		/// </summary>
		private static void Clear()
		{
			QuitRequested = delegate { };
			PlayerDied = delegate { };
			PauseRequested = delegate { };
			ResumeRequested = delegate { };
			GameSceneChangeRequested = delegate { };
			LevelChangeRequested = delegate { };
			RoomChanged = delegate { };
			LevelFinished = delegate { };
			PlaytimeTicked = delegate { };

			BatteryCollected = delegate { };
			LightActivated = delegate { };
			CardCollected = delegate { };
			CardConsumed = delegate { };
			CurrentCardChanged = delegate { };

			TitleScreenRequested = delegate { };
			FileSelectOpened = delegate { };
			SaveSelected = delegate { };
			WorldSelected = delegate { };
			LevelSelected = delegate { };
		}
	}
}