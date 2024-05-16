using Gamedev.Main.Events;
using Gamedev.Main.Persistent;
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


	public GameState State
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

	private GameState _file;

	private Tween ColorTween;


	public override void _Ready()
	{
		base._Ready();
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

	private void UpdateChildren(GameState state)
	{
		WorldProgress.Generate(state.File.CompletedLevels);
		PlayerProfile.HasSaveData = state.File.IsStarted;
		FileName.Slot = state.File.Slot;
		DeathCount.Count = state.File.Deaths;
		Playtime.Time = state.File.PlaytimeTicks;
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
		if (State.CurrentWorld != -1 && State.CurrentLevel != -1 && State.CurrentRoom != -1)
		{
			PersistentEvents.OnLevelSelected(State);
		}
		else
		{
			PersistentEvents.OnSaveSelected(State);
		}
	}
}
