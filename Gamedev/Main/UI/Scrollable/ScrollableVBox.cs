using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Gamedev.Main.Extensions.VectorExtensions;

namespace Gamedev.Main.UI.Scrollable
{
    public partial class ScrollableVBox : VBoxContainer
    {
        private List<Selectable> Items = new();
        private int Selected = 0;

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Items = GetChildren().OfType<Selectable>().ToList();
            Items.Skip(1).ToList().ForEach(item => item.Unfocus());
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
            if (@event.IsActionPressed(Direction.South.ToString()) && Selected + 1 < Items.Count)
            {
                Items[Selected].Unfocus();
                Selected++;
                Items[Selected].Focus();
            }
            else if (@event.IsActionPressed(Direction.North.ToString()) && Selected - 1 >= 0)
            {
                Items[Selected].Unfocus();
                Selected--;
                Items[Selected].Focus();

            }

            if (@event.IsActionPressed("Accept") && Items.Any())
            {
				Items[Selected].Trigger();
            }
        }


    }

}