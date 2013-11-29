using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{
	public float max = 3.0f;
	public float min = -0.6f;
	public float sensitivity = 0.5f;
	public float friction = 0.05f;
	
	private Transform _parent;
	
	private float _max;
	private float _min;
	private bool _dragging;
	private float _dragSpeed;
	private float _dragOffset;
	private Vector3 _dragPosition;

	void Awake()
	{
		_parent = transform.parent;
	}

	void Start()
	{
		_max = max * _parent.localScale.x;
		_min = min * _parent.localScale.x;
		
		_dragOffset = 0.0f;
		_dragSpeed = 0.0f;
		_dragPosition = transform.position;
		
		_dragPosition = transform.position;
		_dragPosition.y = _parent.position.y + _min;	
		transform.position = _dragPosition;
	}
	
	void Update()
	{
		_dragPosition.y += _dragSpeed;
		
		if(_dragPosition.y < _parent.position.y + _min)
		{
			_dragPosition.y = _parent.position.y + _min;
			_dragSpeed = 0.0f;	
		}
		else if(_dragPosition.y > _parent.position.y + _max)
		{
			_dragPosition.y = _parent.position.y + _max;
			_dragSpeed = 0.0f;
		}
		else
		{
			_dragSpeed *= (1.0f - friction);
		}
		
		if(Mathf.Abs(_dragSpeed) < 0.001f)
		{
			_dragSpeed = 0.0f;
		}
		
		Vector3 position = transform.position;
		position.y = _dragPosition.y;
		transform.position = position;
		
		if(!Input.GetMouseButton(0))
		{
			return;
		}
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		ray.origin -= new Vector3(0.0f, 0.0f, 5.0f);
		
		if(!Physics.Raycast(ray, out hit))
		{
			return;
		}
		
		if(Input.GetMouseButtonDown(0))
		{
			_dragOffset = hit.point.y - _dragPosition.y;
		}
		else if(hit.transform.gameObject == gameObject)
		{
			_dragSpeed = (hit.point.y - _dragPosition.y - _dragOffset) * sensitivity;
		}
	}

	public void ScrollToTop()
	{
		_max = max * _parent.localScale.x;
		_min = min * _parent.localScale.x;

		_dragPosition = transform.position;
		_dragPosition.y = _parent.position.y + _min;
		transform.position = _dragPosition;
	}

}
