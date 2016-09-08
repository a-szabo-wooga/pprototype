using System.Collections.Generic;

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
			return Create(data.Cells, data.Columns, data.Rows);
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

			return new CubeModel(front, back, left, right, top, bottom);
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

