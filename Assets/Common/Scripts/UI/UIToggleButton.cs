using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class UIToggleButton : UIComponent
{
	public string Option = "";
	public bool DefaultValue = false;

	//public Texture2D NormalOn = null;
	public Texture2D ClickOn = null;
	public Texture2D NormalOff = null;
	public Texture2D ClickOff = null;
	public AudioClip PressSound = null;
	public AudioClip ReleaseSound = null;
	
	int _ixNormalOn;
	int _ixClickOn;
	int _ixNormalOff;
	int _ixClickOff;
	
	bool _data;
	bool _isPressed;

	private exSprite _exs;

	public void Awake()
	{
		_exs = GetComponent<exSprite>();
		
		_ixNormalOn = _exs.index;
		_ixClickOn = _exs.atlas.GetIndexByName(ClickOn.name);
		_ixNormalOff = _exs.atlas.GetIndexByName(NormalOff.name);
		_ixClickOff = _exs.atlas.GetIndexByName(ClickOff.name);

		_data = SiriusPrefs.B[Option];
		
		if(_ixClickOn == -1)
		{
			Debug.LogWarning(gameObject.GetPath() + ": Click (ON) texture not found in atlas.");
		}
		
		if(_ixNormalOff == -1)
		{
			Debug.LogWarning(gameObject.GetPath() + ": Normal (OFF) texture not found in atlas.");
		}
		
		if(_ixClickOff == -1)
		{
			Debug.LogWarning(gameObject.GetPath() + ": Click (OFF) texture not found in atlas.");
		}
			
		_isPressed = false;
	}

	void Start()
	{
		if(_data)
		{
			_exs.SetSprite(_exs.atlas, _ixNormalOn);
		}
		else
		{
			_exs.SetSprite(_exs.atlas, _ixNormalOff);
		}
	}

	public bool IsPressed()
	{
		return(_isPressed);	
	}
	
	public override void OnPress()
	{
		if(_data)	
		{
			_exs.SetSprite(_exs.atlas, _ixClickOn, false);
		}
		else
		{
			_exs.SetSprite(_exs.atlas, _ixClickOff, false);
		}
		
		_isPressed = true;

        SiriusAudio.Play(PressSound);
	}
	
	public override void OnRelease()
	{
		if(_data)	
		{
			_exs.SetSprite(_exs.atlas, _ixNormalOn, false);
		}
		else
		{
			_exs.SetSprite(_exs.atlas, _ixNormalOff, false);
		}
		
		_isPressed = false;

        SiriusAudio.Play(ReleaseSound);
	}

	public override void OnClick()
	{
		_data = !_data;
		SiriusPrefs.B[Option] = _data;
	}
}
