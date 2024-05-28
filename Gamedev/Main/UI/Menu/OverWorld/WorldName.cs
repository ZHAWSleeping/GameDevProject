using Godot;
using System;

namespace Gamedev.Main.UI.Menu
{
	public partial class WorldName : Label
	{
		[Export]
		private string Prefix;

		private int _world;
		public int World
		{
			set
			{
				_world = value;
				Text = $"{Prefix} {value}";
			}
			get
			{
				return _world;
			}
		}
	}
}