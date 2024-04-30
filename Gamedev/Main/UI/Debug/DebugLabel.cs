using Gamedev.Main.Extensions;
using Godot;
using System;

namespace Gamedev.Main.UI.Debug
{
	public partial class DebugLabel : HBoxContainer
	{
		[Export]
		public string LabelName;

		[Export]
		private Label NameLabel;

		[Export]
		public Label DataLabel;

		public override void _Ready()
		{
			base._Ready();
			NameLabel.Text = LabelName + ":";
		}

		protected void DisplayBool(bool b)
		{
			DataLabel.Text = b ? "Y" : "N";
		}

		protected void DisplayVector2(Vector2 vec)
		{
			DataLabel.Text = $"{vec.X}, {vec.Y}";
		}

		protected void DisplayDirection(VectorExtensions.Direction? dir)
		{
			DataLabel.Text = dir?.ToString() ?? "None";
		}
	}
}
