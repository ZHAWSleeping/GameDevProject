using Gamedev.Main.Characters;
using Gamedev.Main.Events;
using Godot;
using System;

namespace Gamedev.Main.Characters
{
	public partial class EnemyTrigger : Area2D
	{
		/// <summary>
		/// Trigger for when the palyer gets caught by an enemy.
		/// </summary>
		public override void _Ready()
		{
			BodyEntered += (_) => CollisionEvents.OnCollisionDeath();
		}
	}
}