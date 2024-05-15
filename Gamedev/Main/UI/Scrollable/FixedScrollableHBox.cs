using Gamedev.Main.Extensions;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.UI.Scrollable
{
	public partial class FixedScrollableHBox : HBoxContainer, IScrollable
	{
		private const float Duration = 0.3f;
		private List<Selectable> Items = new();
		private int Selected = 0;
		private bool Released = false;
		private Vector2 StartingPosition;
		private Vector2 TargetPosition;

		private float Gap;

		private Tween ShiftTween;

		public Control Instance { get => this; set { } }

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			RefreshChildren();
		}

		public override void _PhysicsProcess(double delta)
		{
			float input = InputExtensions.MovementVector().X;
			if (input == 1 && Selected + 1 < Items.Count && Released)
			{
				Items[Selected].Unfocus();
				LeftShift((Control)Items[Selected]);
				Selected++;
				Items[Selected].Focus();
				Released = false;
			}
			else if (input == -1 && Selected - 1 >= 0 && Released)
			{
				Items[Selected].Unfocus();
				RightShift((Control)Items[Selected]);
				Selected--;
				Items[Selected].Focus();
				Released = false;
			}
			else if (input == 0)
			{
				Released = true;
			}
		}

		private void ShiftAnimation(Vector2 target)
		{
			ShiftTween = CreateTween();
			ShiftTween.TweenProperty(
				this,
				PropertyName.GlobalPosition.ToString(),
				target,
				Duration
			)
			.SetEase(Tween.EaseType.Out)
			.SetTrans(Tween.TransitionType.Cubic);
		}

		private void LeftShift(Control node)
		{
			TargetPosition = new(TargetPosition.X - Gap - node.Size.X, TargetPosition.Y);
			ShiftAnimation(TargetPosition);
		}

		private void RightShift(Control node)
		{
			TargetPosition = new(TargetPosition.X + Gap + node.Size.X, TargetPosition.Y);
			ShiftAnimation(TargetPosition);
		}

		public override void _Input(InputEvent @event)
		{
			base._Input(@event);

			if (@event.IsActionPressed("Accept") && Items.Any())
			{
				Items[Selected].Trigger();
			}
		}

		public void RefreshChildren()
		{
			Items = GetChildren().OfType<Selectable>().ToList();
			Items.Skip(1).ToList().ForEach(item => item.Unfocus());
			StartingPosition = GlobalPosition;
			TargetPosition = GlobalPosition;
			Gap = GetThemeConstant("separation");
		}
	}

}