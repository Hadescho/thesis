using UnityEngine;
using System.Collections;

[RequireComponent(typeof(exSprite))]
public class Overlay : MonoBehaviour
{
    public float TransitionTime = 0.5f;

	protected MeshFilter _filter;

	protected string _level = null;
    protected exSprite _sprite;

	protected float _alphaInitial = 0.0f;
	protected float _alphaTarget = 0.0f;
    protected Color _color;

	protected float _timeCurrent = 1.0f;
	protected float _timeTotal = 1.0f;

    public static Overlay Find(string name)
    {
        GameObject go = GameObject.Find(name);

        if(go == null)
        {
            return(null);
        }

        return(go.GetComponent<Overlay>());
    }

	protected void Awake()
	{
        _sprite = GetComponent<exSprite>();
		_filter = GetComponent<MeshFilter>();
		_color = _sprite.color;

		DontDestroyOnLoad(gameObject);
	}

    protected void Update()
    {
        _timeCurrent += Time.deltaTime;

		_color.a = _alphaInitial + (_alphaTarget - _alphaInitial) * (_timeCurrent / _timeTotal);
		_SetColor();

        if(_timeCurrent >= _timeTotal)
        {
            _timeCurrent = _timeTotal;

            if(_level != null)
            {
                Application.LoadLevel(_level);
				FadeTo(0.0f);
                _level = null;
            }
            else
            {
                enabled = false;
            }
        }

        if(_color.a <= 0.0f)
        {
            gameObject.SetActive(false);
        }
    }

    public float Alpha
    {
        set
        {
            _color.a = value;
            GetComponent<exSprite>().color = _color;
        }
        get
        {
            return(_color.a);
        }
    }

	public void FadeTo(float target = 0.8f)
	{
		_color = GetComponent<exSprite>().color;
        _alphaInitial = Mathf.Clamp(_color.a, 0.0f, 1.0f);
        _alphaTarget = target;

        _timeCurrent = 0.0f;
        _timeTotal = TransitionTime;

        enabled = true;
        gameObject.SetActive(true);
	}

    public void FadeTo(string level)
    {
        _level = level;
        FadeTo(1.0f);
    }

	private void _SetColor()
	{
		// Man, ex2D sucks so much, why do I even have to do this...
		// Боби, аре да си направим енджин.

		_sprite.color = _color;
		_sprite.updateFlags &= ~exPlane.UpdateFlags.Color;

		Color[] colors = new Color[_filter.mesh.vertices.Length];
		
		for(int i = 0; i < colors.Length; i++)
		{
			colors[i] = _color;
		}

		_filter.mesh.colors = colors;
	}
}
