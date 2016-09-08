using UnityEngine;

namespace pPrototype
{
	public class CellScript : MonoBehaviour
	{
		public CubeScript Cube;
		public BgPanelScript Background;
		public int Column;
		public int Row;

		public void Setup(int column, int row, Colour backgroundColour, CubeModel cube)
		{
			Column = column;
			Row = row;
			SetupCube(cube);
			SetupBackground(backgroundColour);
		}

		public void Refresh(MoveInput input)
		{
			if (Cube.gameObject.activeInHierarchy)
			{
				Cube.Refresh(input);
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

		private void SetupBackground(Colour backgroundColour)
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
		}
	}
}