using System;
using UnityEngine;

namespace pPrototype
{
	public class BlockerScript : MonoBehaviour
	{
		public GameObject BlockerRight;
		public GameObject BlockerLeft;
		public GameObject BlockerTop;
		public GameObject BlockerBottom;

		public void Setup(CubeModel model)
		{
			BlockerRight.gameObject.SetActive(model.IsLocked(MoveInput.SwipeRight));
			BlockerLeft.gameObject.SetActive(model.IsLocked(MoveInput.SwipeLeft));
			BlockerTop.gameObject.SetActive(model.IsLocked(MoveInput.SwipeUp));
			BlockerBottom.gameObject.SetActive(model.IsLocked(MoveInput.SwipeDown));
		}
	}
}

