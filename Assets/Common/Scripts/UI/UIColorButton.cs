using UnityEngine;
using System.Collections;

[RequireComponent(typeof(exSprite))]
public class UIColorButton : UIComponent
{
	public Color NormalColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	public Color ClickColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	public AudioClip PressSound = null;
	public AudioClip ReleaseSound = null;

	protected bool _isPressed;

	private exSprite _exs;

	public void Awake()
	{
		_exs = GetComponent<exSprite>();

		_isPressed = false;
	}

	public bool IsPressed()
	{
		return(_isPressed);
	}

	public override void OnPress()
	{
		_exs.color = NormalColor;
		_isPressed = true;

		SiriusAudio.Play(PressSound);
	}

	public override void OnRelease()
	{
		_exs.color = ClickColor;
		_isPressed = false;

		SiriusAudio.Play(ReleaseSound);
	}

	public override void OnClick()
	{
		
	}
}
