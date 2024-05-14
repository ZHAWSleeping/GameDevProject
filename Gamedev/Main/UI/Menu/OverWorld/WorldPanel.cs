using Gamedev.Main.Events;
using Gamedev.Main.Peristent;
using Gamedev.Main.UI.Scrollable;
using Godot;
using System;

public partial class WorldPanel : PanelContainer, Selectable
{

	private const float Duration = 0.1f;
	private const float Offset = 20;
	private Tween Animation;
	private Vector2? OriginalPosition = null;

	private SaveFile File;
	private int World;

	[Export]
	private WorldName WorldName;

	[Export]
	private WorldIcon WorldIcon;

	[Export]
	private WorldCompletion WorldCompletion;

	private void JiggleAnimation()
	{
		if (OriginalPosition == null)
		{
			OriginalPosition = Position;
		}

		if (Animation != null)
			Animation.Stop();

		Vector2 position = (Vector2)OriginalPosition;
		Vector2 new_position = position;
		Animation = CreateTween();

		new_position.X += Offset;
		Animation.TweenProperty(this, PropertyName.Position.ToString(), new_position, Duration);

		new_position.X = position.X - Offset / 2;
		Animation.TweenProperty(this, PropertyName.Position.ToString(), new_position, Duration);
		Animation.TweenProperty(this, PropertyName.Position.ToString(), position, Duration);
	}

	public void Focus()
	{
		JiggleAnimation();
	}

	public void Unfocus() { }

	public void Trigger()
	{
		PersistentEvents.OnWorldSelected(File, World);
	}

	public void UpdateChildren(SaveFile file, int world)
	{
		File = file;
		WorldName.World = file.World;
		WorldCompletion.Levels = file.CompletedLevels[world];
	}
}
