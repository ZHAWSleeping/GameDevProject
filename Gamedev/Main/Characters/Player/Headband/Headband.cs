using Gamedev.Main.Constants;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
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
	private Color DefaultColor = Colors.Magenta;

	[Export]
	public Sprite2D Sprite { get; private set; }


	[Export]
	public RigidBody2D Body { get; private set; }

	public override void _Ready()
	{
		Color = DefaultColor;
		NodePaths.Headband = this;
		CollisionEvents.CollisionDeath += () => this.SetProcessModeDeferred(ProcessModeEnum.Disabled);
		CollisionEvents.CurrentCardChanged += card =>
		{
			if (card.CardType == Gamedev.Main.Objects.Cards.PowerUpCard.Type.Invalid)
			{
				Color = DefaultColor;
			}
			else
			{
				Color = card.CardColor;
			}
		};
	}
}
