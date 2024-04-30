using System;
using Godot;

namespace Gamedev.Events
{
	public static class StateEvents
	{
		public static event Action QuitRequested = delegate { };
		public static void OnQuitRequested() => QuitRequested();

		public static event Action RestartRequested = delegate { };
		public static void OnRestartRequested() => RestartRequested();

		public static event Action PauseRequested = delegate { };
		public static void OnPauseRequested() => PauseRequested();

		public static event Action ResumeRequested = delegate { };
		public static void OnResumeRequested() => ResumeRequested();

		public static event Action<PackedScene> SceneChangeRequest = delegate { };
		public static void OnSceneChangeRequest(PackedScene packedScene) => SceneChangeRequest(packedScene);

		public static event Action<PackedScene> LevelChangeRequest = delegate { };
		public static void OnLevelChangeRequest(PackedScene packedScene) => LevelChangeRequest(packedScene);

		public static event Action<PackedScene> LevelFinished = delegate { };
		public static void OnLevelFinished(PackedScene packedScene) => LevelFinished(packedScene);


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
		}
	}
}