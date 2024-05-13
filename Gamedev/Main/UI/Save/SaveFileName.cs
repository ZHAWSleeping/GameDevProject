using Godot;
using System;

public partial class SaveFileName : Label
{
	[Export]
	private string Content;

	public int Slot
	{
		set
		{
			_slot = value;
			Text = $"{Content} {value}";
		}
		get
		{
			return _slot;
		}
	}
	private int _slot;
}
