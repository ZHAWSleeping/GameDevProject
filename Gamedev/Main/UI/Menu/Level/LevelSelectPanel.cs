using Gamedev.Main.Events;
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

		public bool Completed
		{
			set
			{
				_completed = value;
				Star.Modulate = value ? Colors.White : Colors.Transparent;
			}
			get
			{
				return _completed;
			}
		}
		private bool _completed;

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

		}

		public void Unfocus()
		{
			ColorTween.Pause();
			StyleBox.BorderColor = DefaultColor;
		}

		public void Trigger()
		{
			PersistentEvents.OnLevelSelected(World, Level);
		}
	}
}
