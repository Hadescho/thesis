using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SiriusPrefs))]
public class SiriusPrefsEditor : Editor
{
	bool foldData = false;

	public override void OnInspectorGUI()
	{
		SiriusPrefs prefs = (SiriusPrefs)target;

		prefs.FileName = EditorGUILayout.TextField("File Name", prefs.FileName);
		EditorGUILayout.TextField("Path", prefs.Path);
		prefs.EncryptionKey = EditorGUILayout.TextField("Encryption Key", prefs.EncryptionKey);
		prefs.TimeAutoSave = EditorGUILayout.FloatField("Time Auto Save", prefs.TimeAutoSave);

		foldData = EditorGUILayout.Foldout(foldData, "Data");

		if(foldData)
		{
			List<string> data = new List<string>();

			foreach(DictionaryEntry entry in SiriusPrefs.Data)
			{
				data.Add((string)entry.Key);
			}

			data.Sort();

			for(int i = 0; i < data.Count; i++)
			{
				string key = data[i];

				EditorGUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(key);

					if(SiriusPrefs.Data[key] is bool)
					{
						SiriusPrefs.B[key] = EditorGUILayout.Toggle(SiriusPrefs.B[key]);
					}
					else if(SiriusPrefs.Data[key] is int)
					{
						SiriusPrefs.I[key] = EditorGUILayout.IntField(SiriusPrefs.I[key]);
					}
					else if(SiriusPrefs.Data[key] is float)
					{
						SiriusPrefs.F[key] = EditorGUILayout.FloatField(SiriusPrefs.F[key]);
					}
					else if(SiriusPrefs.Data[key] is string)
					{
						SiriusPrefs.S[key] = EditorGUILayout.TextField(SiriusPrefs.S[key]);
					}
				EditorGUILayout.EndHorizontal();
			}

			if(GUILayout.Button("Delete All"))
			{
				SiriusPrefs.DeleteAll();
			}
		}
	}
}