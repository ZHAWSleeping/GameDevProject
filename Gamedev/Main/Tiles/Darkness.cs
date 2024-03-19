using Godot;
using System;

namespace Gamedev.Main.Tiles
{
    public partial class Darkness : CanvasModulate
    {
        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            Color = Colors.Black;
        }
    }
}