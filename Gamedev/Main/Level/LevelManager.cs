using Gamedev.Events;
using Gamedev.Main.Events;
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
	public bool[][] LevelsCompleted { get; private set; }

	public int World { get; private set; } = 0;
	public int Level { get; private set; } = 0;
	public int Room { get; private set; } = 0;


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
		StateEvents.LevelChangeRequested += ChangeLevel;
		StateEvents.LevelFinished += MarkAsCompleted;
		StateEvents.RoomChanged += ChangeRoom;
		StateEvents.OnGameSceneChangeRequested(Levels[World][Level]);
	}

	private bool IsInBounds(int world, int level)
	{
		return world >= 0
			&& world < Levels.Length
			&& level >= 0
			&& level < Levels[world].Length;
	}

	private void ChangeLevel(int world, int level)
	{
		if (IsInBounds(world, level))
		{
			World = world;
			Level = level;
		}
		else if (IsInBounds(world, level - 1) && IsInBounds(world + 1, 0))
		{
			World = world + 1;
			Level = 0;
		}
		else
		{
			World = 0;
			Level = 0;
			GD.PrintErr($"Attempted to load invalid level {world}-{level}, loading default level {World}-{Level}");
		}
		StateEvents.OnGameSceneChangeRequested(Levels[World][Level]);
		DebugEvents.OnRoomChanged(this);
	}

	private void MarkAsCompleted(int world, int level)
	{
		if (IsInBounds(world, level))
		{
			LevelsCompleted[world][level] = true;
		}
		else
		{
			GD.PrintErr($"Attempted to complete non-existent level {world}-{level}");
		}
		DebugEvents.OnRoomChanged(this);
	}

	private void ChangeRoom(int room)
	{
		Room = room;
		DebugEvents.OnRoomChanged(this);
	}
}
