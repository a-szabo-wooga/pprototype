using System.Collections.Generic;
using UnityEngine;

namespace pPrototype
{
	public class TestSpawnerScript : MonoBehaviour
	{
		public LevelManagerScript LevelManager;

		private void Start()
		{
			var lpm = CreateThreeByThreeTestLPM();
			LevelManager.Setup(lpm);
		}

		private LevelPlayModel CreateTestLevelPlayModel()
		{
			var data = "RcwrBGoy";
			var level = new List<string> { data };
			return LevelPlayModelFactory.Create(level, 1, 1);
		}

		private LevelPlayModel CreateOtherTestLPM()
		{
			var cellOne = "RcwrBGoy";
			var cellTwo = "RcrwBGoy";

			var level = new List<string> { cellOne, cellTwo };
			return LevelPlayModelFactory.Create(level, 2, 1);
		}

		private LevelPlayModel CreateThreeTestLPM()
		{
			var cellOne = "RcwrBGoy";
			var cellTwo = "RcrwBGoy";
			var cellThree = "Bcrwrwrw";
			var cellFour = "Ee";

			var level = new List<string> { cellOne, cellTwo, cellFour, cellThree };
			return LevelPlayModelFactory.Create(level, 2, 2);
		}

		private LevelPlayModel CreateThreeByThreeTestLPM()
		{
			var cellOne = "RcwrBGoy";
			var cellTwo = "RcrwBGoy";
			var cellThree = "Rcrwrwrw";
			var cellFour = "Ee";
			var cellFive = "BcrwBGoy";
			var cellSix = "Ee";

			var level = new List<string> { cellOne, cellTwo, cellThree, cellFour, cellFive, cellSix, cellOne, cellTwo, cellThree };
			return LevelPlayModelFactory.Create(level, 3, 3);
		}

	}
}
