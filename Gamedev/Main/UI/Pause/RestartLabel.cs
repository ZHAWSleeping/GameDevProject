using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace CaveGame.Main.UI.Pause
{
	public partial class RestartLabel : SelectableLabel
	{
		public override void Trigger()
		{
			PersistentEvents.OnResumeRequested();
			CollisionEvents.OnCollisionDeath();
		}
	}

}