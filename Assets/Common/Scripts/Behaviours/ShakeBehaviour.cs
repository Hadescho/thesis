using System;
using System.Collections.Generic;
using UnityEngine;

class ShakeBehaviour : SiriusBehaviour
{
	float _normal;
	float _rotation;
	float _a;
	float _b;
	float _velocity;
	float _time;

	void Update()
	{
		_time -= Time.deltaTime;

		if(_time <= 0.0f)
		{
			gameObject.SetRotation(_normal);
			Destroy(this);
			return;
		}

		_rotation += _velocity * Time.deltaTime;

		if(_rotation > _a)
		{
			_rotation = _a;
			_velocity = -_velocity;
		}

		if(_rotation < _b)
		{
			_rotation = _b;
			_velocity = -_velocity;
		}
		
		gameObject.SetRotation(_rotation);
	}

	public static ShakeBehaviour Attach(GameObject go, float magnitude, float velocity, float time)
	{
		ShakeBehaviour sh = go.AddComponent<ShakeBehaviour>();
		sh._normal = go.transform.rotation.z;
		sh._rotation = sh._normal;
		sh._a = sh._normal + magnitude;
		sh._b = sh._normal - magnitude;
		sh._velocity = velocity;
		sh._time = time;
		return(sh);
	}
}