using Gamedev.Main.Audio;
using Gamedev.Main.Events;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

namespace Gamedev.Main.UI.Pause
{
	public partial class PauseScreen : Control
	{
		[Export]
		private ScrollableVBox Menu;

		[Export]
		private AudioStreamPlayer Audio;

		private bool Paused = false;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			PersistentEvents.PauseRequested += Pause;
			PersistentEvents.ResumeRequested += Resume;
			Menu.RefreshChildren();
		}



		public override void _Input(InputEvent @event)
		{
			base._Input(@event);
			if (@event.IsActionPressed("Pause"))
			{
				if (Paused)
				{
					PersistentEvents.OnResumeRequested();
				}
				else if (LevelManager.Instance.InLevel)
				{
					PersistentEvents.OnPauseRequested();
				}
			}

			if (@event.IsActionPressed("Cancel") && Paused)
			{
				PersistentEvents.OnResumeRequested();
			}
		}

		private void Pause()
		{
			Tween tween = CreateTween();
			tween.TweenProperty(this, PropertyName.Modulate.ToString(), Colors.White, 0.25f);
			Menu.ProcessMode = ProcessModeEnum.Inherit;
			Audio.PitchScale = 1.0f;
			PersistentAudioEvents.OnAudioRequested(GlobalAudioManager.Sound.MenuAccept);
			Paused = !Paused;
		}

		private void Resume()
		{
			Tween tween = CreateTween();
			tween.TweenProperty(this, PropertyName.Modulate.ToString(), Colors.Transparent, 0.25f);
			Menu.ProcessMode = ProcessModeEnum.Disabled;
			Audio.PitchScale = 1.5f;
			PersistentAudioEvents.OnAudioRequested(GlobalAudioManager.Sound.MenuBack);
			Paused = !Paused;
		}
	}
}
