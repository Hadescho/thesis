using UnityEngine;
using System.Collections;

public class CountingTextBehaviour : SiriusBehaviour
{
	private exSpriteFont _exf;
	private string _prefix;
	private int _start;
	private int _target;
	private float _wait;
	private float _time;
	private float _timeMax;
	
	void Start()
	{
		_exf.text = _prefix + _start;
	}

	void Update()
	{
		if(_wait > 0)
		{
			_wait -= Time.deltaTime;
			
			if(_wait < 0.0f)
			{
				_time -= _wait;	
			}
		}
		else
		{
			_time += Time.deltaTime;
			
			if(_time >= _timeMax)
			{
				_exf.text = _prefix + _target;
				Destroy(this);
			}
			else
			{
				float now = _start + (_target - _start) * Mathf.Sin(_time / _timeMax * Mathf.PI * 0.5f);
				_exf.text = _prefix + (int)now;
			}
		}
	}
	
	public static CountingTextBehaviour Attach(GameObject go, float wait, string prefix, int start, int target, float time)
	{
		CountingTextBehaviour ct = Attach(go.GetComponent<exSpriteFont>(), wait, prefix, start, target, time);
		return(ct);
	}
	
	public static CountingTextBehaviour Attach(exSpriteFont exf, float wait, string prefix, int start, int target, float time)
	{
		CountingTextBehaviour ct = exf.gameObject.AddComponent<CountingTextBehaviour>();
		
		ct._exf = exf;
		ct._wait = wait;
		ct._prefix = prefix;
		ct._start = start;
		ct._target = target;
		ct._timeMax = time;
		ct._time = 0.0f;	
	
		return(ct);
	}
}
