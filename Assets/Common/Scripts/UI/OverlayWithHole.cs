using System;
using UnityEngine;

public class OverlayWithHole : Overlay
{
	new void Awake()
	{
		base.Awake();
	}

	new void Update()
	{
		base.Update();
	}

	public new static OverlayWithHole Find(string name)
	{
		GameObject go = GameObject.Find(name);
		
		if(go == null)
		{
			return(null);	
		}
		
		return(go.GetComponent<OverlayWithHole>());
	}
	
	public void SetHole(float x = 0.0f, float y = 0.0f, float radius = 5.0f)
	{
		float w = _sprite.width * 0.5f;
		float h = _sprite.height * 0.5f;
		float r = radius;

		_filter.mesh.vertices = new Vector3[]
		{
			new Vector3(-w,  h, 0.0f),
			new Vector3( w,  h, 0.0f),
			new Vector3( w, -h, 0.0f),
			new Vector3(-w, -h, 0.0f),

			new Vector3(x - r, y + r, 0.0f),
			new Vector3(x + r, y + r, 0.0f),
			new Vector3(x + r, y - r, 0.0f),
			new Vector3(x - r, y - r, 0.0f)
		};

		_filter.mesh.uv = new Vector2[]
		{
			new Vector2(0.0f, 0.0f),
			new Vector2(1.0f, 0.0f),
			new Vector2(1.0f, 1.0f),
			new Vector2(0.0f, 1.0f),

			new Vector2(0.0001f, 0.0001f),
			new Vector2(0.9999f, 0.0001f),
			new Vector2(0.9999f, 0.9999f),
			new Vector2(0.0001f, 0.9999f)
		};

		_filter.mesh.normals = new Vector3[]
		{
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 0.0f, 0.0f),

			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 0.0f, 0.0f),
			new Vector3(0.0f, 0.0f, 0.0f)
		};

		_filter.mesh.triangles = new int[]
		{
			4, 0, 1,
			4, 1, 5,
			5, 1, 2,
			5, 2, 6,
			6, 2, 3,
			6, 3, 7,
			7, 3, 0,
			7, 0, 4,

			4, 5, 6,
			6, 7, 4
		};
	}
}
