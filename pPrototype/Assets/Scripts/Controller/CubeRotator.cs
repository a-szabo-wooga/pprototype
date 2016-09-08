using UnityEngine;

namespace pPrototype
{
	public class CubeRotator : MonoBehaviour
	{
		public GameObject Cube;

		private void OnValidate()
		{
			Debug.Assert(Cube != null, string.Format("{0} -> Cube missing", this.gameObject.name));
		}

		private void Update()
		{
			ProcessInput();
		}

		private void ProcessInput()
		{
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				RotateCube(0f, -90f, 0f);
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				RotateCube(0f, 90f, 0f);
			}

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				RotateCube(90f, 0f, 0f);
			}

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				RotateCube(-90f, 0f, 0f);
			}
		}

		private void RotateCube(float aroundX, float aroundY, float aroundZ)
		{
			Cube.transform.Rotate(new Vector3(aroundX, aroundY, aroundZ), Space.World);
		}
	}
}
