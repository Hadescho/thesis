using System;
using UnityEngine;

public class SiriusBehaviour : MonoBehaviour
{
	private bool _paused = false;
	
	public static void BroadcastAll(string str, System.Object msg, SendMessageOptions options)
	{
		GameObject[] all = (GameObject[])GameObject.FindObjectsOfType(typeof(GameObject));
		
    	foreach(GameObject go in all)
		{
        	if(go.transform.parent == null)
			{
            	go.gameObject.BroadcastMessage(str, msg, options);
       		}
    	}	
	}
	
	public static void PauseAll()
	{
		BroadcastAll("Pause", null, SendMessageOptions.DontRequireReceiver);
	}
	
	public static void ResumeAll()
	{
		BroadcastAll("Resume", null, SendMessageOptions.DontRequireReceiver);
	}
	
	public void Pause()
	{
		if(!_paused && enabled)
		{
			_paused = true;
			enabled = false;
		}
	}
	
	public void Resume()
	{
		if(_paused)
		{
			_paused = false;
			enabled = true;
		}
	}
	
	public bool IsPaused()
	{
		return(_paused);
	}
	
	public void ExecAfter(float time, Callback callback)
	{
		Sirius.ExecAfter(time, callback);
	}
}
