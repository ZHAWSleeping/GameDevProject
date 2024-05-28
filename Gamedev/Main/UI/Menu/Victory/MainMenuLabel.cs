using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace Gamedev.Main.UI.Menu
{
	public partial class MainMenuLabel : SelectableLabel
	{
		[Export]
		private PackedScene mainMenuScene;
		public override void Trigger()
		{
			GD.Print("Main menu");
			base.Trigger();
			//PersistentEvents.OnSceneChangeRequested(mainMenuScene);
		}
	}
}