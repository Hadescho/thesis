using System;
using UnityEngine;

public class Sirius : SiriusBehaviour
{
	public static Sirius instance;

	void Awake()
	{
		if(instance != null)
		{
			Debug.LogError("More than one Sirius instances are not allowed.");
			Destroy(this);
			return;
		}
		
		instance = this;
		
		DontDestroyOnLoad(gameObject);
	}
	
	public static bool IsFirstPlay()
	{
		bool first = SiriusPrefs.B["IsFirstPlay", true];
		
		if(first)
		{
			SiriusPrefs.B["IsFirstPlay"] = false;
		}
		
		return(first);
	}
	
	public new static void ExecAfter(float time, Callback callback)
	{
		TimedExecBehaviour.Attach(instance.gameObject, time, callback);
	}
	
	public static T FindComponentObject<T>(string path) where T : Component
	{
		GameObject go = GameObject.Find(path);
		
		if(go == null)
		{
			return(null);
		}
		
		return(go.GetComponent<T>());
	}
}