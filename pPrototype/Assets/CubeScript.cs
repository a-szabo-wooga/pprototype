using System.Collections;
using UnityEngine;

namespace pPrototype
{
	public class CubeScript : MonoBehaviour
	{
		public const float ROT_DEGREE_PER_FRAME = 9f;
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

		public Material Red_Transparent;
		public Material Green_Transparent;
		public Material Blue_Transparent;
		public Material Yellow_Transparent;
		public Material Orange_Transparent;
		public Material White_Transparent;

		private MeshRenderer _cubeMeshRenderer;
		private MeshRenderer OwnMeshRenderer
		{
			get
			{
				if (_cubeMeshRenderer == null)
				{
					_cubeMeshRenderer = GetComponent<MeshRenderer>();
				}

				return _cubeMeshRenderer;
			}
		}

		private CubeModel _model;
		private float _fakedRotation;
		private float _lastFakedMagnitude;
		private MoveInput _fakedInput;

		public void Setup(CubeModel model)
		{
			_model = model;
			SetColor(_model, false);
		}

		public void SetColor(CubeModel model, bool transparent)
		{
			Front.material = GetMaterialForColour(model.Front, transparent);
			Back.material = GetMaterialForColour(model.Back, transparent);
			Left.material = GetMaterialForColour(model.Left, transparent);
			Right.material = GetMaterialForColour(model.Right, transparent);
			Top.material = GetMaterialForColour(model.Top, transparent);
			Bottom.material = GetMaterialForColour(model.Bottom, transparent);
		}

		public void SetTransparent(bool isTransparent)
		{
			OwnMeshRenderer.enabled = !isTransparent;

			var shownFrontSide = _model.SideFacingForward;

			switch (shownFrontSide)
			{
				case Side.Front:	Front.material = GetMaterialForColour(_model.Front, isTransparent); break;
				case Side.Back:		Back.material = GetMaterialForColour(_model.Front, isTransparent); break;
				case Side.Left:		Left.material = GetMaterialForColour(_model.Front, isTransparent); break;
				case Side.Right:	Right.material = GetMaterialForColour(_model.Front, isTransparent); break;
				case Side.Top:		Top.material = GetMaterialForColour(_model.Front, isTransparent); break;
				case Side.Bottom:	Bottom.material = GetMaterialForColour(_model.Front, isTransparent); break;
				default: break;
			}
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
			_fakedInput = MoveInput.None;
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

		public void ClearFakeSwipe()
		{
			if (!Mathf.Approximately(_fakedRotation, 0f))
			{
				Refresh(_fakedInput, -_fakedRotation, false);
				ResetFakeRotation();
			}
		}

		public void FakeSwipe(MoveInput input, float magnitude)
		{
			var magDifference = _lastFakedMagnitude - magnitude;

			if (Mathf.Abs(magDifference) > FAKE_MAGNITUDE_THRESHOLD)
			{
				_lastFakedMagnitude = magnitude;
				_fakedInput = input;

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
				LevelManagerScript.CubeIsMoving();
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

			LevelManagerScript.CubeStoppedMoving();
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

		private Material GetMaterialForColour(Colour colour, bool transparent)
		{
			switch (colour)
			{
				case Colour.Blue: return transparent ? Blue_Transparent : Blue;
				case Colour.Green: return transparent ? Green_Transparent : Green;
				case Colour.Red: return transparent ? Red_Transparent : Red;
				case Colour.Yellow: return transparent ? Yellow_Transparent : Yellow;
				case Colour.White: return transparent ? White_Transparent : White;
				case Colour.Orange: return transparent ? Orange_Transparent : Orange;
				default:
					return null;
			}
		}
	}
}
