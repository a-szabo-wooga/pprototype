using UnityEngine;

namespace pPrototype
{
	public class CellScript : MonoBehaviour
	{
		public CubeScript Cube;
		public BgPanelScript Background;

		public void Setup(Colour backgroundColour, CubeModel cube)
		{
			Cube.Setup(cube);
			Background.Setup(backgroundColour);
		}
	}
}