using System.Collections.Generic;
using UnityEngine;

public struct Cell
{
	public Vector2 TopLeft;
	public Vector2 BottomRight;

	public Cell(int column, int row)
	{
		TopLeft = new Vector2(column, row);
		BottomRight = new Vector2(column + 1, row + 1);
	}
}

public class Grid
{
	private readonly int _columnCount;
	private readonly int _rowCount;

	private Dictionary<int, Cell> _cells;

	public Grid(int columns, int rows)
	{
		_columnCount = columns;
		_rowCount = rows;
		_cells = new Dictionary<int, Cell>();

		for (int i = 0; i < rows; ++i)
		{
			for (int j = 0; j < columns; ++j)
			{
				_cells[GetCellID(j, i)] = new Cell(j, i);

			}
		}
	}

	public int GetCellID(int column, int row)
	{
		return (row * _columnCount) + column;
	}
}

