using Godot;
using System;

public partial class DebugLabel : HBoxContainer
{
	[Export]
	private string LabelName;

	[Export]
	private Label NameLabel;

	[Export]
	protected Label DataLabel;

	public override void _Ready()
	{
		base._Ready();
		NameLabel.Text = LabelName;
	}
}
