using Godot;
using System;

namespace Gamedev.Main.UI.Save
{
	public partial class DeathCount : Label
	{
		public int Count
		{
			set
			{
				_count = value;
				Text = value.ToString();
			}
			get
			{
				return _count;
			}
		}
		private int _count;
	}
}