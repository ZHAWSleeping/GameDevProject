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

	private void RemoveLastCard(PowerUpCard _)
	{
		TextureRect oldCard = CardRects.Last();
		CardRects.RemoveLast();
		ConsumeCardAnimation(oldCard);
	}



	private void ConsumeCardAnimation(TextureRect card)
	{
		Tween tween = card.CreateTween();
		tween.TweenProperty(
			card,
			TextureRect.PropertyName.Scale.ToString(),
			new Vector2(0, 0),
			0.5f
		).SetEase(Tween.EaseType.InOut).SetTrans(Tween.TransitionType.Cubic);
		tween.TweenCallback(Callable.From(() => card.QueueFree()));
	}

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
