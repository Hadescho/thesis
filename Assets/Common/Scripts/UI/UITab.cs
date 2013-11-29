using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITab : UIComponent
{
	public static Hashtable groups = new Hashtable();

	public string Group = "";
	public float ZOffset = 0.3f;
	public GameObject Panel = null;
	public bool Default = false;

	//public Texture2D Normal = null;
	public Texture2D Selected = null;
	public AudioClip Press = null;
	public AudioClip Release = null;

	protected int _ixNormal;
	protected int _ixSelected;

	private exSprite _exs;

	private bool _isSelected;

	void Awake()
	{
		_exs = GetComponent<exSprite>();

		_ixNormal = _exs.index;
		_ixSelected = _exs.atlas.GetIndexByName(Selected.name);

		if(_ixSelected == -1)
		{
			Debug.LogWarning(gameObject.GetPath() + ": Click texture not found in atlas.");
		}

		_isSelected = false;

		if(!groups.ContainsKey(Group))
		{
			groups.Add(Group, null);
		}
	}

	void Start()
	{
		if(Default)
		{
			Select();
		}
		else
		{
			Panel.SetActive(false);
		}
	}
	
	void OnDestroy()
	{
		if(groups[Group] == this)
		{
			groups[Group] = null;
		}
	}
	
	public override void OnPress()
	{
		SiriusAudio.Play(Press);
	}

	public override void OnRelease()
	{
		SiriusAudio.Play(Release);
	}

	public override void OnClick()
	{
		Select();
	}

	public bool IsSelected()
	{
		return(_isSelected);
	}

	public void Select()
	{
		if(_isSelected)
		{
			return;
		}

		_exs.SetSprite(_exs.atlas, _ixSelected, false);

		if(groups[Group] != null)
		{
			UITab tabOld = (UITab)groups[Group];
			tabOld.Deselect();
		}

		_isSelected = true;
		groups[Group] = this;

		Panel.SetActive(true);
		gameObject.SetLocalZ(gameObject.GetLocalZ() - ZOffset);
	}

	public void Deselect()
	{
		_exs.SetSprite(_exs.atlas, _ixNormal, false);

		_isSelected	 = false;
		groups[Group] = null;

		Panel.SetActive(false);
		gameObject.SetLocalZ(gameObject.GetLocalZ() + ZOffset);
	}
}