using System.Collections.Generic;
using UnityEngine;

class FadeBehaviour : SiriusBehaviour
{
	List<exSprite> _sprites = new List<exSprite>();
	List<exSpriteFont> _fonts = new List<exSpriteFont>();
	float _from;
	float _to;
	float _speed;
	bool _destroy;

	void Start()
	{
		_sprites.AddRange(GetComponents<exSprite>());
		_fonts.AddRange(GetComponents<exSpriteFont>());
		_sprites.AddRange(GetComponentsInChildren<exSprite>());
		_fonts.AddRange(GetComponentsInChildren<exSpriteFont>());

		for(int i = 0; i < _sprites.Count; i++)
		{
			_sprites[i].SetAlpha(_from);
		}

		for(int i = 0; i < _fonts.Count; i++)
		{
			_fonts[i].SetAlpha(_from);
		}
	}

	void Update()
	{
		for(int i = 0; i < _sprites.Count; i++)
		{
			Color color = _UpdateColor(_sprites[i].color);
			_sprites[i].color = color;

			if(color.a == _to)
			{
				if(_destroy)
				{
					Destroy(_sprites[i].gameObject);
				}

				_sprites.RemoveAt(i);
				i--;
				continue;
			}
		}

		for(int i = 0; i < _fonts.Count; i++)
		{
			Color color = _UpdateColor(_fonts[i].topColor);
			_fonts[i].SetAlpha(color.a);

			if(color.a == _to)
			{
				if(_destroy)
				{
					Destroy(_fonts[i].gameObject);
				}

				_fonts.RemoveAt(i);
				i--;
				continue;
			}
		}

		if(_sprites.Count <= 0 && _fonts.Count <= 0)
		{
			Destroy(this);
		}
	}

	Color _UpdateColor(Color color)
	{
		float delta = _to - color.a;
		float speed = _speed * Time.deltaTime;

		if(Mathf.Abs(delta) < speed)
		{
			color.a = _to;
		}
		else if(delta < 0.0f)
		{
			color.a -= speed;
		}
		else
		{
			color.a += speed;
		}

		return(color);
	}

	public static FadeBehaviour Attach(GameObject go, float from = 1.0f, float to = 0.0f, float speed = 1.0f, bool destroy = false)
	{
		FadeBehaviour fd = go.AddComponent<FadeBehaviour>();
		fd._from = from;
		fd._to = to;
		fd._speed = speed;
		fd._destroy = destroy;
		return(fd);
	}
}