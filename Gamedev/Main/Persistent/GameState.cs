namespace Gamedev.Main.Persistent
{
	public record struct GameState
	{
		public SaveFile File;
		public int CurrentWorld;
		public int CurrentLevel;
		public int CurrentRoom;
	}
}