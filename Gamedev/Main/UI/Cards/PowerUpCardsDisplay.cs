using Gamedev.Main.Events;
using Gamedev.Main.Objects.Cards;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PowerUpCardsDisplay : Control
{
	private const int MaxCards = 3;

	[Export]
	private Godot.Collections.Array<Texture2D> Cards;

	[Export]
	private float CardOffset = 30;

	private LinkedList<TextureRect> CardRects = new();

	public override void _Ready()
	{
		// Clear editor rects if necessary
		GetChildren().OfType<TextureRect>().ToList().ForEach(node => node.QueueFree());
		CollisionEvents.CardCollected += AddCard;
		CollisionEvents.CardConsumed += RemoveLastCard;
	}

	/// <summary>
	/// Adds a new card to the top of the hand
	/// If the hand limit would be exceeded, removes the bottom card
	/// </summary>
	/// <param name="card"></param>
	private void AddCard(PowerUpCard card)
	{
		if (CardRects.Count() >= MaxCards)
		{
			RemoveFirstCard();
		}

		TextureRect newCard = new TextureRect();
		newCard.Texture = Cards[(int)card.CardType];
		newCard.Position = new(CardOffset * CardRects.Count(), 400);
		if (CardRects.Any())
		{
			CardRects.Last().AddSibling(newCard);
		}
		else
		{
			AddChild(newCard);
		}
		CardRects.AddLast(newCard);
		GainCardAnimation(newCard);
	}

	/// <summary>
	/// Removes the first (top) card from the hand and plays an animation
	/// </summary>
	private void RemoveFirstCard()
	{
		TextureRect oldCard = CardRects.First();
		CardRects.RemoveFirst();
		LoseCardAnimation(oldCard);
		foreach (var card in CardRects)
		{
			LeftShiftAnimation(card);
		}
	}

	/// <summary>
	/// Removes the last (bottom) card from the hand and plays an animation
	/// </summary>
	/// <param name="_"></param>
	private void RemoveLastCard(PowerUpCard _)
	{
		TextureRect oldCard = CardRects.Last();
		CardRects.RemoveLast();
		ConsumeCardAnimation(oldCard);
	}

	/// <summary>
	/// Consumes then removes the first (top) card and plays an animation
	/// </summary>
	/// <param name="card"></param>
	private void ConsumeCardAnimation(TextureRect card)
	{
		Tween tween = card.CreateTween();
		tween.SetParallel(true);
		tween.TweenProperty(
			card,
			TextureRect.PropertyName.Scale.ToString(),
			new Vector2(0, 0),
			0.5f
		).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
		tween.TweenProperty(
			card,
			TextureRect.PropertyName.Position.ToString(),
			card.Position + card.Size / 2,
			0.5f
		).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
		tween.SetParallel(false);
		tween.TweenCallback(Callable.From(() => card.QueueFree()));
	}

	/// <summary>
	/// Shifts a card to the left by the set offset
	/// </summary>
	/// <param name="card"></param>
	private void LeftShiftAnimation(TextureRect card)
	{
		Tween tween = card.CreateTween();
		tween.TweenProperty(
			card,
			TextureRect.PropertyName.Position.ToString(),
			new Vector2(card.Position.X - CardOffset, card.Position.Y),
			0.25f
		).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
	}

	/// <summary>
	/// Animates the loss of the last (bottom) card.
	/// </summary>
	/// <param name="card"></param>
	private void LoseCardAnimation(TextureRect card)
	{
		Tween tween = card.CreateTween();
		tween.SetParallel(true);
		tween.TweenProperty(
			card,
			TextureRect.PropertyName.Position.ToString(),
			new Vector2(card.Position.X - CardOffset, card.Position.Y),
			0.25f
		).SetEase(Tween.EaseType.InOut);
		tween.TweenProperty(
			card,
			TextureRect.PropertyName.Modulate.ToString(),
			Colors.Transparent,
			0.25f
		).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
		tween.SetParallel(false);
		tween.TweenCallback(Callable.From(() => card.QueueFree()));
	}

	/// <summary>
	/// Animates the gain of the first (top) card.
	/// </summary>
	/// <param name="card"></param>
	private void GainCardAnimation(TextureRect card)
	{
		Tween tween = card.CreateTween();
		tween.TweenProperty(
			card,
			TextureRect.PropertyName.Position.ToString(),
			new Vector2(card.Position.X, 0),
			0.25f
		).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);

	}
}
