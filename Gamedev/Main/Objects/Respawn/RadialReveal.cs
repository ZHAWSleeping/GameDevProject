using Gamedev.Main.Constants;
using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Objects.Respawn
{
	public partial class RadialReveal : Sprite2D
	{
		[Export]
		private float Duration = 0.6f;

		[Export]
		private float Delay = 1;

		private Tween Animation;

		public override void _Ready()
		{
			HideImmediate();
			GameStateEvents.PlayerRespawned += Show;
			GameStateEvents.PlayerDied += Hide;
		}

		private Vector2 MapToTextureSpace(Vector2 position)
		{
			return ToLocal(position) / Texture.GetSize();
		}

		public void Hide(Vector2 position)
		{
			Vector2 target = MapToTextureSpace(position);
			((GradientTexture2D)Texture).FillFrom = target;
			((GradientTexture2D)Texture).FillTo = new Vector2(2, 2);
			if (Animation != null)
			{
				Animation.Stop();
			}
			Animation = CreateTween();
			Animation.TweenProperty(
				Texture,
				GradientTexture2D.PropertyName.FillTo.ToString(),
				target - new Vector2(0.001f, 0.001f),
				Duration
			)
			.SetDelay(Delay)
			.SetEase(Tween.EaseType.InOut)
			.SetTrans(Tween.TransitionType.Cubic);
			GameStateEvents.PlayerRespawned -= Show;
			GameStateEvents.PlayerDied -= Hide;

		}

		public void HideImmediate()
		{
			((GradientTexture2D)Texture).FillTo = ((GradientTexture2D)Texture).FillFrom - new Vector2(0.001f, 0.001f);
		}

		public void Show(Vector2 position)
		{
			Vector2 target = MapToTextureSpace(position);
			((GradientTexture2D)Texture).FillFrom = target;
			((GradientTexture2D)Texture).FillTo = target - new Vector2(0.001f, 0.001f);
			if (Animation != null)
			{
				Animation.Stop();
			}
			Animation = CreateTween();
			Animation.TweenProperty(
				Texture,
				GradientTexture2D.PropertyName.FillTo.ToString(),
				new Vector2(2, 2),
				Duration
			)
			//.SetDelay(Delay)
			.SetEase(Tween.EaseType.InOut)
			.SetTrans(Tween.TransitionType.Cubic);
		}

		public void ShowImmediate()
		{
			((GradientTexture2D)Texture).FillTo = new Vector2(2, 2);
		}
	}
}