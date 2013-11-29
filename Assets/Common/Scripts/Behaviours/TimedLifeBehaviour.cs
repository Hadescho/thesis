using UnityEngine;
using System.Collections;

public class TimedLifeBehaviour : SiriusBehaviour
{
	private float _time = 3.0f;
	
	void Update()
	{
		_time -= Time.deltaTime;
		
		if(_time <= 0.0f)
		{
			Destroy(gameObject);
		}
	}
	
	public static TimedLifeBehaviour Attach(GameObject go, float time)
	{
		TimedLifeBehaviour tl = go.AddComponent<TimedLifeBehaviour>();
		tl._time = time;
		return(tl);
	}
}
