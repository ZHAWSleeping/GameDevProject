using System;
using Godot;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Events
{
	public static class StateEvents
	{
		public static event Action QuitRequested;
		public static void OnQuitRequested() => QuitRequested();

		public static event Action<Vector2> RestartRequested;
		public static void OnRestartRequested(Vector2 playerPosition) => RestartRequested(playerPosition);

		public static event Action<Vector2> PlayerRespawned;
		public static void OnPlayerRespawned(Vector2 playerPosition) => PlayerRespawned(playerPosition);

		public static event Action PauseRequested;
		public static void OnPauseRequested() => PauseRequested();

		public static event Action ResumeRequested;
		public static void OnResumeRequested() => ResumeRequested();

		public static event Action<PackedScene> MainMenuRequested;
		public static void OnMainMenuRequested(PackedScene packedScene) => MainMenuRequested(packedScene);

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

		public static event Action<int, int> LevelFinished;
		public static void OnLevelFinished(int world, int level) => LevelFinished(world, level);

		public static event Action<Vector2, Direction> HeadbandAnchorMoved;
		public static void OnHeadbandAnchorMoved(Vector2 pos, Direction dir) => HeadbandAnchorMoved(pos, dir);


		static StateEvents()
		{
			Clear();
		}

		public static void Clear()
		{
			QuitRequested = delegate { };
			RestartRequested = delegate { };
			PlayerRespawned = delegate { };
			PauseRequested = delegate { };
			ResumeRequested = delegate { };
			SceneChangeRequested = delegate { };
			GameSceneChangeRequested = delegate { };
			LevelChangeRequested = delegate { };
			LevelFinished = delegate { };
			HeadbandAnchorMoved = delegate { };
		}
	}
}