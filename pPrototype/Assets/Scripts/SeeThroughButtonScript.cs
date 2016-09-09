using UnityEngine;
using UnityEngine.UI;

namespace pPrototype
{
	public class SeeThroughButtonScript : Button
	{
		public InputHandlerScript InputHandler;
	
		private bool _pressed;

		private void Update()
		{
			if (IsPressed())
			{
				if (!_pressed)
				{
					_pressed = true;
					InputHandler.SetTransparency(true);
				}
			}
			else
			{
				if (_pressed)
				{
					_pressed = false;
					InputHandler.SetTransparency(false);
				}
			}
		}
	}
}

