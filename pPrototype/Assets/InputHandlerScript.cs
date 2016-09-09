using System.Collections.Generic;
using UnityEngine;

namespace pPrototype
{
	public enum SwipeDirection
	{
		None,

		Horizontal,
		Vertical
	}

	public class InputHandlerScript : MonoBehaviour 
	{
		public const int NONE = -1;
		public const int LMB = 0;
		public const float SWIPE_START_THRESHOLD = 0.01f;
		public const float SWIPE_COMMIT_THRESHOLD = 0.05f;

		public LifeCycleScript LifeCycle;

		private int _rowOrColumnID = NONE;
		private List<KeyCode> _alphaKeys;

		private CellScript _swipeStartCell;
		private Vector3 _swipeStartPos = Vector3.zero;
		private SwipeDirection _onGoingSwipe = SwipeDirection.None;

		private void Awake()
		{
			SetupAlphaKeys();
		}

		private void Update()
		{
			HandleKeyboardInput();
			HandleSwipe();
			HandleMouse();
		}

		private void HandleSwipe()
		{
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				HandleHit(Input.GetTouch(0).position);
			}
		}

		private void HandleMouse()
		{
			if (Input.GetMouseButtonDown(LMB))
			{
				HandleHit(Input.mousePosition);
			}

			if (Input.GetMouseButton(LMB))
			{
				CalculateDisplacement(Input.mousePosition);
			}

			if (Input.GetMouseButtonUp(LMB))
			{
				if (_onGoingSwipe != SwipeDirection.None)
				{
					LifeCycle.Snapback();
				}
				_swipeStartPos = Vector3.zero;
				_onGoingSwipe = SwipeDirection.None;
			}
		}

		private void CalculateDisplacement(Vector3 pos)
		{
			if (_swipeStartPos != Vector3.zero)
			{
				var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));
				var delta = worldPos - _swipeStartPos;
				var deltaMagnitude = Vector3.Magnitude(delta);

				if (deltaMagnitude > SWIPE_START_THRESHOLD)
				{
					var hAbs = Mathf.Abs(delta.x);
					var vAbs = Mathf.Abs(delta.y);

					if (_onGoingSwipe == SwipeDirection.None)
					{
						StartNewSwipe(hAbs > vAbs ? SwipeDirection.Horizontal : SwipeDirection.Vertical);
					}

					if (_onGoingSwipe != SwipeDirection.None && _swipeStartCell != null)
					{
						UpdateSwipe(delta, deltaMagnitude, _onGoingSwipe);
					}
				}
				else
				{
					_onGoingSwipe = SwipeDirection.None;
				}
			}
		}

		private void StartNewSwipe(SwipeDirection dir)
		{
			_onGoingSwipe = dir;
		}

		private void UpdateSwipe(Vector3 delta, float deltaMagnitude, SwipeDirection direction)
		{
			if (deltaMagnitude > SWIPE_COMMIT_THRESHOLD)
			{
				LifeCycle.HandleInput(new Move(_swipeStartCell.Column, _swipeStartCell.Row, GetMoveInputFromDelta(delta, direction)));
				ResetSwipe();
			}
			else
			{
				//LifeCycle.FakeSwipe(new Move(_swipeStartCell.Column, _swipeStartCell.Row, GetMoveInputFromDelta(delta, direction)),
				//			    deltaMagnitude);
			}
		}

		private MoveInput GetMoveInputFromDelta(Vector3 delta, SwipeDirection direction)
		{
			switch (direction)
			{
				case SwipeDirection.Horizontal: return delta.x > 0f ? MoveInput.SwipeRight : MoveInput.SwipeLeft;
				case SwipeDirection.Vertical: return delta.y > 0f ? MoveInput.SwipeUp : MoveInput.SwipeDown;
				default:
					return MoveInput.None;
			}
		}

		private void ResetSwipe()
		{
			_swipeStartPos = Vector3.zero;
			_onGoingSwipe = SwipeDirection.None;
			_swipeStartCell = null;
		}

		private void HandleHit(Vector3 pos)
		{
			var ray = Camera.main.ScreenPointToRay(pos);

			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 20f))
			{
				var cell = hit.collider.GetComponent<CellScript>();

				_swipeStartPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));
				_onGoingSwipe = SwipeDirection.None;

				_swipeStartCell = cell;
			}
		}

		private void HandleKeyboardInput()
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

			if (Input.GetKeyDown(KeyCode.Space))
			{
				LifeCycle.SetTransparentFronts(true);
			}

			if (Input.GetKeyUp(KeyCode.Space))
			{
				LifeCycle.SetTransparentFronts(false);
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

		private void SetupAlphaKeys()
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
