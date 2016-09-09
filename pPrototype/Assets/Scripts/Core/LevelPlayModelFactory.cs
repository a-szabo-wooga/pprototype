using System.Collections.Generic;
using System.Linq;

namespace pPrototype
{
	public static class LevelPlayModelFactory
	{
		public static LevelPlayModel Create()
		{
			return new LevelPlayModel();
		}

		public static LevelPlayModel Create(LevelData data)
		{
			return Create(data.Cells.ToList(), data.Columns, data.Rows);
		}

		public static LevelPlayModel Create(List<string> level, int columns, int rows)
		{
			var lpm = new LevelPlayModel();

			lpm.SetBackground(columns, rows, ParseBackgroundColours(level, columns, rows));
			lpm.SetForeground(columns, rows, ParseForeground(level, columns, rows));

			return lpm;
		}

		public static CubeModel[] ParseForeground(List<string> level, int cols, int rows)
		{
			var cubes = new CubeModel[cols * rows];

			for (int i = 0; i < level.Count; ++i)
			{
				cubes[i] = ParseCube(level[i]);
			}

			return cubes;
		}

		public static CubeModel ParseCube(string cellData)
		{
			var type = Parser.ParseType(cellData[1]);

			switch (type)
			{
				case CellType.Cube: return CreateCubeFromConfig(cellData);
				case CellType.Special: return CreateSpecialCube();
				default:
					return null;
			}
		}

		public static CubeModel CreateCubeFromConfig(string cellData)
		{
			var front	= Parser.ParseColour(cellData[2]);
			var back 	= Parser.ParseColour(cellData[3]);
			var left	= Parser.ParseColour(cellData[4]);
			var right	= Parser.ParseColour(cellData[5]);
			var top		= Parser.ParseColour(cellData[6]);
			var bottom	= Parser.ParseColour(cellData[7]);

			var cube = new CubeModel(front, back, left, right, top, bottom);

			SetBlockers(cellData, cube);

			return cube ;
		}

		private static void SetBlockers(string cellData, CubeModel cube)
		{
			var stringParts = cellData.Split('_');

			if (stringParts.Length > 1)
			{
				var locks = stringParts[1];
				var setLocks = new List<MoveInput>();
				var lockCount = 0;

				if (locks.Contains('t')) { setLocks.Add(MoveInput.SwipeUp); lockCount++; }
				if (locks.Contains('b')) { setLocks.Add(MoveInput.SwipeDown); lockCount++; }
				if (locks.Contains('r')) { setLocks.Add(MoveInput.SwipeRight); lockCount++;}
				if (locks.Contains('l')) { setLocks.Add(MoveInput.SwipeLeft); lockCount++; }

				var lockArray = new MoveInput[lockCount];
				setLocks.CopyTo(lockArray);

				cube.Lock(lockArray);
			}
		}

		public static CubeModel CreateSpecialCube()
		{
			return new CubeModel(Colour.Special);
		}

		public static Colour[] ParseBackgroundColours(List<string> level, int cols, int rows)
		{
			var colours = new Colour[cols * rows];

			for (int i = 0; i < level.Count; ++i)
			{
				colours[i] = ParseBgColour(level[i]);
			}

			return colours;
		}

		public static Colour ParseBgColour(string cellData)
		{
			return Parser.ParseColour(cellData[0]);
		}
	}
}

