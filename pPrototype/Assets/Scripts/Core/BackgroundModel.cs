using System;
using System.Collections.Generic;

namespace pPrototype
{
	public class BackgroundModel
	{
		public bool TryGet(int column, int row, out Colour colour)
		{
			colour = Colour.None;

			var id = GetID(column, row);
			if (_colours == null || _colours.Length <= id)
			{
				return false;
			}

			colour = _colours[id];
			return true;
		}

		public int GetID(int column, int row)
		{
			return (row * Columns) + column;
		}

		private Colour[] _colours;
		private List<int> _lockedRows;
		private List<int> _lockedColumns;

		public int Columns { get; private set; }
		public int Rows { get; private set; }

		public bool IsLockedHorizontally(int rowIndex)
		{
			return _lockedRows.Contains(rowIndex);
		}

		public bool IsLockedVertically(int columnIndex)
		{
			return _lockedColumns.Contains(columnIndex);
		}

		public void LockColumns(params int[] columnsToLock)
		{
			foreach (var index in columnsToLock)
			{
				_lockedColumns.Add(index);
			}
		}

		public void LockRows(params int[] rowsToLock)
		{
			foreach (var index in rowsToLock)
			{
				_lockedRows.Add(index);
			}
		}

		public BackgroundModel(int columns, int rows, Colour[] colours)
		{
			Columns = columns;
			Rows = rows;
			AssignColours(colours);

			_lockedRows = new List<int>();
			_lockedColumns = new List<int>();
		}

		private void AssignColours(Colour[] assignedColours)
		{
			var length = Columns * Rows;
			_colours = new Colour[length];

			if (assignedColours != null && assignedColours.Length == length)
			{
				assignedColours.CopyTo(_colours, 0);
			}
		}
	}
}

