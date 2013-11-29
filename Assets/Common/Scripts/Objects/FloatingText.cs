using UnityEngine;
using System.Collections;

public class FloatingText : SiriusBehaviour
{
	public float TimeLife = 2.0f;
	public float SpeedScale = 0.1f;
	public float ScaleMax = 1.1f;
	public float Friction = 0.0f;
	public Vector3 Speed = new Vector3(0.0f, 0.0f, 0.0f);
	public bool LockZ = true;
	
	private exSpriteFont _exf;
	private float _time;
	private Color _color;
	
	void Awake()
	{
		_exf = GetComponent<exSpriteFont>();
		
		if(_exf == null)
		{
			Debug.LogError(gameObject.GetPath() + ": exSpriteFont not found.");
		}
		
		if(LockZ)
		{
			Speed.z = 0.0f;
		}

		_time = TimeLife;
	}
	
	void Update()
	{
		float alpha = _time / TimeLife * 2.0f;
		
		Color c = _exf.topColor;
		c.a = alpha;
		_exf.topColor = c;
		
		c = _exf.botColor;
		c.a = alpha;
		_exf.botColor = c;
		
		c = _exf.outlineColor;
		c.a = alpha;
		_exf.outlineColor = c;

		transform.position += Speed;
		_time -= Time.deltaTime;

		_exf.scale += (ScaleMax - _exf.scale.x) * new Vector2(SpeedScale, SpeedScale);
		Speed *= (1.0f - Friction);

		if(_time <= 0.0f)
		{
			Destroy(gameObject);
		}
	}
	
	public void SetText(string text)
	{
		_exf.text = text;
	}
	
	public string GetText()
	{
		return(_exf.text);	
	}

	public void SetColor(Color color)
	{
		_color = color;
		_exf.topColor = _color * 0.9f;
		_exf.botColor = _color * 1.1f;
	}

	public Color GetColor()
	{
		return(_color);
	}
	
	public void SetPosition(Vector3 position)
	{
		if(LockZ)
		{
			position.z = transform.position.z;
		}
		
		transform.position = position;
	}
	
	public Vector3 GetPosition()
	{
		return(transform.position);	
	}
	
	public static FloatingText Find(string path)
	{
		GameObject go = GameObject.Find(path);
		
		if(go == null)
		{
			return(null);
		}
		
		return(go.GetComponent<FloatingText>());
	}
	
	public FloatingText Clone(Vector3 position)
	{
		FloatingText ft = gameObject.Clone().GetComponent<FloatingText>();
		ft.SetPosition(position);
		return(ft);
	}
	
	public FloatingText Clone(string text, Vector3 position)
	{
		FloatingText ft = Clone(position);
		ft.SetText(text);
		return(ft);
	}

	public FloatingText Clone(string text, Vector3 position, Color color)
	{
		FloatingText ft = Clone(text, position);
		ft.SetColor(color);
		return(ft);
	}
}
