using Godot;
using System;

public partial class Profile : TextureRect
{
	[Export]
	private Texture2D SavedTexture;

	[Export]
	private Texture2D UnsavedTexture;

	public bool HasSaveData
	{
		set
		{
			_hasSaveData = value;
			Texture = value ? SavedTexture : UnsavedTexture;
		}
		get
		{
			return _hasSaveData;
		}
	}
	private bool _hasSaveData;
}
