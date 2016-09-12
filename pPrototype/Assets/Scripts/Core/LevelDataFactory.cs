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
	}
}

