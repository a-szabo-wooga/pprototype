using System.Collections.Generic;

namespace pPrototype
{
	public enum Side
	{
		None,

		Front,
		Back,
		Left,
		Right,
		Top,
		Bottom
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

		private Dictionary <Side, Side> _s = new Dictionary<Side, Side>();

		public Side SideFacingForward
		{
			get
			{
				return _s[Side.Front];
			}
		}

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

			SetupSideBookkeeping();
		}

		private void SetupSideBookkeeping()
		{
			_s[Side.Front]	= Side.Front;
			_s[Side.Back]		= Side.Back;
			_s[Side.Left]		= Side.Left;
			_s[Side.Right]	= Side.Right;
			_s[Side.Top]		= Side.Top;
			_s[Side.Bottom]	= Side.Bottom;
		}

		public bool Update(MoveInput input)
		{
			var success = true;

			switch (input)
			{
				case MoveInput.SwipeRight: SwipeRight(); break;
				case MoveInput.SwipeLeft: SwipeLeft(); break;
				case MoveInput.SwipeUp: SwipeUp(); break;
				case MoveInput.SwipeDown: SwipeDown(); break;

				default:
					success = false;
					break;
			}

			return success;
		}

		private void SwipeRight()
		{
			Swap(ref _front, ref _right);	SwapSides(Side.Front, Side.Right);
			Swap(ref _front, ref _back);	SwapSides(Side.Front, Side.Back);
			Swap(ref _front, ref _left);	SwapSides(Side.Front, Side.Left);
		}

		private void SwipeLeft()
		{
			Swap(ref _front, ref _left);	SwapSides(Side.Front, Side.Left);
			Swap(ref _front, ref _back);	SwapSides(Side.Front, Side.Back);
			Swap(ref _front, ref _right);	SwapSides(Side.Front, Side.Right);
		}

		private void SwipeUp()
		{
			Swap(ref _front, ref _top);		SwapSides(Side.Front, Side.Top);
			Swap(ref _front, ref _back);	SwapSides(Side.Front, Side.Back);
			Swap(ref _front, ref _bottom);	SwapSides(Side.Front, Side.Bottom);
		}

		private void SwipeDown()
		{
			Swap(ref _front, ref _bottom);	SwapSides(Side.Front, Side.Bottom);
			Swap(ref _front, ref _back);	SwapSides(Side.Front, Side.Back);
			Swap(ref _front, ref _top);		SwapSides(Side.Front, Side.Top);
		}

		private void SwapSides(Side a, Side b)
		{
			var carry = _s[a];

			_s[a] = _s[b];
			_s[b] = carry;
		}

		private void Swap<T>(ref T a, ref T b)
		{
			var carry = a;

			a = b;
			b = carry;
		}
	}
}

