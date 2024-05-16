using Godot;
using System;

public partial class FilePlaytime : Label
{
	private double _time;
	public double Time
	{
		set
		{
			_time = 1000 / (int)ProjectSettings.GetSetting("physics/common/physics_ticks_per_second") * value;
			Text = $"{Math.Truncate(TimeSpan.FromMilliseconds(_time).TotalHours)}:{Math.Truncate(TimeSpan.FromMilliseconds(_time).TotalMinutes % 60):00}:{Math.Truncate(TimeSpan.FromMilliseconds(_time).TotalSeconds % 60):00}.{_time % 1000:000}";
		}
		get
		{
			return _time;
		}
	}
}
