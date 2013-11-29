using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIFlow))]
class UIPagination : UIComponent
{
	public float Insensitivity = 25.0f;
	public float PageOffset = 1000.0f;
	public float EaseFactor = 15.0f;
	public float SpeedFactor = 0.1f;
	public GameObject PageView = null;
	public float DotDistance = 50.0f;

	protected bool _isPressed = false;

	private int _pages = 1;
	private int _page = 0;

	private float _offset = 0.0f;
	private float _delta = 0.0f;
	private bool _moved = false;

	private GameObject _dot = null;
	private GameObject _dotActive = null;
	private List<GameObject> _dots = null;

	void Awake()
	{
		if(PageView != null)
		{
			_dot = PageView.FindChild("Dot");
			_dotActive = PageView.FindChild("Dot Active");

			_dots = new List<GameObject>();

			_dot.SetActive(false);

			_dots.Add(_dotActive);
		}
	}

	void Update()
	{
		Vector3 position;

		if(_isPressed)
		{
			Vector3 point = (Vector3)PhysicsExt.RaycastPoint();

			position = transform.position;
			_delta = position.x;

			position.x = _offset + point.x;

			_delta -= position.x;
			transform.position = position;

			if(Mathf.Abs(_delta) * Time.deltaTime > Insensitivity)
			{
				_moved = true;
			}
		}
		else
		{
			position = transform.position;
			position.x += (-_page * PageOffset - transform.position.x) * SpeedFactor;
			transform.position = position;
		}
	}

	public bool IsMoved()
	{
		return(_moved);
	}

	public void SetPages(int pages)
	{
		if(pages < 1)
		{
			pages = 1;
		}

		if(pages != _pages)
		{
			_pages = pages;

			SetPage(_page);

			if(PageView != null)
			{
				while(_dots.Count < _pages)
				{
					GameObject dot = _dot.Clone();
					dot.SetActive(true);
					dot.SetParent(PageView);
					_dots.Add(dot);
				}

				while(_dots.Count > _pages)
				{
					Destroy(_dots.Last());
					_dots.RemoveLast();
				}

				float x = -(_dots.Count - 1) * DotDistance / 2.0f;

				for(int i = 0; i < _dots.Count; i++)
				{
					_dots[i].transform.localPosition = new Vector3(x, 0.0f, 0.0f);
					x += DotDistance;
				}
			}
		}
	}

	public void SetPage(int page)
	{
		if(page >= _pages)
		{
			page = _pages - 1;
		}
		else if(page < 0)
		{
			page = 0;
		}

		if(page != _page)
		{
			if(PageView)
			{
				GameObject dotOld = _dots[_page];
				GameObject dotNew = _dots[page];

				Vector3 temp = dotOld.transform.position;
				dotOld.transform.position = dotNew.transform.position;
				dotNew.transform.position = temp;

				_dots[_page] = dotNew;
				_dots[page] = dotOld;
			}

			_page = page;
		}
	}

	public override void OnPress()
	{
		Vector3 point = (Vector3)PhysicsExt.RaycastPoint();

		_offset = transform.position.x - point.x;
		
		_isPressed = true;
		_moved = false;
	}

	public override void OnRelease()
	{
		int page = -(int)Mathf.Round((transform.position.x - _delta * EaseFactor) / PageOffset);

		SetPage(page);
		
		_isPressed = false;
		_moved = false;
	}

	public override void OnClick()
	{
		// ...
	}
}