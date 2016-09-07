using System;

public enum Colour
{
	None,

	Special,

	Red,
	Green,
	Blue,
	Yellow,
	White,
}

public enum LevelPlayState
{
	None,

	Unstarted,
	Ongoing,
	Won,
	Failed
}

public enum MoveInput
{
	None,

	SwipeUp,
	SwipeRight,
	SwipeDown,
	SwipeLeft,

	Press,
}

public struct Move
{
	public MoveInput Input;
	public int Row;
	public int Column;
}

public class CubeModel
{
	private Colour _front;
	public Colour Front { get { return _front;  } }

	private Colour _back;
	public Colour Back { get { return _back; } }

	private Colour _left;
	public Colour Left { get { return _left; } }

	private Colour _right;
	public Colour Right { get { return _right; } }

	private Colour _top;
	public Colour Top { get { return _top; } }

	private Colour _bottom;
	public Colour Bottom { get { return _bottom; } }

	public CubeModel() : this (Colour.Red, Colour.Red, Colour.Green, Colour.Yellow, Colour.Blue, Colour.White)
	{
		// Default cube: front-back: red, left: green, right: yellow, top: blue, bottom: white
	}

	public CubeModel(Colour singleColour) : this (singleColour, singleColour, singleColour,
												  singleColour, singleColour, singleColour)
	{
	}

	public CubeModel(Colour front, Colour back, Colour left, Colour right, Colour top, Colour bottom)
	{
		_front = front;
		_back = back;
		_left = left;
		_right = right;
		_top = top;
		_bottom = bottom;
	}

	public void Update(MoveInput input)
	{
		switch (input)
		{
			case MoveInput.SwipeRight: SwipeRight(); break;
			case MoveInput.SwipeLeft: SwipeLeft(); break;
			case MoveInput.SwipeUp: SwipeUp(); break;
			case MoveInput.SwipeDown: SwipeDown(); break;

			default:
				break;
		}
	}

	private void SwipeRight()
	{
		Swap(ref _front, ref _right);
		Swap(ref _front, ref _back);
		Swap(ref _front, ref _left);
	}

	private void SwipeLeft()
	{
		Swap(ref _front, ref _left);
		Swap(ref _front, ref _back);
		Swap(ref _front, ref _right);
	}

	private void SwipeUp()
	{
		Swap(ref _front, ref _top);
		Swap(ref _front, ref _back);
		Swap(ref _front, ref _bottom);
	}

	private void SwipeDown()
	{
		Swap(ref _front, ref _bottom);
		Swap(ref _front, ref _back);
		Swap(ref _front, ref _top);
	}

	private void Swap(ref Colour a, ref Colour b)
	{
		var carry = a;

		a = b;
		b = carry;
	}
}

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

public class ForegroundModel
{

}

public class LevelPlayData
{

}



public static class LevelPlayModelFactory
{
	public static LevelPlayModel Create()
	{
		return new LevelPlayModel();
	}
}

public class LevelPlayModel
{
	public BackgroundModel Background;
	public ForegroundModel Foreground;
	public LevelPlayData Statistics;
	public LevelPlayState CurrentState = LevelPlayState.Unstarted;

	public bool CanStillMakeMoves()
	{
		return CurrentState == LevelPlayState.Unstarted || CurrentState == LevelPlayState.Ongoing;
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
		
	}

}