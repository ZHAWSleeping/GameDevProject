using Gamedev.Main.Peristent;
using Godot;
using System;
using System.Linq;

public partial class SaveFilePanel : PanelContainer
{
	[Export]
	private WorldProgress WorldProgress;

	[Export]
	private Profile PlayerProfile;

	[Export]
	private SaveFileName FileName;

	[Export]
	private DeathCount DeathCount;

	[Export]
	private FilePlaytime Playtime;

	public SaveFile File
	{
		set
		{
			_file = value;
			UpdateChildren(value);
		}
		get
		{
			return _file;
		}
	}

	private SaveFile _file;

	private void UpdateChildren(SaveFile file)
	{
		WorldProgress.Generate(file.CompletedLevels);
		PlayerProfile.HasSaveData = file.CompletedLevels.Any();
		FileName.Slot = file.Slot;
		DeathCount.Count = file.Deaths;
		Playtime.Time = file.PlaytimeMsecs;
	}

}
