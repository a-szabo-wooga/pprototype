using System;

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

		public int Columns { get; private set; }
		public int Rows { get; private set; }

		public BackgroundModel(int columns, int rows, Colour[] colours)
		{
			Columns = columns;
			Rows = rows;
			AssignColours(colours);
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

