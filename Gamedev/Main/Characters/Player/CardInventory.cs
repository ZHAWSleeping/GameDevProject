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
			return Cards.LastOrDefault();
		}
	}
}