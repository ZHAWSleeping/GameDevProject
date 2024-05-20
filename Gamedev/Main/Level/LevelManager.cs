using Gamedev.Main.Events;
using Gamedev.Main.Persistent;
using Godot;
using System;
using System.Linq;

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


	// Called when the node enters the scene tree for the first time.
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

	private bool IsInBounds(int world, int level)
	{
		return world >= 0
			&& world < Levels.Length
			&& level >= 0
			&& level < Levels[world].Length;
	}

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
		PersistentEvents.OnGameSceneChangeRequested(Levels[State.CurrentWorld][State.CurrentLevel]);
		DebugEvents.OnRoomChanged(this);
	}

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
		PersistentEvents.OnWorldSelected(State);
		DebugEvents.OnRoomChanged(this);
	}

	private void ChangeRoom(int room)
	{
		State.CurrentRoom = room;
		DebugEvents.OnRoomChanged(this);
	}

	public void Tick()
	{
		State.File.PlaytimeTicks += 1;
	}

	public void Die()
	{
		State.File.Deaths += 1;
	}
}
