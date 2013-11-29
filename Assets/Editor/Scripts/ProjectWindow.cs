using UnityEngine;
using System.Collections;
using UnityEditor;


public class ProjectWindow : EditorWindow {
	[MenuItem("Gotinoto-td/Place Blockers")]
	public static void Placelockers()
	{
		
		object[] allObjects = GameObject.FindSceneObjectsOfType(typeof(GameObject));
		foreach(object o in allObjects)
		{
			GameObject go = (GameObject) o;
			
			if (go.name == "BuildBlocker")
			{
				go = TowerSpawn.Grid(go);
			}
		}
	}
}
