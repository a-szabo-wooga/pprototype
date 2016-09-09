using UnityEngine;

namespace pPrototype
{
	public static class LevelDataFactory
	{
		public static LevelData MockLevelData()
		{
			return CreateLevel(4);
		}

		public static LevelData CreateLevel(int level)
		{
			var textAsset = Resources.Load<TextAsset>(string.Format("Levels/Level_{0}", level));
			var levelData = JsonUtility.FromJson<LevelData>(textAsset.text);
			return levelData;
		}

		/*public static LevelData Level01()
		{
			var data = new LevelData();

			data.Columns = 1;
			data.Rows = 1;

			data.Cells = new List<string>
			{
				"RcWWrrOO"
			};

			return data;
		}

		public static LevelData Mock3x3()
		{
			var data = new LevelData();

			data.Columns = 3;
			data.Rows = 3;

			data.Cells = new List<string>
			{
				"RcWBrrBW",
				"RcRwwwwR",
				"RcWBrrBW",

				"RcYOrwBG",
				"Re",
				"RcYOwrBG",

				"RcWBrrBW",
				"RcRwwwwR",
				"RcWBrrBW",
			};

			return data;
		}

		private static LevelData Mock2x2()
		{
			var data = new LevelData();

			data.Columns = 2;
			data.Rows = 2;

			data.Cells = new List<string>
			{
				"RcWBrrBW",
				"RcRRwwRR",
				"RcWBrrBW",
				"RcRRwwRR"
			};

			return data;
		}*/
	}
}

