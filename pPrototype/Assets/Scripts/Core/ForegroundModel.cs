namespace pPrototype
{
	public enum CellType
	{
		None,

		Empty,
		Cube,
		Special
	}

	public class ForegroundModel
	{
		public bool TryGet(int column, int row, out Colour colour)
		{
			colour = Colour.None;

			var id = GetID(column, row);
			if (_cubes == null || _cubes.Length <= id)
			{
				return false;
			}

			colour = _cubes[id].Front;
			return true;
		}

		public int GetID(int column, int row)
		{
			return (row * Columns) + column;
		}

		private CubeModel[] _cubes;

		public int Columns { get; private set; }
		public int Rows { get; private set; }

		public ForegroundModel(int columns, int rows, CubeModel[] cubes)
		{
			Columns = columns;
			Rows = rows;
			AssignCubes(cubes);
		}

		public void Update(Move move)
		{
			switch (move.Input)
			{
				case MoveInput.SwipeRight:
				case MoveInput.SwipeLeft:
					UpdateRow(move.Row, move.Input);
					break;

				case MoveInput.SwipeUp:
				case MoveInput.SwipeDown:
					UpdateColumn(move.Column, move.Input);
					break;

				default:
					break;
			}
		}

		private void UpdateRow(int row, MoveInput input)
		{
			var indicesToUpdate = new int[Columns];
			var offset = row * Columns;

			for (int i = 0; i < Columns; ++i)
			{
				indicesToUpdate[i] = offset + i;
			}

			DoUpdate(indicesToUpdate, input);
		}

		private void UpdateColumn(int column, MoveInput input)
		{
			var indicesToUpdate = new int[Rows];

			for (int i = 0; i < Rows; ++i)
			{
				indicesToUpdate[i] = column + (Columns * i);
			}

			DoUpdate(indicesToUpdate, input);
		}

		private void DoUpdate(int[] indicesToUpdate, MoveInput input)
		{
			for (int i = 0; i < indicesToUpdate.Length; ++i)
			{
				_cubes[indicesToUpdate[i]].Update(input);
			}
		}

		private void AssignCubes(CubeModel[] cubes)
		{
			var length = Columns * Rows;
			_cubes = new CubeModel[length];

			if (cubes != null && cubes.Length == length)
			{
				cubes.CopyTo(_cubes, 0);
			}
		}
	}
}

