using UnityEngine;
using System;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;

public class SiriusConfig : MonoBehaviour
{
	public static SiriusConfig instance;

	public string URL = "http://zariba.siriushome.com/bubble-zoo";
	public float Interval = 1.0f;
	public TextAsset ConfigFile = null;
	public bool UseLocalConfig = false;

	private static float _remaining = 0.0f;
	private static WWW _www = null;
	private static int _time;

	void Awake()
	{
		instance = this;
		
		if(UseLocalConfig)
		{
			if(ConfigFile != null)
			{
				_ParseXML(ConfigFile.text);
				Debug.Log("SiriusConfig: Loaded from local file.");
			}
			else
			{
				Debug.LogError("SiriusConfig: No config file.");
			}
			
			Destroy(this);
			return;
		}

		_remaining = Interval;

		_Request();

		for(int i = 0; i < 10 && !_www.isDone; i++)
		{
			Thread.Sleep(100);
		}

		if(!_www.isDone)
		{
			Debug.LogError("SiriusConfig: Downloading data is taking too long.");
			return;
		}
		
		_OnDone();
	}

	void Update()
	{
		if(_www == null)
		{
			_remaining -= Time.deltaTime;

			if(_remaining <= 0.0f)
			{
				_remaining += Interval;
				_Request();
			}
		}
		else if(_www.isDone)
		{
			_OnDone();
		}
	}

	private void _Request()
	{
		_www = new WWW(URL + "/get.php?time=" + _time);
	}

	private void _OnDone()
	{
		if(_www.error != null)
		{
			Debug.LogError("SiriusConfig: Failed to download data.");
			Debug.LogError(_www.error);
		}
		else if(_www.text.Length > 0)
		{
			_ParseXML(_www.text);
			Debug.Log("SiriusConfig: Updated (" + DateTimeExt.FromUnixTime(_time) + ").");
		}

		_www = null;
	}

	private void _ParseXML(string text)
	{
		StringBuilder xml = new StringBuilder("");
		string[] lines = text.Split('\n');
		Type type = null;

		_time = int.Parse(lines[0]);

		for(int i = 1; i < lines.Length; i++)
		{
			string line = lines[i];

			if(line.StartsWith("@"))
			{
				if(type != null)
				{
					_UpdateConfig(type, xml.ToString());
				}

				string name = line.Substring(1, line.Length - 1).Trim();
				type = Type.GetType(name);

				xml = new StringBuilder();
			}
			else if(type != null)
			{
				xml.Append(line);
			}
		}

		if(type != null && xml.Length > 0)
		{
			_UpdateConfig(type, xml.ToString());
		}
	}

	private void _UpdateConfig(Type type, string xml)
	{
		FieldInfo info = type.GetField("config");
		XmlSerializer szer = new XmlSerializer(info.FieldType);
		info.SetValue(null, szer.Deserialize(new StringReader(xml)));
	}
}
