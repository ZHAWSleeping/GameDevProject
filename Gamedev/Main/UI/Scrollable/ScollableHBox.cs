using Gamedev.Main.Extensions;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.UI.Scrollable
{
	public partial class ScrollableHBox : HBoxContainer
	{
		private List<Selectable> Items = new();
		private int Selected = 0;
		private bool Released = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			Items = GetChildren().OfType<Selectable>().ToList();
			Items.Skip(1).ToList().ForEach(item => item.Unfocus());
		}

		public override void _PhysicsProcess(double delta)
		{
			float input = InputExtensions.MovementVector().X;
			if (input == 1 && Selected + 1 < Items.Count && Released)
			{
				Items[Selected].Unfocus();
				Selected++;
				Items[Selected].Focus();
				Released = false;
			}
			else if (input == -1 && Selected - 1 >= 0 && Released)
			{
				Items[Selected].Unfocus();
				Selected--;
				Items[Selected].Focus();
				Released = false;
			}
			else if (input == 0)
			{
				Released = true;
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