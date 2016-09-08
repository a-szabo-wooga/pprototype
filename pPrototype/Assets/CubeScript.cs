using UnityEngine;

namespace pPrototype
{
	public class CubeScript : MonoBehaviour
	{
		public MeshRenderer Front;
		public MeshRenderer Back;
		public MeshRenderer Left;
		public MeshRenderer Right;
		public MeshRenderer Top;
		public MeshRenderer Bottom;

		public Material Red;
		public Material Green;
		public Material Blue;
		public Material Yellow;
		public Material Orange;
		public Material White;

		public void Setup(CubeModel model)
		{
			Front.material = GetMaterialForColour(model.Front);
			Back.material = GetMaterialForColour(model.Back);
			Left.material = GetMaterialForColour(model.Left);
			Right.material = GetMaterialForColour(model.Right);
			Top.material = GetMaterialForColour(model.Top);
			Bottom.material = GetMaterialForColour(model.Bottom);
		}

		public void Refresh(MoveInput input)
		{
			switch (input)
			{
				case MoveInput.SwipeLeft: 	Rotate(0f, 90f, 0f); break;
				case MoveInput.SwipeRight: 	Rotate(0f, -90f, 0f); break;
				case MoveInput.SwipeUp:		Rotate(90f, 0f, 0f); break;
				case MoveInput.SwipeDown:	Rotate(-90f, 0f, 0f); break;
				default:
					break;
			}
		}

		public void Rotate(float aroundX, float aroundY, float aroundZ)
		{
			this.transform.Rotate(new Vector3(aroundX, aroundY, aroundZ), Space.World);
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
