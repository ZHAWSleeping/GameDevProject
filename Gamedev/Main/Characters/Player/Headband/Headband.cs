using Gamedev.Events;
using Gamedev.Main.Constants;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Gamedev.Main.Extensions.VectorExtensions;

public partial class Headband : Node2D
{
	[Export]
	public Color Color
	{
		get
		{
			return _color;
		}
		set
		{
			_color = value;
			Sprite.Modulate = value;
			Body.Modulate = value;
		}
	}
	private Color _color;

	[Export]
	public Sprite2D Sprite { get; private set; }


	[Export]
	public RigidBody2D Body { get; private set; }

	public override void _Ready()
	{
		Color = Colors.Magenta;
		NodePaths.Headband = this;
	}
}
