using UnityEngine;

namespace pPrototype
{
	public class LifeCycleScript : MonoBehaviour
	{
		public const int LEVEL_COUNT = 7;
		public const int START_AT = 1;

		public LevelManagerScript LevelManager;
		public GameObject WinPanel;
		private LevelPlayModel _lpm;

		private int _lastLevelPlayed = 1;

		private void Awake()
		{
			Application.targetFrameRate = 30;
			LoadProgress();
		}

		private void LoadProgress()
		{
			#if UNITY_EDITOR
			_lastLevelPlayed = START_AT;
			#else
			_lastLevelPlayed = PlayerPrefs.GetInt("LAST_LEVEL", 1);
			#endif
		}

		private void Start()
		{
			Reset();
		}

		public void Reset()
		{
			var levelData = LoadLevelData(_lastLevelPlayed);
			_lpm = LevelPlayModelFactory.Create(levelData);
			LevelManager.Setup(_lpm);
			WinPanel.gameObject.SetActive(false);
		}

		private LevelData LoadLevelData(int levelToLoad)
		{
			return LevelDataFactory.CreateLevel(levelToLoad);
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
				WinGame();
			}
		}

		private void WinGame()
		{
			WinPanel.gameObject.SetActive(true);
		}

		public void LoadNextLevel()
		{
			_lastLevelPlayed++;

			if (_lastLevelPlayed > LEVEL_COUNT)
			{
				_lastLevelPlayed = 1;
			}

			PlayerPrefs.SetInt("LAST_LEVEL", _lastLevelPlayed);
			PlayerPrefs.Save();

			Reset();
		}
	}
}
