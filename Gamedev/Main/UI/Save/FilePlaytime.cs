using Godot;
using System;

public partial class FilePlaytime : Label
{
	private int _time;
	public int Time
	{
		set
		{
			_time = value;
			Text = $"{Math.Truncate(TimeSpan.FromMilliseconds(value).TotalHours)}:{Math.Truncate(TimeSpan.FromMilliseconds(value).TotalMinutes % 60):00}:{Math.Truncate(TimeSpan.FromMilliseconds(value).TotalSeconds % 60):00}.{value % 1000:000}";
		}
		get
		{
			return _time;
		}
	}
}
