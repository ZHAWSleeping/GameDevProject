using System;
using Godot;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Events
{
	public static class StateEvents
	{
		public static event Action QuitRequested;
		public static void OnQuitRequested() => QuitRequested();

		public static event Action RestartRequested;
		public static void OnRestartRequested() => RestartRequested();

		public static event Action PauseRequested;
		public static void OnPauseRequested() => PauseRequested();

		public static event Action ResumeRequested;
		public static void OnResumeRequested() => ResumeRequested();

		public static event Action<PackedScene> MainMenuRequest;
		public static void OnMainMenuRequest(PackedScene packedScene) => MainMenuRequest(packedScene);

		public static event Action<PackedScene> SceneChangeRequested;
		public static void OnSceneChangeRequested(PackedScene scene) => SceneChangeRequested(scene);

		public static event Action<PackedScene> LevelChangeRequested;
		public static void OnLevelChangeRequested(PackedScene scene) => LevelChangeRequested(scene);


		public static event Action<PackedScene> LevelFinished;
		public static void OnLevelFinished(PackedScene packedScene) => LevelFinished(packedScene);

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
			PauseRequested = delegate { };
			ResumeRequested = delegate { };
			MainMenuRequest = delegate { };
			LevelFinished = delegate { };
			HeadbandAnchorMoved = delegate { };
		}
	}
}