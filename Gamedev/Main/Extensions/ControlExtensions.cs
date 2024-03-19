using System.Drawing;
using Godot;

namespace Gamedev.Main.Extensions
{
	public static class ControlExtensions
	{
		public static void SetFontColor(this Control control, Godot.Color color)
		{
			control.AddThemeColorOverride("font_color", color);
		}
	}
}