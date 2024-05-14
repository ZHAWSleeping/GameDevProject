using Godot;
using System;

public partial class WorldName : Label
{
	[Export]
	private string Prefix;

	private int _world;
	public int World
	{
		set
		{
			_world = value;
			Text = $"{Prefix} {value}";
		}
		get
		{
			return _world;
		}
	}
}
