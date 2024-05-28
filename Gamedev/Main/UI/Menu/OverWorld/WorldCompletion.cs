using Godot;
using System;
using System.Linq;

namespace Gamedev.Main.UI.Menu
{
	public partial class WorldCompletion : Label
	{

		private bool[] _levels;
		public bool[] Levels
		{
			set
			{
				_levels = value;
				int max = value.Count();
				int complete = value.Sum(b => b ? 1 : 0);
				Text = $"{complete} / {max}";
			}
			get
			{
				return _levels;
			}
		}

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}