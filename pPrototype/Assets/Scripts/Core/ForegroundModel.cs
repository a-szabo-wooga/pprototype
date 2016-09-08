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
			if (_cubes == null || _cubes.Length <= id || _cubes[id] == null)
			{
				return false;
			}

			colour = _cubes[id].Front;
			return true;
		}

		public CubeModel GetCubeModelOrNull(int column, int row)
		{
			var id = GetID(column, row);

			if (_cubes == null || _cubes.Length <= id)
			{
				return null;
			}

			return _cubes[id];
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

		public PlayerMove Update(Move move)
		{
			switch (move.Input)
			{
				case MoveInput.SwipeRight:
				case MoveInput.SwipeLeft:
					return UpdateRow(move.Row, move.Input);

				case MoveInput.SwipeUp:
				case MoveInput.SwipeDown:
					return UpdateColumn(move.Column, move.Input);

				default:
					return null;
			}
		}

		private PlayerMove UpdateRow(int row, MoveInput input)
		{
			var indicesToUpdate = new int[Columns];
			var offset = row * Columns;

			for (int i = 0; i < Columns; ++i)
			{
				indicesToUpdate[i] = offset + i;
			}

			return DoUpdate(indicesToUpdate, input);
		}

		private PlayerMove UpdateColumn(int column, MoveInput input)
		{
			var indicesToUpdate = new int[Rows];

			for (int i = 0; i < Rows; ++i)
			{
				indicesToUpdate[i] = column + (Columns * i);
			}

			return DoUpdate(indicesToUpdate, input);
		}

		private PlayerMove DoUpdate(int[] indicesToUpdate, MoveInput input)
		{
			var playerMove = new PlayerMove();

			playerMove.UpdatedIndices = new int[indicesToUpdate.Length];
			playerMove.Input = input;

			for (int i = 0; i < indicesToUpdate.Length; ++i)
			{
				var cube = _cubes[indicesToUpdate[i]];

				if (cube != null)
				{
					cube.Update(input);
				}

				playerMove.UpdatedIndices[i] = indicesToUpdate[i];
			}

			return playerMove;
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

