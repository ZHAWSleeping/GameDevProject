using Gamedev.Main.Characters.Players;
using Gamedev.Main.Events;
using Gamedev.Main.Extensions;
using Godot;
using System;
using System.Linq;

namespace Gamedev.Main.Characters.Enemies
{
	public partial class RayCastEnemy : Node2D
	{
		[Export]
		RayCast2D rayFront;
		[Export]
		RayCast2D rayBack;
		[Export]
		ShapeCast2D rayRight;
		[Export]
		ShapeCast2D rayLeft;


		public bool groundFront;
		public bool groundBack;
		public bool wallRight;
		public bool wallLeft;

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _PhysicsProcess(double delta)
		{
			if (rayLeft.GetCollisions().OfType<Player>().Any() || rayRight.GetCollisions().OfType<Player>().Any())
			{
				this.SetProcessModeDeferred(ProcessModeEnum.Disabled);
				CollisionEvents.OnCollisionDeath();
				return;
			}
			groundFront = rayFront.IsColliding();
			groundBack = rayBack.IsColliding();

			wallRight = rayRight.IsColliding();
			wallLeft = rayLeft.IsColliding();
		}

		public bool NoGap()
		{
			return groundFront && groundBack;
		}

		public bool NoWall()
		{
			return !(wallRight || wallLeft);
		}

		public bool NoFloor()
		{
			return !groundFront && !groundBack;
		}
	}
}