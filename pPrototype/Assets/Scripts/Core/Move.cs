namespace pPrototype
{
	public struct Move
	{
		public int Column;
		public int Row;
		public MoveInput Input;

		public Move(int col, int row, MoveInput input)
		{
			Input = input;
			Row = row;
			Column = col;
		}
	}
}

