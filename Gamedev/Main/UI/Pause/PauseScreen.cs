using Gamedev.Events;
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
            StateEvents.PauseRequested += Pause;
            StateEvents.ResumeRequested += Resume;
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }

        public override void _Input(InputEvent @event)
        {
            base._Input(@event);
            if (@event.IsActionPressed("Pause"))
            {
                if (Paused)
                {
                    StateEvents.OnResumeRequested();
                }
                else
                {
                    StateEvents.OnPauseRequested();
                }
            }

            if (@event.IsActionPressed("Cancel") && Paused)
            {
                StateEvents.OnResumeRequested();
            }
        }

        private void Pause()
        {
            Tween tween = CreateTween();
            tween.TweenProperty(this, PropertyName.Modulate.ToString(), Colors.White, 0.25f);
            Menu.ProcessMode = ProcessModeEnum.Inherit;
            Audio.PitchScale = 1.0f;
            Audio.Play();
            Paused = !Paused;
        }

        private void Resume()
        {
            Tween tween = CreateTween();
            tween.TweenProperty(this, PropertyName.Modulate.ToString(), Colors.Transparent, 0.25f);
            Menu.ProcessMode = ProcessModeEnum.Disabled;
            Audio.PitchScale = 1.5f;
            Audio.Play();
            Paused = !Paused;
        }
    }
}
