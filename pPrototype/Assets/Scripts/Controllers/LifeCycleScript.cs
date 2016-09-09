using UnityEngine;

namespace pPrototype
{
	public class LifeCycleScript : MonoBehaviour
	{
		public LevelManagerScript LevelManager;
		public GameObject WinPanel;
		private LevelPlayModel _lpm;

		private void Awake()
		{
			Application.targetFrameRate = 30;
		}

		private void Start()
		{
			Reset();
		}

		public void Reset()
		{
			var levelData = LoadLevelData();
			_lpm = LevelPlayModelFactory.Create(levelData);
			LevelManager.Setup(_lpm);
			WinPanel.gameObject.SetActive(false);
		}

		private LevelData LoadLevelData()
		{
			//TODO: don't mock
			return LevelDataFactory.MockLevelData();
		}

		public void HandleInput(Move move)
		{
			if (LevelManager.CanMove() && MoveOK(move))
			{
				LevelManager.SetTransparentFronts(false);
				_lpm.MakeAMove(move);
				LevelManager.Refresh(_lpm);
				EvaluateLevelState();
			}
		}

		public void SetTransparentFronts(bool isTransparent)
		{
			if (LevelManager.CanMove())
			{
				LevelManager.SetTransparentFronts(isTransparent);
			}
		}

		public void FakeSwipe(Move move, float magnitude)
		{
			if (LevelManager.CanMove() && MoveOK(move))
			{
				var playerMove = _lpm.Foreground.Update(move, fakeIt: true);
				LevelManager.FakeSwipe(playerMove, magnitude);
			}
		}

		public void Snapback()
		{
			LevelManager.ClearFakeSwipe();
		}

		private bool MoveOK(Move move)
		{
			if (!_lpm.CanStillMakeMoves())
			{
				return false;
			}

			switch (move.Input)
			{
				case MoveInput.SwipeRight:
				case MoveInput.SwipeLeft:
					return move.Row < _lpm.Background.Rows;

				case MoveInput.SwipeUp:
				case MoveInput.SwipeDown:
					return move.Column < _lpm.Background.Columns;

				default:
					return false;
			}
		}

		private void EvaluateLevelState()
		{
			if (_lpm.CurrentState == LevelPlayState.Won)
			{
				WinPanel.gameObject.SetActive(true);
			}
		}
	}
}
