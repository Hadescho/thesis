using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

static class SiriusEd
{
	[MenuItem("Sirius/Add all sounds")]
	public static void AddAllSounds()
	{
		SiriusAudio audio = Sirius.FindComponentObject<SiriusAudio>("Sirius");
		string[] paths = Directory.GetFiles(Application.dataPath + "/Sounds/", "*", SearchOption.AllDirectories);

		audio.Clips.Clear();

		for(int i = 0; i < paths.Length; i++)
		{
			if(paths[i].EndsWith(".meta"))
			{
				continue;
			}

			paths[i] = paths[i].Substring(paths[i].IndexOf("/Assets") + 1);

			audio.Clips.Add((AudioClip)AssetDatabase.LoadAssetAtPath(paths[i], typeof(AudioClip)));
			Debug.Log("Added sound: " + paths[i]);
		}
	}
}