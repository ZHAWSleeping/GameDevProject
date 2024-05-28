using Gamedev.Main.Persistent;
using Godot;
using System;
using System.Linq;

namespace Gamedev.Main.UI.Save
{
	public partial class WorldProgress : HBoxContainer
	{
		[Export]
		private PackedScene CompleteWorldScene;

		[Export]
		private PackedScene IncompleteWorldScene;

		public void Generate(bool[][] CompletedLevels)
		{
			foreach (var World in CompletedLevels)
			{
				if (World.All(b => b) && World.Any())
				{
					AddChild(CompleteWorldScene.Instantiate());
				}
				else
				{
					AddChild(IncompleteWorldScene.Instantiate());
				}
			}

		}
	}
}