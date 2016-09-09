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

		public Material Red_Emissive;
		public Material Green_Emissive;
		public Material Blue_Emissive;
		public Material Yellow_Emissive;
		public Material Orange_Emissive;
		public Material White_Emissive;

		public MeshRenderer QuadRenderer;

		private Material _normal;
		private Material _lit;

		public void Setup(Colour colour)
		{
			_normal = GetMaterialForColour(colour);
			_lit = GetEmissiveMaterialForColour(colour);

			LightUp(false);
		}

		public void LightUp(bool state)
		{
			if (state)
			{
				QuadRenderer.material = _normal;
			}
			else
			{
				QuadRenderer.material = _lit;
			}
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

		private Material GetEmissiveMaterialForColour(Colour colour)
		{
			switch (colour)
			{
				case Colour.Blue: return Blue_Emissive;
				case Colour.Green: return Green_Emissive;
				case Colour.Red: return Red_Emissive;
				case Colour.Yellow: return Yellow_Emissive;
				case Colour.White: return White_Emissive;
				case Colour.Orange: return Orange_Emissive;
				default:
					return null;
			}
		}
	}
}
