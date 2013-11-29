using UnityEngine;
using System.Collections;

public class UIEffectButton : UIComponent
{
	public AudioClip PressSound = null;
	public AudioClip ReleaseSound = null;

	protected bool _isPressed;

	private GameObject _child;

	public void Start()
	{
		_isPressed = false;

		_child = gameObject.GetChild(0);
		_child.renderer.enabled = false;
	}

	public bool IsPressed()
	{
		return(_isPressed);
	}

	public override void OnPress()
	{
		_child.renderer.enabled = true;
		_isPressed = true;

		SiriusAudio.Play(PressSound);
	}

	public override void OnRelease()
	{
		_child.renderer.enabled = false;
		_isPressed = false;

		SiriusAudio.Play(ReleaseSound);
	}

	public override void OnClick()
	{
		// ...
	}
}