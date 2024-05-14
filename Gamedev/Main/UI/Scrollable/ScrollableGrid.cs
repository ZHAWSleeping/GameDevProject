using Gamedev.Main.Extensions;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.UI.Scrollable
{
	public partial class ScrollableGrid : GridContainer, IScrollable
	{
		private List<Selectable> Items = new();
		private int Selected = 0;
		private bool Released = false;
		private Vector2I GridPosition = Vector2I.Zero;

		public Control Instance { get => this; set { } }

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			RefreshChildren();
		}

		public void RefreshChildren()
		{
			Items = GetChildren().OfType<Selectable>().ToList();
			Items.Skip(1).ToList().ForEach(item => item.Unfocus());
		}

		public override void _PhysicsProcess(double delta)
		{
			Vector2 input = InputExtensions.MovementVector();
			if (input.Y == 1 && Released)
			{
				Move(Vector2I.Down);
				Released = false;
			}
			else if (input.Y == -1 && Released)
			{
				Move(Vector2I.Up);
				Released = false;
			}
			else if (input.X == 1 && Released)
			{
				Move(Vector2I.Right);
				Released = false;
			}
			else if (input.X == -1 && Released)
			{
				Move(Vector2I.Left);
				Released = false;
			}
			else if (input == Vector2.Zero)
			{
				Released = true;
			}
		}

		private void Move(Vector2I movement)
		{
			Vector2I DesiredPosition = GridPosition + movement;
			int DesiredIndex = DesiredPosition.Y * Columns + DesiredPosition.X;
			if (DesiredIndex < Items.Count && DesiredIndex >= 0)
			{
				GridPosition = DesiredPosition;
				Items[Selected].Unfocus();
				Selected = DesiredIndex;
				Items[Selected].Focus();
			}
		}

		public override void _Input(InputEvent @event)
		{
			base._Input(@event);

			if (@event.IsActionPressed("Accept") && Items.Any())
			{
				Items[Selected].Trigger();
			}
		}


	}

}