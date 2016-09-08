using System;
using System.Collections.Generic;

namespace pPrototype
{
	public static class LevelDataFactory
	{
		public static LevelData MockLevelData()
		{
			return Mock2x2();
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
		}
	}
}

