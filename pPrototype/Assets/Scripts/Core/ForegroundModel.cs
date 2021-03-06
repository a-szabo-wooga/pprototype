﻿using System.Collections.Generic;

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

		private Dictionary<int, HashSet<int>> _lockedColumns;
		private Dictionary<int, HashSet<int>> _lockedRows;

		public int Columns { get; private set; }
		public int Rows { get; private set; }

		public ForegroundModel(int columns, int rows, CubeModel[] cubes)
		{
			Columns = columns;
			Rows = rows;

			_lockedColumns = new Dictionary<int, HashSet<int>>();
			_lockedRows = new Dictionary<int, HashSet<int>>();

			AssignCubes(cubes);
		}

		public void LockColumns(int columnA, int columnB)
		{
			AddLockedColumn(columnA, columnB);
			AddLockedColumn(columnB, columnA);
		}

		public void LockRows(int rowA, int rowB)
		{
			AddLockedRow(rowA, rowB);
			AddLockedRow(rowB, rowA);
		}

		private void AddLockedRow(int rowA, int rowB)
		{
			if (!_lockedRows.ContainsKey(rowA))
			{
				_lockedRows[rowA] = new HashSet<int>();
			}

			_lockedRows[rowA].Add(rowB);
		}

		private void AddLockedColumn(int columnA, int columnB)
		{
			if (!_lockedColumns.ContainsKey(columnA))
			{
				_lockedColumns[columnA] = new HashSet<int>();
			}

			_lockedColumns[columnA].Add(columnB);
		}

		public PlayerMove Update(Move move, bool fakeIt = false)
		{
			switch (move.Input)
			{
				case MoveInput.SwipeRight:
				case MoveInput.SwipeLeft:
					return UpdateRow(move.Row, move.Input, fakeIt);

				case MoveInput.SwipeUp:
				case MoveInput.SwipeDown:
					return UpdateColumn(move.Column, move.Input, fakeIt);

				default:
					return null;
			}
		}

		private PlayerMove UpdateRow(int row, MoveInput input, bool fakeIt)
		{
			var indicesToUpdate = new List<int>();
			var originalOffset = row * Columns;

			HashSet<int> lockedRows;
			_lockedRows.TryGetValue(row, out lockedRows);

			var offSets = new List<int> { originalOffset };

			if (lockedRows != null)
			{
				foreach (var lockedRow in lockedRows)
				{
					if (lockedRow != originalOffset)
					{
						offSets.Add(lockedRow * Columns);
					}
				}
			}

			foreach (var offset in offSets)
			{
				for (int i = 0; i < Columns; ++i)
				{
					indicesToUpdate.Add(offset + i);
				}
			}

			return DoUpdate(indicesToUpdate, input, fakeIt);
		}

		private PlayerMove UpdateColumn(int column, MoveInput input, bool fakeIt)
		{
			var indicesToUpdate = new List<int>();

			HashSet<int> lockedColumns;
			_lockedColumns.TryGetValue(column, out lockedColumns);

			for (int i = 0; i < Rows; ++i)
			{
				indicesToUpdate.Add(column + (Columns * i));

				if (lockedColumns != null)
				{
					foreach (var lockedColumn in lockedColumns)
					{
						indicesToUpdate.Add(lockedColumn + (Columns * i));
					}
				}
			}

			return DoUpdate(indicesToUpdate, input, fakeIt);
		}

		private PlayerMove DoUpdate(List<int> indicesToUpdate, MoveInput input, bool fakeIt)
		{
			var playerMove = new PlayerMove();

			playerMove.UpdatedIndices = new List<int>();
			playerMove.Input = input;

			for (int i = 0; i < indicesToUpdate.Count; ++i)
			{
				var cube = _cubes[indicesToUpdate[i]];

				var couldHandleIt = false;

				if (cube != null && !fakeIt) 
				{
					couldHandleIt = cube.Update(input);
				}

				if (couldHandleIt)
				{
					playerMove.UpdatedIndices.Add(indicesToUpdate[i]);
				}
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

