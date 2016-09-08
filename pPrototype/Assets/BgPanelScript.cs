using UnityEngine;

namespace pPrototype
{
	public class BgPanelScript : MonoBehaviour
	{
		public Material Red;
		public Material Green;
		public Material Blue;
		public Material Yellow;
		public Material Orange;
		public Material White;

		public MeshRenderer QuadRenderer;

		public void Setup(Colour colour)
		{
			QuadRenderer.material = GetMaterialForColour(colour);
		}

		private Material GetMaterialForColour(Colour colour)
		{
			switch (colour)
			{
				case Colour.Blue: return Blue;
				case Colour.Green: return Green;
				case Colour.Red: return Red;
				case Colour.Yellow: return Yellow;
				case Colour.White: return White;
				case Colour.Orange: return Orange;
				default:
					return null;
			}
		}
	}
}
