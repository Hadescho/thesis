using System;
using System.Collections.Generic;
using UnityEngine;

class SiriusUI : MonoBehaviour
{
	private GameObject _lastPressed;

	void Update()
	{
		GameObject hit;

		if(Input.GetMouseButtonDown(0))
		{
			hit = PhysicsExt.Raycast();

			if(hit != null)
			{
				UIComponent uic = hit.GetComponent<UIComponent>();

				if(uic != null)
				{
					uic.OnPress();
					_lastPressed = hit;
				}
			}
		}
		else if(_lastPressed != null && Input.GetMouseButtonUp(0))
		{
			UIComponent uic = _lastPressed.GetComponent<UIComponent>();

			hit = PhysicsExt.Raycast();

			if(hit == _lastPressed)
			{
				uic.OnClick();

				for(int i = 0; i < UI.all.Count; i++)
				{
					UI.all[i].OnClick(uic);
				}
			}

			uic.OnRelease();

			_lastPressed = null;
		}
	}
}