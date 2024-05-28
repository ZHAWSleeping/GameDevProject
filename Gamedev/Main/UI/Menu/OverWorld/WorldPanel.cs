using Gamedev.Main.Audio;
using Gamedev.Main.Events;
using Gamedev.Main.Persistent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace Gamedev.Main.UI.Menu
{
	/// <summary>
	/// Panel where a world can be selected.
	/// A world contains multiple levels.
	/// </summary>
	public partial class WorldPanel : PanelContainer, Selectable
	{

		private const float Duration = 0.1f;
		private const float Offset = 20;
		private Tween Animation;
		private Vector2? OriginalPosition = null;

		private GameState State;
		private int World;

		[Export]
		private WorldName WorldName;

		[Export]
		private WorldIcon WorldIcon;

		[Export]
		private WorldCompletion WorldCompletion;

		private void JiggleAnimation()
		{
			if (OriginalPosition == null)
			{
				OriginalPosition = Position;
			}

			if (Animation != null)
				Animation.Stop();

			Vector2 position = (Vector2)OriginalPosition;
			Vector2 new_position = position;
			Animation = CreateTween();

			new_position.X += Offset;
			Animation.TweenProperty(this, PropertyName.Position.ToString(), new_position, Duration);

			new_position.X = position.X - Offset / 2;
			Animation.TweenProperty(this, PropertyName.Position.ToString(), new_position, Duration);
			Animation.TweenProperty(this, PropertyName.Position.ToString(), position, Duration);

			PersistentAudioEvents.OnAudioRequested(GlobalAudioManager.Sound.MenuScroll);

		}

		public void Focus()
		{
			JiggleAnimation();
		}

		public void Unfocus() { }

		public void Trigger()
		{
			PersistentAudioEvents.OnAudioRequested(GlobalAudioManager.Sound.MenuAccept);
			PersistentEvents.OnWorldSelected(State);
		}

		public void UpdateChildren(GameState state)
		{
			State = state;
			WorldName.World = state.CurrentWorld;
			WorldCompletion.Levels = state.File.CompletedLevels[state.CurrentWorld];
		}
	}
}