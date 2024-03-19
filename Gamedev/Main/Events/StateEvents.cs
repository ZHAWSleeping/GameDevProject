using System;
using Godot;

namespace Gamedev.Events
{
	public static class StateEvents {
		public static event Action QuitRequested = delegate { };
		public static void OnQuitRequested() => QuitRequested();

		public static event Action RestartRequested = delegate { };
		public static void OnRestartRequested() => RestartRequested();

		public static event Action PauseRequested = delegate { };
		public static void OnPauseRequested() => PauseRequested();

		public static event Action ResumeRequested = delegate { };
		public static void OnResumeRequested() => ResumeRequested();


		static StateEvents()
		{
			QuitRequested += () => GD.Print("Quit requested");
			RestartRequested += () => GD.Print("Restart requested");
			PauseRequested += () => GD.Print("Pause requested");
			ResumeRequested += () => GD.Print("Resume requested");
		}

		public static void Clear()
		{
			QuitRequested = delegate { };
			RestartRequested = delegate { };
			PauseRequested = delegate { };
			ResumeRequested = delegate { };
		}
	}
}