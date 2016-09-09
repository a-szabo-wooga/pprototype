using System.Collections.Generic;

namespace pPrototype
{
	public class PlayerMove
	{
		public int[] UpdatedIndices;
		public MoveInput Input;
	}

	public class LevelPlayModel
	{
		public BackgroundModel Background;
		public ForegroundModel Foreground;
		public LevelPlayData Statistics = new LevelPlayData();
		public LevelPlayState CurrentState = LevelPlayState.Unstarted;

		private Stack<PlayerMove> _moves = new Stack<PlayerMove>();

		public PlayerMove GetLastMove()
		{
			if (_moves != null && _moves.Count > 0)
			{
				return _moves.Peek();
			}

			return null;
		}

		public void SetBackground(int columns, int rows, Colour[] colours)
		{
			Background = new BackgroundModel(columns, rows, colours);
		}

		public void SetForeground(int columns, int rows, CubeModel[] cubes)
		{
			Foreground = new ForegroundModel(columns, rows, cubes);
		}

		public bool CanStillMakeMoves()
		{
			return CurrentState == LevelPlayState.Unstarted || CurrentState == LevelPlayState.Ongoing;
		}

		public bool CellCorrect(int column, int row)
		{
			Colour bg, fg;

			if (Background.TryGet(column, row, out bg))
			{
				Foreground.TryGet(column, row, out fg);

				return fg == bg || fg == Colour.None || fg == Colour.Special;
			}

			return false;
		}

		public void MakeAMove(Move move)
		{
			if (CanStillMakeMoves())
			{
				Update(move);
			}
		}

		private void Update(Move move)
		{
			Statistics.MovesStarted++;
			_moves.Push(Foreground.Update(move, fakeIt: false));
			UpdateCurrentState();
		}

		private void UpdateCurrentState()
		{
			var allCellsOK = true;

			for (int i = 0; i < Background.Columns; ++i)
			{
				for (int j = 0; j < Background.Rows; ++j)
				{
					allCellsOK &= CellCorrect(i, j);

					if (!allCellsOK)
					{
						break;
					}
				}
			}

			if (allCellsOK)
			{
				CurrentState = LevelPlayState.Won;
			}
			else
			{
				CurrentState = Statistics.MovesStarted > 0 ? LevelPlayState.Ongoing : LevelPlayState.Unstarted;
			}
		}
	}
}
