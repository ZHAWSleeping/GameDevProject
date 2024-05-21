using Gamedev.Main.Audio;
using Gamedev.Main.Events;
using Gamedev.Main.Persistent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace Gamedev.Main.UI.Menu.Level
{
	public partial class LevelSelectPanel : PanelContainer, Selectable
	{
		[Export]
		private Label Number;

		[Export]
		private StyleBoxFlat StyleBox;

		[Export]
		private Color PrimaryColor;

		[Export]
		private Color SecondaryColor;

		[Export]
		private Color DefaultColor;

		[Export]
		private Control Star;

		public int World { get => State.CurrentWorld; }
		public int Level { get => State.CurrentLevel; }

		public bool Completed { get => State.File.CompletedLevels[World][Level]; }

		public GameState State
		{
			set
			{
				_state = value;
				Number.Text = Level.ToString();
				Star.Modulate = Completed ? Colors.White : Colors.Transparent;
			}
			get
			{
				return _state;
			}
		}
		private GameState _state;

		private Tween ColorTween;

		private Vector2? OriginalPosition = null;

		public override void _Ready()
		{
			base._Ready();
			ColorTween = CreateTween();
			ColorTween.TweenProperty(
				StyleBox,
				StyleBoxFlat.PropertyName.BorderColor.ToString(),
				PrimaryColor,
				0.1f
			);
			ColorTween.TweenProperty(
				StyleBox,
				StyleBoxFlat.PropertyName.BorderColor.ToString(),
				SecondaryColor,
				0.1f
			);
			ColorTween.SetLoops();
			AddThemeStyleboxOverride("panel", StyleBox);
		}

		public void Focus()
		{
			if (OriginalPosition == null)
			{
				OriginalPosition = Position;
			}
			ColorTween.Play();
			Vector2 position = (Vector2)OriginalPosition;
			Vector2 new_position = position;
			Tween bounceTween = CreateTween();

			new_position.Y += 4;
			bounceTween.TweenProperty(this, PropertyName.Position.ToString(), new_position, 0.1f);

			new_position.Y = position.Y - 2;
			bounceTween.TweenProperty(this, PropertyName.Position.ToString(), new_position, 0.1f);
			bounceTween.TweenProperty(this, PropertyName.Position.ToString(), position, 0.1f);

			PersistentAudioEvents.OnAudioRequested(GlobalAudioManager.Sound.MenuScroll);

		}

		public void Unfocus()
		{
			ColorTween.Pause();
			StyleBox.BorderColor = DefaultColor;
		}

		public void Trigger()
		{
			PersistentAudioEvents.OnAudioRequested(GlobalAudioManager.Sound.MenuAccept);
			PersistentEvents.OnLevelSelected(State);
		}
	}
}
