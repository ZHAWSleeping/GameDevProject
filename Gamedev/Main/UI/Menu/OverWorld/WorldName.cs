using Godot;
using System;

namespace Gamedev.Main.UI.Menu
{
	/// <summary>
	/// Label for a a world in the world selector.
	/// </summary>
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