namespace pPrototype
{
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
}

