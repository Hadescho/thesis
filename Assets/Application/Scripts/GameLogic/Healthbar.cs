using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour 
{
	private float _start;
	private float _target;
	private float _step;
	private float _timer;
	GameObject filler;
	
	void Start ()
	{
		filler = gameObject.transform.FindChild("Filler").gameObject;
		_timer=3.0f;
		_start=1.0f;
	}
	
	
	void Update ()
	{
		
		_timer+=Time.deltaTime;
		if(_timer>3.0f)
		{
			gameObject.SetActive(false);	
		}
		Resize();
	}
	
	public void Resize()
	{
		if(_start>_target)
		{
			_step=(_target - _start) * 0.1f;
			Vector3 scale = filler.transform.localScale;
			scale.x += _step;
			filler.transform.localScale =scale;
			_start+=_step;
		}
	}
	public void Show( float target)
	{		
		int type;
		type=gameObject.GetParent().transform.GetComponent<Enemy>().currEnemyTypeForHealthbar;
		_target=target/Enemy.config.max_healths[type];
		gameObject.SetActive(true);		
		_timer=0.0f;
	}
	
}