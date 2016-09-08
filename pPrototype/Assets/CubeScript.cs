using System.Collections;
using UnityEngine;

namespace pPrototype
{
	public class CubeScript : MonoBehaviour
	{
		public const float ROT_DEGREE_PER_FRAME = 3f;
		public const float FAKE_MAGNITUDE_THRESHOLD = 0.001f;

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

		private float _fakedRotation;
		private float _lastFakedMagnitude;

		public void Setup(CubeModel model)
		{
			Front.material = GetMaterialForColour(model.Front);
			Back.material = GetMaterialForColour(model.Back);
			Left.material = GetMaterialForColour(model.Left);
			Right.material = GetMaterialForColour(model.Right);
			Top.material = GetMaterialForColour(model.Top);
			Bottom.material = GetMaterialForColour(model.Bottom);
		}

		public void Refresh(MoveInput input, float degree = 90f, bool tween = true)
		{
			if (tween)
			{
				degree -= _fakedRotation;
			}

			switch (input)
			{
				case MoveInput.SwipeLeft: 	Rotate(0f, degree, 0f, tween); break;
				case MoveInput.SwipeRight: 	Rotate(0f, -degree, 0f, tween); break;
				case MoveInput.SwipeUp:		Rotate(degree, 0f, 0f, tween); break;
				case MoveInput.SwipeDown:	Rotate(-degree, 0f, 0f, tween); break;
				default:
					break;
			}

			if (tween)
			{
				ResetFakeRotation();
			}
		}

		private void ResetFakeRotation()
		{
			_lastFakedMagnitude = 0f;
			_fakedRotation = 0f;
		}

		public float GetMoveSign(MoveInput Input)
		{
			switch (Input)
			{
				case MoveInput.SwipeRight:
				case MoveInput.SwipeDown:
					return -1;

				default:
					return 1;
			}
		}

		public void FakeSwipe(MoveInput input, float magnitude)
		{
			var magDifference = _lastFakedMagnitude - magnitude;

			if (Mathf.Abs(magDifference) > FAKE_MAGNITUDE_THRESHOLD)
			{
				_lastFakedMagnitude = magnitude;

				var sign = GetMoveSign(input);
				var rotDegree = sign * ROT_DEGREE_PER_FRAME;

				if ((magDifference > 0f) == (rotDegree < 0f))
				{
					_fakedRotation += rotDegree;
					Refresh(input, rotDegree, false);
				}
				else
				{
					_fakedRotation -= rotDegree;
					Refresh(input, -rotDegree, false);
				}
			}
		}

		public void Rotate(float aroundX, float aroundY, float aroundZ, bool tween = true)
		{
			if (tween)
			{
				StartCoroutine(AnimRotate(aroundX, aroundY, aroundZ));
			}
			else
			{
				this.transform.Rotate(new Vector3(aroundX, aroundY, aroundZ), Space.World);
			}
		}

		private IEnumerator AnimRotate(float aroundX, float aroundY, float aroundZ)
		{
			var goal = 0f;

			var deltaX = GetDelta(aroundX, ref goal);
			var deltaY = GetDelta(aroundY, ref goal);
			var deltaZ = GetDelta(aroundZ, ref goal);

			var increment = Mathf.Max(Mathf.Abs(deltaX), Mathf.Abs(deltaY), Mathf.Abs(deltaZ));

			goal = Mathf.Abs(goal);
			var sum = 0f;

			while (sum < goal)
			{
				this.transform.Rotate(new Vector3(deltaX, deltaY, deltaZ), Space.World);

				yield return new WaitForEndOfFrame();

				sum += increment;
			}
		}

		private float GetDelta(float rotation, ref float goal)
		{
			if (!Mathf.Approximately(rotation, 0f))
			{
				goal = rotation;

				if (rotation > 0f)
				{
					return ROT_DEGREE_PER_FRAME;
				}
				else
				{
					return -ROT_DEGREE_PER_FRAME;
				}
			}

			return 0f;
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
