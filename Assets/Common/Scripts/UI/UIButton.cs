using UnityEngine;
using System.Collections;

[RequireComponent(typeof(exSprite))]
public class UIButton : UIComponent
{
	//public Texture2D Normal = null;
	public Texture2D Click = null;
    public AudioClip PressSound = null;
    public AudioClip ReleaseSound = null;

	protected int _ixNormal;
	protected int _ixClick;
	
	protected bool _isPressed;

	private exSprite _exs;
	
	public void Awake()
	{
		_exs = GetComponent<exSprite>();

		_ixNormal = _exs.index;
		_ixClick = _exs.atlas.GetIndexByName(Click.name);
		
		if(_ixClick == -1)
		{
			Debug.LogWarning(gameObject.GetPath() + ": Click texture not found in atlas.");
		}
		
		_isPressed = false;
	}

	public bool IsPressed()
	{
		return(_isPressed);	
	}
	
	public override void OnPress()
	{
		_exs.SetSprite(_exs.atlas, _ixClick, false);
		_isPressed = true;

        SiriusAudio.Play(PressSound);
	}
	
	public override void OnRelease()
	{
		_exs.SetSprite(_exs.atlas, _ixNormal, false);
		_isPressed = false;

        SiriusAudio.Play(ReleaseSound);
	}

	public override void OnClick()
	{
		// ...
	}
}
