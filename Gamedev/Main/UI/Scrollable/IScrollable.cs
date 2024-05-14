using Godot;

namespace Gamedev.Main.UI.Scrollable
{
	public interface IScrollable
	{
		public Control Instance { get; set; }
		public void RefreshChildren();
	}
}