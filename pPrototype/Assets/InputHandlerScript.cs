using System.Collections.Generic;
using UnityEngine;

namespace pPrototype
{
	public class InputHandlerScript : MonoBehaviour 
	{
		public const int NONE = -1;

		public LifeCycleScript LifeCycle;

		private int _rowOrColumnID = NONE;
		private List<KeyCode> _alphaKeys;

		private void Awake()
		{
			_alphaKeys = new List<KeyCode>
			{
				KeyCode.Alpha0,
				KeyCode.Alpha1,
				KeyCode.Alpha2,
				KeyCode.Alpha3,
				KeyCode.Alpha4,
				KeyCode.Alpha5,
				KeyCode.Alpha6,
				KeyCode.Alpha7,
				KeyCode.Alpha8,
				KeyCode.Alpha9
			};
		}

		private void Update()
		{
			var id = GetRowOrColumnID();

			if (id != NONE)
			{
				var direction = GetDirection();

				if (direction != MoveInput.None)
				{
					LifeCycle.HandleInput(new Move(id, id, direction));
					_rowOrColumnID = NONE;
				}
			}
		}

		private int GetRowOrColumnID()
		{
			var id = _rowOrColumnID;

			for (int i = 0; i < 10; ++i)
			{
				if (Input.GetKeyUp(_alphaKeys[i]))
				{
					id = i;
					_rowOrColumnID = i;
					break;
				}
			}

			return id;
		}

		private MoveInput GetDirection()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				return MoveInput.SwipeRight;
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				return MoveInput.SwipeLeft;
			}

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				return MoveInput.SwipeUp;
			}

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				return MoveInput.SwipeDown;
			}

			return MoveInput.None;
		}
	}
}
