using Gamedev.Main.Events;
using Gamedev.Main.Persistent;
using Godot;
using System;
using System.Linq;

namespace Gamedev.Main.Levels
{
	/// <summary>
	/// A level selector from which the palyer can select what level they want to play.
	/// </summary>
	public partial class LevelManager : Node
	{
		public static LevelManager Instance { get; private set; }

		/// <summary>
		/// List of levels.
		/// </summary>
		[Export]
		private Godot.Collections.Array<PackedScene> ExportedLevels = new();

		/// <summary>
		/// List of level counts per world. We need this since Godot doesn't support typed nested arrays at the moment.
		/// </summary>
		[Export]
		private Godot.Collections.Array<int> ExportedWorlds = new();

		public PackedScene[][] Levels { get; private set; }
		public bool[][] LevelsCompleted { get; set; }

		public GameState State;

		public bool InLevel { get; private set; }


		public override void _Ready()
		{
			Instance = this;
			Levels = ExportedWorlds
				.Select(
					(w, i) => ExportedLevels.Skip(ExportedWorlds.Take(i).Sum()).Take(w).ToArray()
				)
				.ToArray();
			//Levels = ExportedLevels.Select(w => w.ToArray()).ToArray();
			LevelsCompleted = Levels.Select(w => w.Select(_ => false).ToArray()).ToArray();
			PersistentEvents.LevelChangeRequested += ChangeLevel;
			PersistentEvents.LevelFinished += MarkAsCompleted;
			PersistentEvents.RoomChanged += ChangeRoom;
			PersistentEvents.LevelSelected += ChangeLevel;
			PersistentEvents.PlaytimeTicked += Tick;
			//PersistentEvents.OnGameSceneChangeRequested(Levels[World][Level]);

		}

		/// <summary>
		/// Checks if the given level exists.
		/// </summary>
		/// <param name="world">World</param>
		/// <param name="level">Level</param>
		/// <returns></returns>
		private bool IsInBounds(int world, int level)
		{
			return world >= 0
				&& world < Levels.Length
				&& level >= 0
				&& level < Levels[world].Length;
		}


		/// <summary>
		/// Loads the current level in the specified state object.
		/// </summary>
		/// <param name="state">The state</param>
		private void ChangeLevel(GameState state)
		{
			var world = state.CurrentWorld;
			var level = state.CurrentLevel;

			if (IsInBounds(world, level))
			{
				State = state;
			}
			else if (IsInBounds(world, level - 1) && IsInBounds(world + 1, 0))
			{
				State = state;
				State.CurrentWorld += 1;
				State.CurrentLevel = 0;
			}
			else
			{
				State = state;
				State.CurrentWorld = 0;
				State.CurrentLevel = 0;
				GD.PrintErr($"Attempted to load invalid level {world}-{level}, loading default level {0}-{0}");
			}
			InLevel = true;
			PersistentEvents.OnGameSceneChangeRequested(Levels[State.CurrentWorld][State.CurrentLevel]);
			State.File.World = State.CurrentWorld;
			State.File.Level = State.CurrentLevel;
			State.File.Room = State.CurrentRoom;
			SaveManager.Save(State.File.Slot);
			DebugEvents.OnRoomChanged(this);
		}

		/// <summary>
		/// Marks the current level in the specified state as marked.
		/// </summary>
		/// <param name="state">The state</param>
		private void MarkAsCompleted(GameState state)
		{
			int world = state.CurrentWorld;
			int level = state.CurrentLevel;
			if (IsInBounds(world, level))
			{
				State.File.CompletedLevels[world][level] = true;
			}
			else
			{
				GD.PrintErr($"Attempted to complete non-existent level {world}-{level}");
			}
			InLevel = false;
			PersistentEvents.OnWorldSelected(State);
			State.CurrentRoom = -1;
			SaveManager.Save(State.File.Slot);
			DebugEvents.OnRoomChanged(this);
		}

		/// <summary>
		/// Update the current room
		/// </summary>
		/// <param name="room"></param>
		private void ChangeRoom(int room)
		{
			State.CurrentRoom = room;
			DebugEvents.OnRoomChanged(this);
		}

		/// <summary>
		/// Tick the playtime tracker
		/// </summary>
		public void Tick()
		{
			State.File.PlaytimeTicks += 1;
		}

		/// <summary>
		/// Increment the death counter
		/// </summary>
		public void Die()
		{
			State.File.Deaths += 1;
		}
	}
}