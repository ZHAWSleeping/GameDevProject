using System.Collections.Generic;
using System.Linq;
using Gamedev.Main.Events;
using Gamedev.Main.Objects.Cards;
using Godot;

namespace Gamedev.Main.Characters.Players
{
	/// <summary>
	/// Inventory for how many and what cards the player is holding.
	/// Max amount can be set/altered.
	/// If a pickup would exceed the max amount, the oldest card is removed instead.
	/// Functions as a STACK!
	/// </summary>
	public class CardInventory
	{
		public const int CardLimit = 3;
		private LinkedList<PowerUpCard> Cards = new();

		public CardInventory()
		{
			CollisionEvents.CardCollected += Add;
		}

		/// <summary>
		/// Adds a given card to the palyers inventory.
		/// Card is added ontop of the current card stack.
		/// If card limit would be exceed then the oldest card is removed.
		/// </summary>
		/// <param name="card"></param> the card type to be added to the inventory
		public void Add(PowerUpCard card)
		{
			if (Cards.Count() >= CardLimit)
			{
				Cards.RemoveFirst();
			}
			Cards.AddLast(card);
			CollisionEvents.OnCurrentCardChanged(Current());
			PersistentEvents.OnCurrentCardChanged(Current());
		}

		public PowerUpCard Consume()
		{
			var card = Current();
			if (card.CardType != PowerUpCard.Type.Invalid)
				Cards.RemoveLast();
			CollisionEvents.OnCurrentCardChanged(Current());
			PersistentEvents.OnCurrentCardChanged(Current());
			return card;
		}

		public PowerUpCard Current()
		{
			return Cards.LastOrDefault(new PowerUpCard(PowerUpCard.Type.Invalid, PlayerFSM.State.Invalid, Colors.Pink));
		}
	}
}