using UnityEngine;

public delegate void Callback();

class TimedExecBehaviour : SiriusBehaviour
{
	private float _time;
	private Callback _callback;
	
	void Update()
	{
		_time -= Time.deltaTime;
		
		if(_time <= 0.0f)
		{
			_callback();
			Destroy(this);
		}
	}
	
	public static TimedExecBehaviour Attach(GameObject go, float time, Callback callback)
	{
		TimedExecBehaviour te = go.AddComponent<TimedExecBehaviour>();
		
		te._time = time;
		te._callback = callback;
		
		return(te);
	}
}