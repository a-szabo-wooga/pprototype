namespace pPrototype
{
	public static class Parser
	{
		public static CellType ParseType(char c)
		{
			switch (c)
			{
				case 'c':
				case 'C':
					return CellType.Cube;

				case 'e':
				case 'E':
					return CellType.Empty;

				case 's':
				case 'S':
					return CellType.Special;

				default:
					return CellType.None;
			}
		}

		public static Colour ParseColour(char c)
		{
			switch (c)
			{
				case 'r':
				case 'R':
					return Colour.Red;

				case 'g':
				case 'G':
					return Colour.Green;

				case 'b':
				case 'B':
					return Colour.Blue;

				case 'y':
				case 'Y':
					return Colour.Yellow;

				case 'w':
				case 'W':
					return Colour.White;

				case 'o':
				case 'O':
					return Colour.Orange;

				case 's':
				case 'S':
					return Colour.Special;

				default:
					return Colour.None;
			}
		}
	}
}

