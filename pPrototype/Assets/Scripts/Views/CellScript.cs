using UnityEngine;

namespace pPrototype
{
	public class CellScript : MonoBehaviour
	{
		public CubeScript Cube;
		public BgPanelScript Background;
		public GameObject HorizontalLock;
		public GameObject VerticalLock;

		public int Column;
		public int Row;

		public void Setup(int column, int row, Colour backgroundColour, CubeModel cube, bool lockedHorizontally, bool lockedVertically)
		{
			Column = column;
			Row = row;
			SetupCube(cube);
			SetupBackground(backgroundColour, lockedHorizontally, lockedVertically);
		}

		public void Refresh(MoveInput input)
		{
			if (Cube.gameObject.activeInHierarchy)
			{
				Cube.Refresh(input);
			}
		}

		public void LightUpBackground(bool state)
		{
			if (Background.gameObject.activeInHierarchy)
			{
				Background.LightUp(state);
			}
		}

		public void SetTransparentFront(bool state)
		{
			if (Cube.gameObject.activeInHierarchy)
			{
				Cube.SetTransparent(state);
			}
		}

		public void ClearFakeSwipe()
		{
			if (Cube.gameObject.activeInHierarchy)
			{
				Cube.ClearFakeSwipe();
			}
		}

		public void FakeSwipe(MoveInput input, float magnitude)
		{
			if (Cube.gameObject.activeInHierarchy)
			{
				Cube.FakeSwipe(input, magnitude);
			}
		}

		private void SetupCube(CubeModel cube)
		{
			if (cube != null)
			{
				Cube.gameObject.SetActive(true);
				Cube.Setup(cube);
			}
			else
			{
				Cube.gameObject.SetActive(false);
			}
		}

		private void SetupBackground(Colour backgroundColour, bool lockedHorizontally, bool lockedVertically)
		{
			if (backgroundColour != Colour.None)
			{
				Background.gameObject.SetActive(true);
				Background.Setup(backgroundColour);
			}
			else
			{
				Background.gameObject.SetActive(false);
			}

			HorizontalLock.gameObject.SetActive(lockedHorizontally);
			VerticalLock.gameObject.SetActive(lockedVertically);
		}
	}
}