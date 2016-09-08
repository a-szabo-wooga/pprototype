using UnityEngine;

namespace pPrototype
{
	public class LifeCycleScript : MonoBehaviour
	{
		public LevelManagerScript LevelManager;
		private LevelPlayModel _lpm;

		private void Start()
		{
			var levelData = LoadLevelData();
			_lpm = LevelPlayModelFactory.Create(levelData);
			LevelManager.Setup(_lpm);
		}

		private LevelData LoadLevelData()
		{
			//TODO: don't mock
			return LevelDataFactory.MockLevelData();
		}

		public void HandleInput(Move move)
		{
			if (MoveOK(move))
			{
				_lpm.MakeAMove(move);
				LevelManager.Refresh(_lpm);
				EvaluateLevelState();
			}
		}

		public void FakeSwipe(Move move, float magnitude)
		{
			if (MoveOK(move))
			{
				var playerMove = _lpm.Foreground.Update(move, fakeIt: true);
				LevelManager.FakeSwipe(playerMove, magnitude);
			}
		}

		private bool MoveOK(Move move)
		{
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
			// TODO
		}
	}
}
