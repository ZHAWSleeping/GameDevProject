using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Godot;
using System;

namespace Gamedev.Main.UI.Scrollable
{
	public partial class SelectableLabel : Label, Selectable
	{
		private Tween ColorTween;

		[Export]
		private Color PrimaryColor;

		[Export]
		private Color SecondaryColor;

		public override void _Ready()
		{
			ColorTween = CreateTween();
			ColorTween.TweenMethod(Callable.From((Color color) => this.SetFontColor(color)), PrimaryColor, SecondaryColor, 0.1f);
			ColorTween.TweenMethod(Callable.From((Color color) => this.SetFontColor(color)), SecondaryColor, PrimaryColor, 0.1f);
			ColorTween.SetLoops();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public void Focus()
		{
			ColorTween.Play();

			Vector2 position = Position;
			Vector2 new_position = position;
			Tween bounceTween = CreateTween();

			new_position.Y += 4;
			bounceTween.TweenProperty(this, PropertyName.Position.ToString(), new_position, 0.1f);

			new_position.Y = position.Y - 2;
			bounceTween.TweenProperty(this, PropertyName.Position.ToString(), new_position, 0.1f);
			bounceTween.TweenProperty(this, PropertyName.Position.ToString(), position, 0.1f);

			PersistentAudioEvents.OnAudioRequested(Audio.GlobalAudioManager.Sound.MenuScroll);

		}

		public void Unfocus()
		{
			ColorTween.Pause();
			this.SetFontColor(Colors.White);
		}

		public virtual void Trigger()
		{
			PersistentAudioEvents.OnAudioRequested(Audio.GlobalAudioManager.Sound.MenuAccept);
		}
	}
}
