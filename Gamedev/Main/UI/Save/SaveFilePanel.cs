using Gamedev.Main.Events;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;
using System.Linq;

public partial class SaveFilePanel : PanelContainer, Selectable
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


	[Export]
	private Color PrimaryColor;

	[Export]
	private Color SecondaryColor;

	[Export]
	private Color DefaultColor;

	[Export]
	private StyleBoxFlat StyleBox;


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

	private Tween ColorTween;


	public override void _Ready()
	{
		base._Ready();
		GD.Print(StyleBox.BorderColor);
		ColorTween = CreateTween();
		ColorTween.TweenProperty(
			StyleBox,
			StyleBoxFlat.PropertyName.BorderColor.ToString(),
			PrimaryColor,
			0.1f
		);
		ColorTween.TweenProperty(
			StyleBox,
			StyleBoxFlat.PropertyName.BorderColor.ToString(),
			SecondaryColor,
			0.1f
		);
		ColorTween.SetLoops();
		AddThemeStyleboxOverride("panel", StyleBox);
	}

	private void UpdateChildren(SaveFile file)
	{
		WorldProgress.Generate(file.CompletedLevels);
		PlayerProfile.HasSaveData = file.CompletedLevels.Any();
		FileName.Slot = file.Slot;
		DeathCount.Count = file.Deaths;
		Playtime.Time = file.PlaytimeMsecs;
	}

	public void Focus()
	{
		ColorTween.Play();
		Vector2 position = Position;
		Vector2 new_position = position;
		Tween bounceTween = CreateTween();

		new_position.Y += 4;
		bounceTween.TweenProperty(this, PropertyName.Position.ToString(), new_position, 0.1f);

		new_position.Y = position.Y - 2;
		bounceTween.TweenProperty(this, PropertyName.Position.ToString(), new_position, 0.1f);
		bounceTween.TweenProperty(this, PropertyName.Position.ToString(), position, 0.1f);

		// TODO: Add Audios
		//SelectAudio.Play();
	}

	public void Unfocus()
	{
		ColorTween.Pause();
		StyleBox.BorderColor = DefaultColor;
	}

	public void Trigger()
	{
		PersistentEvents.OnSaveSelected(File);
	}
}
