using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace Gamedev.Main.UI.Menu.Level
{
	public partial class LevelSelectPanel : PanelContainer
	{
		[Export]
		private Label Number;
		
		public int World;
		public int Level
		{
			set
			{
				_level = value;
				Number.Text = value.ToString();
			}
			get
			{
				return _level;
			}
		}
		private int _level;

	}
}
