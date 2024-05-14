using System.Collections.Generic;
using System.Text.Json;
using Godot;

namespace Gamedev.Main.Peristent
{
	/// <summary>
	/// Holds the data of one save file
	/// </summary>
	public class SaveFile
	{
		public int Slot { get; init; } = -1;
		public int World { get; init; } = -1;
		public int Level { get; init; } = -1;
		public int Room { get; init; } = -1;
		public int Deaths { get; init; } = 0;
		public int PlaytimeMsecs { get; init; } = 0;
		public bool[][] CompletedLevels { get; init; }
	}

	/// <summary>
	/// Handles the saving and loading of save files.
	/// </summary>
	public static class SaveManager
	{
		private const string Path = "user://user.save";
		private static readonly Dictionary<int, SaveFile> DefaultSaveFiles = new Dictionary<int, SaveFile>
		{
			{
				0, new SaveFile {
					CompletedLevels = [],
					Slot = 0,
					PlaytimeMsecs = 18853200,
				}
			},
			{
				1, new SaveFile {
					CompletedLevels = [],
					Slot = 1,
				}
			},
			{
				2, new SaveFile {
					CompletedLevels = [],
					Slot = 2,
				}
			},
		};

		public static Dictionary<int, SaveFile> SaveFiles { get; private set; } = new();

		static SaveManager()
		{
			if (FileAccess.FileExists(Path))
			{
				using FileAccess file = FileAccess.Open(Path, FileAccess.ModeFlags.Read);
				SaveFiles = JsonSerializer.Deserialize<Dictionary<int, SaveFile>>(file.GetLine());
			}
			else
			{
				SaveFiles = DefaultSaveFiles;
				Write();
			}
		}

		/// <summary>
		/// Writes the current SaveFiles array to disk
		/// </summary>
		private static void Write()
		{
			using FileAccess file = FileAccess.Open(Path, FileAccess.ModeFlags.WriteRead);
			file.StoreLine(JsonSerializer.Serialize(SaveFiles));
		}

		/// <summary>
		/// Captures the current player progress and saves it to disk.
		/// </summary>
		/// <param name="slot"></param>
		public static void Save(int slot)
		{
			SaveFiles[slot] = new SaveFile
			{
				Slot = slot,
				World = LevelManager.Instance.World,
				Level = LevelManager.Instance.Level,
				Room = LevelManager.Instance.Room,
				CompletedLevels = LevelManager.Instance.LevelsCompleted,
			};
			Write();
		}

		/// <summary>
		/// Loads the specified save file and returns it.
		/// </summary>
		/// <param name="slot">Which save file</param>
		/// <returns>The save file</returns>
		public static SaveFile Load(int slot)
		{
			return SaveFiles[slot];
		}
	}
}