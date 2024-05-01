using System.Collections.Generic;
using Gamedev.Main.Characters.Player;
using Gamedev.Main.Events;
using Godot;

namespace Gamedev.Main.Objects.Cards
{
	[Tool]
	public partial class PowerUpCard2D : Area2D
	{
		private readonly Dictionary<PowerUpCard.Type, PlayerFSM.State> TypeMap = new()
		{
			[PowerUpCard.Type.Dash] = PlayerFSM.State.Dashing,
			[PowerUpCard.Type.DoubleJump] = PlayerFSM.State.DoubleJumping,
			[PowerUpCard.Type.Stomp] = PlayerFSM.State.Stomping,
		};

		[Export]
		private AnimatedSprite2D Sprite;

		[Export]
		private PowerUpCard.Type CardType
		{
			get
			{
				return _cardType;
			}
			set
			{
				_cardType = value;
				if (Sprite != null)
					Sprite.Frame = (int)value;
			}
		}

		private PowerUpCard.Type _cardType;

		[Export]
		private float BobbingDuration = 1.0f;

		[Export]
		private float BobbingDistance = 3;

		[Export]
		private float CollectDuration = 0.5f;

		private Vector2 OriginalPosition;

		public override void _Ready()
		{
			base._Ready();
			OriginalPosition = Position;
			if (!Engine.IsEditorHint())
			{
				BodyEntered += Collected;

				Tween tween = CreateTween();
				tween.TweenProperty(
					this,
					PropertyName.Position.ToString(),
					new Vector2(OriginalPosition.X, OriginalPosition.Y - BobbingDistance),
					BobbingDuration)
					.SetEase(Tween.EaseType.InOut);

				tween.TweenProperty(
					this,
					PropertyName.Position.ToString(),
					OriginalPosition,
					BobbingDuration)
					.SetEase(Tween.EaseType.InOut);
				tween.SetLoops();
			}

		}

		private void Collected(object _)
		{
			BodyEntered -= Collected;
			CollisionEvents.OnCardCollected(new PowerUpCard(CardType, TypeMap[CardType]));
			if (!Engine.IsEditorHint())
			{
				// Spinning animation
				Tween scaleTween = CreateTween();
				float spinSpeed = 0.2f;
				scaleTween.TweenProperty(
					this,
					PropertyName.Scale.ToString(),
					new Vector2(-1, Scale.Y),
					spinSpeed
				).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
				scaleTween.TweenProperty(
					this,
					PropertyName.Scale.ToString(),
					new Vector2(1, Scale.Y),
					spinSpeed
				).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
				scaleTween.SetLoops();

				// Disappear animation
				Tween positionTween = CreateTween();
				positionTween.TweenProperty(
					this,
					PropertyName.Position.ToString(),
					new Vector2(Position.X, Position.Y - 20),
					0.25f
				).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
				positionTween.TweenProperty(
					this,
					PropertyName.Position.ToString(),
					new Vector2(Position.X, Position.Y + 200),
					CollectDuration
				).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
				positionTween.TweenCallback(Callable.From(() => QueueFree()));
			}
		}
	}
}