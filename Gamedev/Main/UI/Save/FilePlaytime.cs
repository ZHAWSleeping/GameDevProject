using Godot;
using System;

public partial class FilePlaytime : Label
{
	private long _time;
	public long Time
	{
		set
		{
			_time = 1000 / (int)ProjectSettings.GetSetting("physics/common/physics_ticks_per_second") * value;
			Text = TimeTracker.DisplayTicks(_time);
		}
		get
		{
			return _time;
		}
	}
}
