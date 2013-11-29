using UnityEngine;

class MoveToBehaviour : SiriusBehaviour
{
	private Vector3 _target = new Vector3(0.0f, 0.0f, 0.0f);
	private float _speed = 0.1f;
	private float _distance;

	private Vector3 _position;
	private Vector3 _positionPrev;
	private Vector3 _positionNext;

	void Awake()
	{
		_position = transform.position;
	}

	void Start()
	{
		_distance = (_target - transform.position).magnitude;
	}

	void FixedUpdate()
	{
		_positionPrev = _position;

		Vector3 step = (_target - _position) * _speed;
		_position += step;

		_distance -= step.magnitude;

		if(_distance < 0.0f)
		{
			_position = _target;
			transform.position = _target;
			Destroy(this);
		}

		_positionNext =_position;
	}

	void Update()
	{
		float t = (Time.time % Time.fixedDeltaTime / Time.fixedDeltaTime);
		transform.position = _positionPrev + (_positionNext - _positionPrev) * t;
	}
	
	public static MoveToBehaviour Attach(GameObject go, Vector3 target, float speed)
	{
		MoveToBehaviour mt = go.AddComponent<MoveToBehaviour>();
		
		mt._target = target;
		mt._speed = speed;
		
		return(mt);
	}
}
