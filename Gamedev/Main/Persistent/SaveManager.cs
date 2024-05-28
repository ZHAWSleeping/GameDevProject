using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Gamedev.Main.Levels;
using Godot;

namespace Gamedev.Main.Persistent
{
	/// <summary>
	/// Holds the data of one save file
	/// </summary>
	public class SaveFile
	{
		public int Slot { get; set; } = -1;
		public int World { get; set; } = -1;
		public int Level { get; set; } = -1;
		public int Room { get; set; } = -1;
		public int Deaths { get; set; } = 0;
		public long PlaytimeTicks { get; set; } = 0;
		public bool[][] CompletedLevels { get; set; }

		public bool IsStarted
		{
			get
			{
				return CompletedLevels.Sum(b => b.Sum(bb => bb ? 1 : 0)) > 0 && PlaytimeTicks > 0;
			}
		}

		public SaveFile()
		{
			CompletedLevels = LevelManager.Instance.LevelsCompleted;
		}
	}

	/// <summary>
	/// Handles the saving and loading of save files.
	/// </summary>
	public static class SaveManager
	{
		public const int Slots = 3;
		private const string Path = "user://user.save";
		private static readonly Dictionary<int, SaveFile> DefaultSaveFiles = new Dictionary<int, SaveFile>
		{
			{
				0, new SaveFile {
					CompletedLevels = [],
					Slot = 0,
					PlaytimeTicks = 18853200,
				}
			},
			{
				1, new SaveFile {
					Slot = 1,
				}
			},
			{
				2, new SaveFile {
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
			SaveFiles[slot] = LevelManager.Instance.State.File;
			Write();
		}

		/// <summary>
		/// Loads the specified save file and returns it.
		/// </summary>
		/// <param name="slot">Which save file</param>
		/// <returns>The save file</returns>
		public static SaveFile Load(int slot)
		{
			if (!SaveFiles.ContainsKey(slot))
			{
				SaveFiles.Add(slot, new SaveFile { Slot = slot });
				Write();
			}
			return SaveFiles[slot];
		}

		public static bool Exists(int slot)
		{
			return SaveFiles.ContainsKey(slot);
		}
	}
}