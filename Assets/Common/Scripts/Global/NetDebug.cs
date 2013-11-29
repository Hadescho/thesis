using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetDebug : MonoBehaviour
{
	public static NetDebug instance;
	
	private static string _deviceID;
	private static Queue<string> _queue = new Queue<string>();
	private static WWW _www = null;
	
	public string URL = "http://zariba.siriushome.com/netlog/log.php";
	
	void Awake()
	{
		if(instance != null)	
		{
			Debug.LogError("More than one NetDebug instances are not allowed.");
			Destroy(this);
			return;	
		}
		
		instance = this;
		
		_deviceID = SystemInfo.deviceUniqueIdentifier;
	}
	
	void Update()
	{
		if(_queue.Count <= 0)
		{
			return;
		}
		
		if(_www == null || _www.isDone)
		{
			_www = 	new WWW(_queue.Dequeue());
		}
	}
	
	public static void Log(object message)
	{
		Debug.Log(message);
		string text = WWW.EscapeURL(message.ToString());
		_queue.Enqueue(instance.URL + "?deviceID=" + _deviceID + "&" + "text=" + text);
	}
	
	public static void LogWarning(object message)
	{
		Debug.LogWarning(message);
		string text = WWW.EscapeURL(message.ToString());
		_queue.Enqueue(instance.URL + "?w&deviceID=" + _deviceID + "&" + "text=" + text);
	}
	
	public static void LogError(object message)
	{
		Debug.LogError(message);
		string text = WWW.EscapeURL(message.ToString());
		_queue.Enqueue(instance.URL + "?e&deviceID=" + _deviceID + "&" + "text=" + text);
	}
}
