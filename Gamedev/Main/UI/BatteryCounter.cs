using Gamedev.Events;
using Gamedev.Main.Events;
using Godot;
using System;

public partial class BatteryCounter : HBoxContainer
{


	[Export]
	private string LabelName;

	[Export]
	private Label NameLabel;

	[Export]
	protected Label DataLabel;

	private int BatteryCount = 0;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CollisionEvents.BatteryCollected += BatteryCollected;
		CollisionEvents.ActivateLight += BatteryUsed;
		NameLabel.Text = "Battery Count";
		DataLabel.Text = $"{BatteryCount}";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	private void BatteryCollected()
	{
		BatteryCount++;
		DataLabel.Text = $"{BatteryCount}";
	}

	private void BatteryUsed()
	{
		BatteryCount--;
		DataLabel.Text = $"{BatteryCount}";
	}
}
