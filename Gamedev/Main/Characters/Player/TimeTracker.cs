using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Characters.Players
{
	/// <summary>
	/// Tracks how long the palyer has played on this specific save.
	/// </summary>
	public partial class TimeTracker : Node
	{
		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			PersistentEvents.OnPlaytimeTicked();
		}

		/// <summary>
		/// Truncates milliseconds played into hours, minutes, and seconds.
		/// </summary>
		/// <param name="time"></param> 
		/// <returns></returns>
		public static string DisplayTicks(long time)
		{
			double msec = 1000 / (int)ProjectSettings.GetSetting("physics/common/physics_ticks_per_second") * time;
			return $"{Math.Truncate(TimeSpan.FromMilliseconds(msec).TotalHours)}:{Math.Truncate(TimeSpan.FromMilliseconds(msec).TotalMinutes % 60):00}:{Math.Truncate(TimeSpan.FromMilliseconds(msec).TotalSeconds % 60):00}.{msec % 1000:000}";
		}
	}
}