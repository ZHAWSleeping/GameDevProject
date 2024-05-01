using System.Collections.Generic;
using System.Linq;
using Gamedev.Main.Events;
using Gamedev.Main.Objects.Cards;

namespace Gamedev.Main.Characters.Player
{
	public class CardInventory
	{
		private const int CardLimit = 3;
		private LinkedList<PowerUpCard> Cards = new();

		public CardInventory()
		{
			CollisionEvents.CardCollected += Add;
		}

		public void Add(PowerUpCard card)
		{
			if (Cards.Count() >= CardLimit)
			{
				Cards.RemoveFirst();
			}
			Cards.AddLast(card);
		}

		public PowerUpCard Consume()
		{
			var card = Cards.LastOrDefault(new PowerUpCard(PowerUpCard.Type.Invalid, PlayerFSM.State.Invalid));
			if (card.CardType != PowerUpCard.Type.Invalid)
				Cards.RemoveLast();
			return card;
		}
	}
}